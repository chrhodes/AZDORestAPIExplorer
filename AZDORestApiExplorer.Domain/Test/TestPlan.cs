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

using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Test
{
    namespace Events
    {
        public class GetTestPlansEvent : PubSubEvent<GetTestPlansEventArgs> { }

        public class GetTestPlansEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Domain.Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedTestPlanChangedEvent : PubSubEvent<TestPlan> { }
    }

    public class TestPlan
    {
        public _Links _links { get; set; }
        public string areaPath { get; set; }
        public int buildId { get; set; }
        public int id { get; set; }
        public string iteration { get; set; }
        public string name { get; set; }
        public object owner { get; set; }
        public Project project { get; set; }

        public int revision { get; set; }
        public Rootsuite rootSuite { get; set; }
        public string state { get; set; }

        public RESTResult<TestPlan> Results { get; set; } = new RESTResult<TestPlan>();

        public async Task<RESTResult<TestPlan>> GetList(GetTestPlansEventArgs args)
        {
            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"testplan/plans"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    TestPlansRoot resultRoot = JsonConvert.DeserializeObject<TestPlansRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<TestPlan>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    while (hasContinuationToken)
                    {
                        string continueToken = continuationHeaders.First();

                        string requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                            + "testplan/plans?"
                            + $"continuationToken={continueToken}"
                            + "&api-version=6.1-preview.1";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            TestPlansRoot resultRootC = JsonConvert.DeserializeObject<TestPlansRoot>(outJson2);

                            Results.ResultItems.AddRange(resultRootC.value);

                            hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                        }
                    }

                    Results.Count = Results.ResultItems.Count;
                }
            }

            return Results;
        }

        public class _Links
        {
            public _Self _self { get; set; }
            public Build build { get; set; }
            public Clienturl clientUrl { get; set; }
            public Rootsuite1 rootSuite { get; set; }
        }

        public class _Self
        {
            public string href { get; set; }
        }

        public class Build
        {
            public string href { get; set; }
        }

        public class Clienturl
        {
            public string href { get; set; }
        }

        public class Rootsuite
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Rootsuite1
        {
            public string href { get; set; }
        }
    }

    public class TestPlansRoot
    {
        public int count { get; set; }
        public TestPlan[] value { get; set; }
    }
}