using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core.Events;
using AZDORestApiExplorer.Domain.WorkItemTracking.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    namespace Events
    {
        public class GetClassificationNodesEvent : PubSubEvent<GetClassificationNodesEventArgs> { }

        public class GetClassificationNodesEventArgs
        {
            public Organization Organization;

            // public Process Process;

            public Domain.Core.Project Project;

            public string IDs = "";

            public int Depth = 1;

            // public Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedClassificationNodeChangedEvent : PubSubEvent<ClassificationNode> { }
    }

    public class ClassificationNodesRoot
    {
        public int count { get; set; }
        public ClassificationNode[] value { get; set; }
    }

    public class ClassificationNode
    {
        public int id { get; set; }
        public string identifier { get; set; }
        public string name { get; set; }
        public string structureType { get; set; }
        public bool hasChildren { get; set; }
        public string path { get; set; }
        public string url { get; set; }
        public Attributes attributes { get; set; }

        public class Attributes
        {
            public DateTime startDate { get; set; }
            public DateTime finishDate { get; set; }
        }

        public RESTResult<ClassificationNode> Results { get; set; } = new RESTResult<ClassificationNode>();

        public async Task<RESTResult<ClassificationNode>> GetList(GetClassificationNodesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Process)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                string requestUri = "";

                if (string.IsNullOrEmpty(args.IDs))
                {
                    requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + "wit/classificationnodes"
                        + "?api-version=6.1-preview.2";
                }
                else
                {
                    requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + "wit/classificationnodes?"
                        //+ $"ids={args.IDs}"
                        + $"depth={args.Depth}"
                        + "&api-version=6.1-preview.2";
                }


                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    ClassificationNodesRoot resultRoot = JsonConvert.DeserializeObject<ClassificationNodesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<ClassificationNode>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(Process)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
