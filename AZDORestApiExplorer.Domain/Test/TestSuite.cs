using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Test.Events;

using Newtonsoft.Json;

using Prism.Events;

namespace AZDORestApiExplorer.Domain.Test
{
    namespace Events
    {
        public class GetTestSuitesEvent : PubSubEvent<GetTestSuitesEventArgs> { }

        public class GetTestSuitesEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Domain.Core.Project Project;

            public Domain.Test.TestPlan TestPlan;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedTestSuiteChangedEvent : PubSubEvent<TestSuite> { }
    }

    public class TestSuitesRoot
    {
        public int count { get; set; }
        public TestSuite[] value { get; set; }
    }

    public class TestSuite
    {
        public int id { get; set; }
        public int revision { get; set; }
        public Project project { get; set; }
        public Lastupdatedby lastUpdatedBy { get; set; }
        public DateTime lastUpdatedDate { get; set; }
        public Plan plan { get; set; }
        public _Links1 _links { get; set; }
        public string suiteType { get; set; }
        public string name { get; set; }
        public bool inheritDefaultConfigurations { get; set; }
        public Defaultconfiguration[] defaultConfigurations { get; set; }

        public RESTResult<TestSuite> Results { get; set; } = new RESTResult<TestSuite>();

        public async Task<RESTResult<TestSuite>> GetList(GetTestSuitesEventArgs args)
        {
            using (HttpClient client = new HttpClient())
            {
                InitializeHttpClient(args.Organization, client);

                // TODO(crhodes)
                // Update Uri  Use args for parameters.
                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"testplan/plans/{args.TestPlan.id}/suites"
                    + "?api-version=6.1-preview.1";

                //RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    //RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    //JObject o = JObject.Parse(outJson);

                    TestSuitesRoot resultRoot = JsonConvert.DeserializeObject<TestSuitesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<TestSuite>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }
            }

            return Results;
        }

        public static void InitializeHttpClient(Organization collection, HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //var username = @"Christopher.Rhodes@bd.com";
            //var password = @"HappyH0jnacki08";

            //string base64PAT = Convert.ToBase64String(
            //        ASCIIEncoding.ASCII.GetBytes($"{username}:{password}"));
            string base64PAT = Convert.ToBase64String(
                    ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", collection.PAT)));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64PAT);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("NTLM");
        }

        public class Lastupdatedby
        {
            public string displayName { get; set; }
            public string url { get; set; }
            public _Links _links { get; set; }
            public string id { get; set; }
            public string uniqueName { get; set; }
            public string imageUrl { get; set; }
            public string descriptor { get; set; }
        }

        public class _Links
        {
            public Avatar avatar { get; set; }
        }

        public class Avatar
        {
            public string href { get; set; }
        }

        public class Plan
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class _Links1
        {
            public _Self _self { get; set; }
            public Testcases testCases { get; set; }
            public Testpoints testPoints { get; set; }
        }

        public class _Self
        {
            public string href { get; set; }
        }

        public class Testcases
        {
            public string href { get; set; }
        }

        public class Testpoints
        {
            public string href { get; set; }
        }

        public class Defaultconfiguration
        {
            public int id { get; set; }
            public string name { get; set; }
        }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class TestSuite

}
