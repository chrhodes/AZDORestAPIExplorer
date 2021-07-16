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
        public class GetTestCasesEvent : PubSubEvent<GetTestCasesEventArgs> { }

        public class GetTestCasesEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Domain.Core.Project Project;

            public Domain.Test.TestPlan TestPlan;

            public Domain.Test.TestSuite TestSuite;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedTestCaseChangedEvent : PubSubEvent<TestCase> { }
    }

    public class TestCasesRoot
    {
        public int count { get; set; }
        public TestCase[] value { get; set; }
    }

    public class TestCase
    {
        public TestPlan testPlan { get; set; }
        public Project project { get; set; }
        public TestSuite testSuite { get; set; }
        public Workitem workItem { get; set; }
        public Pointassignment[] pointAssignments { get; set; }
        public Links links { get; set; }
        public int order { get; set; }

        public RESTResult<TestCase> Results { get; set; } = new RESTResult<TestCase>();

        public async Task<RESTResult<TestCase>> GetList(GetTestCasesEventArgs args)
        {
            using (HttpClient client = new HttpClient())
            {
                InitializeHttpClient(args.Organization, client);

                // TODO(crhodes)
                // Update Uri  Use args for parameters.
                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"testplan/plans/{args.TestPlan.id}/suites/{args.TestSuite.id}/testcase"
                    + "?api-version=6.1-preview.3";

                //RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    //RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    //JObject o = JObject.Parse(outJson);

                    TestCasesRoot resultRoot = JsonConvert.DeserializeObject<TestCasesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<TestCase>(resultRoot.value);

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

        public class Workitem
        {
            public int id { get; set; }
            public string name { get; set; }
            public Workitemfield[] workItemFields { get; set; }
        }

        public class Workitemfield
        {
            public string MicrosoftVSTSTCMSteps { get; set; }
            public string MicrosoftVSTSCommonActivatedBy { get; set; }
            public DateTime MicrosoftVSTSCommonActivatedDate { get; set; }
            public string MicrosoftVSTSTCMAutomationStatus { get; set; }
            public string SystemDescription { get; set; }
            public string SystemState { get; set; }
            public string SystemAssignedTo { get; set; }
            public int MicrosoftVSTSCommonPriority { get; set; }
            public DateTime MicrosoftVSTSCommonStateChangeDate { get; set; }
            public string SystemWorkItemType { get; set; }
            public int SystemRev { get; set; }
        }

        public class Links
        {
            public object testPoints { get; set; }
            public object configuration { get; set; }
            public _Self _self { get; set; }
            public Sourceplan sourcePlan { get; set; }
            public Sourcesuite sourceSuite { get; set; }
            public Sourceproject sourceProject { get; set; }
        }

        public class _Self
        {
            public string href { get; set; }
        }

        public class Sourceplan
        {
            public string href { get; set; }
        }

        public class Sourcesuite
        {
            public string href { get; set; }
        }

        public class Sourceproject
        {
            public string href { get; set; }
        }

        public class Pointassignment
        {
            public int id { get; set; }
            public string configurationName { get; set; }
            public Tester tester { get; set; }
            public int configurationId { get; set; }
        }

        public class Tester
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
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class TestCase


}
