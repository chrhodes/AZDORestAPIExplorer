using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
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
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"testplan/plans/{args.TestPlan.id}/suites/{args.TestSuite.id}/testcase"
                    + "?api-version=6.1-preview.3";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    TestCasesRoot resultRoot = JsonConvert.DeserializeObject<TestCasesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<TestCase>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }
            }

            return Results;
        }

        public class Workitem
        {
            public int id { get; set; }
            public string name { get; set; }
            //public Workitemfield workItemFields { get; set; }
            //public List<Workitemfield> workItemFields { get; set; }
            //public List<string> workItemFields { get; set; }
            public Workitemfield[] workItemFields { get; set; }
        }

        public class Workitemfield
        {
            [JsonProperty("Microsoft.VSTS.TCM.Steps")]
            public string MicrosoftVSTSTCMSteps { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.ActivatedBy")]
            public string MicrosoftVSTSCommonActivatedBy { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.ActivatedDate")]
            public DateTime MicrosoftVSTSCommonActivatedDate { get; set; }

            [JsonProperty("Microsoft.VSTS.TCM.AutomationStatus")]
            public string MicrosoftVSTSTCMAutomationStatus { get; set; }

            [JsonProperty("Microsoft.VSTS.TCM.LocalDataSource")]
            public string MicrosoftVSTSTCMLocalDataSource { get; set; }

            [JsonProperty("System.Description")]
            public string SystemDescription { get; set; }

            [JsonProperty("System.State")]
            public string SystemState { get; set; }

            [JsonProperty("System.AssignedTo")]
            public string SystemAssignedTo { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.Priority")]
            public int MicrosoftVSTSCommonPriority { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.StateChangeDate")]
            public DateTime MicrosoftVSTSCommonStateChangeDate { get; set; }

            [JsonProperty("System.WorkItemType")]
            public string SystemWorkItemType { get; set; }

            [JsonProperty("System.Rev")]
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

}
