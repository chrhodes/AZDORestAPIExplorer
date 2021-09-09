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
        public class GetStatesWITEvent : PubSubEvent<GetStatesWITEventArgs> { }

        public class GetStatesWITEventArgs
        {
            public Domain.Organization Organization;

            public Domain.Core.Project Project;

            public Domain.WorkItemTracking.WorkItemType WorkItemType;
        }

        public class SelectedStateWITChangedEvent : PubSubEvent<State> { }
    }

    public class StatesRoot
    {
        public int count { get; set; }
        public State[] value { get; set; }
    }

    public class State
    {
        public string name { get; set; }
        public string color { get; set; }
        public string category { get; set; }

        public RESTResult<State> Results { get; set; } = new RESTResult<State>();

        public async Task<RESTResult<State>> GetList(GetStatesWITEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Process)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"wit/workitemtypes/{args.WorkItemType.name}/states"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    StatesRoot resultRoot = JsonConvert.DeserializeObject<StatesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<State>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }
            }

            Log.DOMAIN("Exit(Process)", Common.LOG_CATEGORY, startTicks);

                return Results;
        }
    }
}
