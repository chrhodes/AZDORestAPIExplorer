using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Build.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Build
{
    namespace Events
    {
        public class GetBuildInfoEvent : PubSubEvent<GetBuildInfoEventArgs> { }

        public class GetBuildInfoEventArgs
        {
            public Organization Organization;

            public Core.Project Project;

            public Build Build;
        }

        public class SelectedBuildInfoChangedEvent : PubSubEvent<BuildInfo> { }
    }

    public class BuildInfo
    {
            public _Links _links { get; set; }
            public Properties properties { get; set; }
            public object[] tags { get; set; }
            public object[] validationResults { get; set; }
            public Plan[] plans { get; set; }
            public Triggerinfo triggerInfo { get; set; }
            public int id { get; set; }
            public string buildNumber { get; set; }
            public string status { get; set; }
            public DateTime queueTime { get; set; }
            public DateTime startTime { get; set; }
            public string url { get; set; }
            public Definition definition { get; set; }
            public int buildNumberRevision { get; set; }
            public Project1 project { get; set; }
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

        public class Project1
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

        public RESTResult<BuildInfo> Results {
            get; 
            set; 
        } = new RESTResult<BuildInfo>();

        public async Task<RESTResult<BuildInfo>> GetList(GetBuildInfoEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(BuildBuild)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // GET https://dev.azure.com/{organization}/{project}/_apis/build/builds/{buildId}?api-version=6.1-preview.7

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"build/builds/{args.Build.id}?"
                    + "api-version=6.1-preview.7";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    // HACK(crhodes)
                    // There is only one item coming back, not a count and collection.
                    // Hack it into the collection.

                    //BuildBuilds resultRoot = JsonConvert.DeserializeObject<BuildBuilds>(outJson);

                    BuildInfo result = JsonConvert.DeserializeObject<BuildInfo>(outJson);

                    //Results.ResultItems = new ObservableCollection<BuildInfo>(resultRoot.value);

                    Results.ResultItem = result;
                    //Results.SelectedItem = result;

                    //Results.ResultItems = new ObservableCollection<BuildInfo>();

                    //Results.ResultItems.Add(result);

                    //Results.Count = Results.ResultItems.Count;
                    Results.Count = 1;
                }

                Log.DOMAIN("Exit(BuildBuild)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
