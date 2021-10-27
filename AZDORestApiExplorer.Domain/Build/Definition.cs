using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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
        public class GetDefinitionsEvent : PubSubEvent<GetDefinitionsEventArgs> { }

        public class GetDefinitionsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedDefinitionChangedEvent : PubSubEvent<Definition> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Definition

    public class DefinitionsRoot
    {
        public int count { get; set; }
        public Definition[] value { get; set; }
    }

    public class Definition
    {


            public _Links _links { get; set; }
            public string quality { get; set; }
            public Authoredby authoredBy { get; set; }
            public object[] drafts { get; set; }
            public Queue queue { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
            public string uri { get; set; }
            public string path { get; set; }
            public string type { get; set; }
            public string queueStatus { get; set; }
            public int revision { get; set; }
            public DateTime createdDate { get; set; }
            public Project project { get; set; }


        public class _Links
        {
            public Self self { get; set; }
            public Web web { get; set; }
            public Editor editor { get; set; }
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

        public class Editor
        {
            public string href { get; set; }
        }

        public class Badge
        {
            public string href { get; set; }
        }

        public class Authoredby
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

        public class Queue
        {
            public _Links2 _links { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
            public Pool pool { get; set; }
        }

        public class _Links2
        {
            public Self1 self { get; set; }
        }

        public class Self1
        {
            public string href { get; set; }
        }

        public class Pool
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool isHosted { get; set; }
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

        public RESTResult<Definition> Results { get; set; } = new RESTResult<Definition>();

        public async Task<RESTResult<Definition>> GetList(GetDefinitionsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Definition)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"build/definitions?"
                    + "api-version=6.1-preview.7";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    DefinitionsRoot resultRoot = JsonConvert.DeserializeObject<DefinitionsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Definition>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    while (hasContinuationToken)
                    {
                        string continueToken = continuationHeaders.First();

                        string requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                            + $"build/definitions?"
                            + $"continuationToken={continueToken}"
                            + "&api-version=6.1-preview.7";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            DefinitionsRoot resultRootC = JsonConvert.DeserializeObject<DefinitionsRoot>(outJson2);

                            Results.ResultItems.AddRange(resultRootC.value);

                            hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                        }
                    }

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(Definition)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
