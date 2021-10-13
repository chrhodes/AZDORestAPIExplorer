using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Test.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Test
{
    namespace Events
    {
        public class GetTestPointsEvent : PubSubEvent<GetTestPointsEventArgs> { }

        public class GetTestPointsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Domain.Core.Project Project;

            public int TestPlan;

            public int TestSuite;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedTestPointChangedEvent : PubSubEvent<Value> { }
        //public class SelectedTestPointChangedEvent : PubSubEvent<TestPoint> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class TestPoint

    public class TestPointsRoot
    {
        public int count { get; set; }
        public Value[] value { get; set; }
    }


    //public class Rootobject
    //{
    //    public Value[] value { get; set; }
    //    public int count { get; set; }
    //}

    public class Value
    {
        public int id { get; set; }
        public string url { get; set; }
        public Assignedto assignedTo { get; set; }
        public bool automated { get; set; }
        public Configuration configuration { get; set; }
        public Lasttestrun lastTestRun { get; set; }
        public Lastresult lastResult { get; set; }
        public string outcome { get; set; }
        public string state { get; set; }
        public string lastResultState { get; set; }
        public Testcase testCase { get; set; }
        public Workitemproperty[] workItemProperties { get; set; }
        public Lastresultdetails lastResultDetails { get; set; }
        public string lastRunBuildNumber { get; set; }


        public class Assignedto
        {
            public string displayName { get; set; }
            public string id { get; set; }
        }

        public class Configuration
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Lasttestrun
        {
            public string id { get; set; }
        }

        public class Lastresult
        {
            public string id { get; set; }
        }

        public class Testcase
        {
            public string id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
            public string webUrl { get; set; }
        }

        public class Lastresultdetails
        {
            public int duration { get; set; }
            public DateTime dateCompleted { get; set; }
            public Runby runBy { get; set; }
        }

        public class Runby
        {
            public string displayName { get; set; }
            public string id { get; set; }
        }

        public class Workitemproperty
        {
            public Workitem workItem { get; set; }
        }

        public class Workitem
        {
            public string key { get; set; }
            public string value { get; set; }
        }

        public RESTResult<Value> Results { get; set; } = new RESTResult<Value>();

        //public async Task<RESTResult<TestPoint>> GetList(GetTestPointsEventArgs args)
        public async Task<RESTResult<Value>> GetList(GetTestPointsEventArgs args)
        {
            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var skip = 0;
                var top = 100;

                // TODO(crhodes)
                // Need to figure out how to handle skip and top.

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"test/Plans/{args.TestPlan}/Suites/{args.TestSuite}/points"
                    + "?api-version=6.1-preview.2";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    TestPointsRoot resultRoot = JsonConvert.DeserializeObject<TestPointsRoot>(outJson);

                    //Results.ResultItems = new ObservableCollection<TestPoint>(resultRoot.value);

                    Results.ResultItems = new ObservableCollection<Value>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }

                return Results;
            }
        }
    }


    //public class TestPoint
    //{
    //    //public RESTResult<TestPoint> Results { get; set; } = new RESTResult<TestPoint>();
    //    public RESTResult<Value> Results { get; set; } = new RESTResult<Value>();

    //    //public async Task<RESTResult<TestPoint>> GetList(GetTestPointsEventArgs args)
    //    public async Task<RESTResult<Value>> GetList(GetTestPointsEventArgs args)
    //    {
    //        using (HttpClient client = new HttpClient())
    //        {
    //            Results.InitializeHttpClient(client, args.Organization.PAT);

    //            var skip = 0;
    //            var top = 100;

    //            // TODO(crhodes)
    //            // Need to figure out how to handle skip and top.

    //            var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
    //                + $"test/Plans/{args.TestPlan}/Suites/{args.TestSuite}/points"
    //                + "?api-version=6.1-preview.2";

    //            var exchange = Results.InitializeExchange(client, requestUri);

    //            using (HttpResponseMessage response = await client.GetAsync(requestUri))
    //            {
    //                Results.RecordExchangeResponse(response, exchange);

    //                response.EnsureSuccessStatusCode();

    //                string outJson = await response.Content.ReadAsStringAsync();

    //                TestPointsRoot resultRoot = JsonConvert.DeserializeObject<TestPointsRoot>(outJson);

    //                //Results.ResultItems = new ObservableCollection<TestPoint>(resultRoot.value);

    //                Results.ResultItems = new ObservableCollection<Value>(resultRoot.value);

    //                IEnumerable<string> continuationHeaders = default;

    //                bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

    //                Results.Count = Results.ResultItems.Count;
    //            }

    //            return Results;
    //        }
    //    }
    //}
}
