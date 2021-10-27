using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Build.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Build
{
    namespace Events
    {
        public class GetBuildsEvent : PubSubEvent<GetBuildsEventArgs> { }

        public class GetBuildsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedBuildChangedEvent : PubSubEvent<Build> { }
    }


    // Nest any additional classes inside class Build

    public class BuildsRoot
    {
        public int count { get; set; }
        public Build[] value { get; set; }
    }

    public class Build
    {

        //public class Value
        //{
        public _Links _links { get; set; }
        public Properties properties { get; set; }
        public object[] tags { get; set; }
        public object[] validationResults { get; set; }
        public Plan[] plans { get; set; }
        public Triggerinfo triggerInfo { get; set; }
        public int id { get; set; }
        public string buildNumber { get; set; }
        public string status { get; set; }
        public string result { get; set; }
        public DateTime queueTime { get; set; }
        public DateTime startTime { get; set; }
        public DateTime finishTime { get; set; }
        public string url { get; set; }
        public Definition definition { get; set; }
        public int buildNumberRevision { get; set; }
        public Project project { get; set; }
        public string uri { get; set; }
        public string sourceBranch { get; set; }
        public string sourceVersion { get; set; }
        public Queue queue { get; set; }
        public string priority { get; set; }
        public string reason { get; set; }
        public Requestedfor requestedFor { get; set; }
        public Requestedby requestedBy { get; set; }
        public DateTime lastChangedDate { get; set; }
        public Lastchangedby lastChangedBy { get; set; }
        public Orchestrationplan orchestrationPlan { get; set; }
        public Logs logs { get; set; }
        public Repository repository { get; set; }
        public bool retainedByRelease { get; set; }
        public object triggeredByBuild { get; set; }
        public string parameters { get; set; }
        //}

        public class _Links
        {
            public Self self { get; set; }
            public Web web { get; set; }
            public Sourceversiondisplayuri sourceVersionDisplayUri { get; set; }
            public Timeline timeline { get; set; }
            public Badge badge { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Web
        {
            public string href { get; set; }
        }

        public class Sourceversiondisplayuri
        {
            public string href { get; set; }
        }

        public class Timeline
        {
            public string href { get; set; }
        }

        public class Badge
        {
            public string href { get; set; }
        }

        public class Properties
        {
        }

        public class Triggerinfo
        {
            public string cisourceBranch { get; set; }
            public string cisourceSha { get; set; }
            public string cimessage { get; set; }
            public string citriggerRepository { get; set; }
            public string scheduleName { get; set; }
        }

        public class Definition
        {
            public object[] drafts { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
            public string uri { get; set; }
            public string path { get; set; }
            public string type { get; set; }
            public string queueStatus { get; set; }
            public int revision { get; set; }
            public Project project { get; set; }
        }

        public class Project
        {
            public string id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public string url { get; set; }
            public string state { get; set; }
            public int revision { get; set; }
            public string visibility { get; set; }
            public DateTime lastUpdateTime { get; set; }
        }

        //public class Project1
        //{
        //    public string id { get; set; }
        //    public string name { get; set; }
        //    public string description { get; set; }
        //    public string url { get; set; }
        //    public string state { get; set; }
        //    public int revision { get; set; }
        //    public string visibility { get; set; }
        //    public DateTime lastUpdateTime { get; set; }
        //}

        public class Queue
        {
            public int id { get; set; }
            public string name { get; set; }
            public Pool pool { get; set; }
        }

        public class Pool
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool isHosted { get; set; }
        }

        public class Requestedfor
        {
            public string displayName { get; set; }
            public string url { get; set; }
            public _Links1 _links { get; set; }
            public string id { get; set; }
            public string uniqueName { get; set; }
            public string imageUrl { get; set; }
            public string descriptor { get; set; }
        }

        public class _Links1
        {
            public Avatar avatar { get; set; }
        }

        public class Avatar
        {
            public string href { get; set; }
        }

        public class Requestedby
        {
            public string displayName { get; set; }
            public string url { get; set; }
            public _Links2 _links { get; set; }
            public string id { get; set; }
            public string uniqueName { get; set; }
            public string imageUrl { get; set; }
            public string descriptor { get; set; }
        }

        public class _Links2
        {
            public Avatar1 avatar { get; set; }
        }

        public class Avatar1
        {
            public string href { get; set; }
        }

        public class Lastchangedby
        {
            public string displayName { get; set; }
            public string url { get; set; }
            public _Links3 _links { get; set; }
            public string id { get; set; }
            public string uniqueName { get; set; }
            public string imageUrl { get; set; }
            public string descriptor { get; set; }
        }

        public class _Links3
        {
            public Avatar2 avatar { get; set; }
        }

        public class Avatar2
        {
            public string href { get; set; }
        }

        public class Orchestrationplan
        {
            public string planId { get; set; }
        }

        public class Logs
        {
            public int id { get; set; }
            public string type { get; set; }
            public string url { get; set; }
        }

        public class Repository
        {
            public string id { get; set; }
            public string type { get; set; }
            public string name { get; set; }
            public string url { get; set; }
            public object clean { get; set; }
            public bool checkoutSubmodules { get; set; }
        }

        public class Plan
        {
            public string planId { get; set; }
        }

        public RESTResult<Build> Results { get; set; } = new RESTResult<Build>();

        public async Task<RESTResult<Build>> GetList(GetBuildsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Build)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // TODO(crhodes)
                // Update Uri  Use args for parameters.
                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"build/builds?"
                    + "api-version=6.1-preview.7";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    BuildsRoot resultRoot = JsonConvert.DeserializeObject<BuildsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Build>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    while (hasContinuationToken)
                    {
                        string continueToken = continuationHeaders.First();

                        string requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                            + $"build/builds?"
                            + $"continuationToken={continueToken}"
                            + "&api-version=6.1-preview.7";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            BuildsRoot resultRootC = JsonConvert.DeserializeObject<BuildsRoot>(outJson2);

                            Results.ResultItems.AddRange(resultRootC.value);

                            hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                        }
                    }

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(Build)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }

    }
}
