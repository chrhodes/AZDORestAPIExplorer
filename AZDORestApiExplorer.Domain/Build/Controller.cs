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
        public class GetControllersEvent : PubSubEvent<GetControllersEventArgs> { }

        public class GetControllersEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            // public Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedControllerChangedEvent : PubSubEvent<Controller> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Controller

    public class ControllersRoot
    {
        public int count { get; set; }
        public Controller[] value { get; set; }
    }

    public class Controller
    {
            public string uri { get; set; }
            public DateTime createdDate { get; set; }
            public DateTime updatedDate { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
            public string description { get; set; }

        public RESTResult<Controller> Results { get; set; } = new RESTResult<Controller>();

        public async Task<RESTResult<Controller>> GetList(GetControllersEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Controller)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/_apis/"
                    + $"build/controllers?"
                    + "api-version=6.1-preview.2";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    ControllersRoot resultRoot = JsonConvert.DeserializeObject<ControllersRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Controller>(resultRoot.value);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(Controller)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
