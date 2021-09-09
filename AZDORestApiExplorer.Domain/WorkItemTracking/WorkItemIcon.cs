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
        public class GetWorkItemIconsEvent : PubSubEvent<GetWorkItemIconsEventArgs> { }

        public class GetWorkItemIconsEventArgs
        {
            public Organization Organization;

            // public Process Process;

            // public WorkItemType WorkItemType;
        }

        public class SelectedWorkItemIconChangedEvent : PubSubEvent<WorkItemIcon> { }
    }

    public class WorkItemIconsRoot
    {
        public int count { get; set; }
        public WorkItemIcon[] value { get; set; }
    }

    public class WorkItemIcon
    {
        public string id { get; set; }
        public string url { get; set; }

        public RESTResult<WorkItemIcon> Results { get; set; } = new RESTResult<WorkItemIcon>();

        public async Task<RESTResult<WorkItemIcon>> GetList(GetWorkItemIconsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Process)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/_apis/"
                    + "wit/workitemicons"
                    + "?api-version=4.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    WorkItemIconsRoot resultRoot = JsonConvert.DeserializeObject<WorkItemIconsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<WorkItemIcon>(resultRoot.value);

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
