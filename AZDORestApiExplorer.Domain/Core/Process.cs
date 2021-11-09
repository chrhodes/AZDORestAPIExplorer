using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Core
{
    namespace Events
    {
        public class GetProcessesEvent : PubSubEvent<GetProcessesEventArgs> { }

        public class GetProcessesEventArgs
        {
            public Organization Organization;
        }

        public class SelectedProcessChangedEvent : PubSubEvent<Process> { }
    }

    public class ProcessesRoot
    {
        public int count { get; set; }
        public Process[] value { get; set; }
    }

    public class Process //: HTTPExchangeBase
    {
        //private static IEventAggregator _eventAggregator;
        //private static IDialogService _dialogService;

        //public Process() : base(_eventAggregator, _dialogService)
        //{
        //    var i = 0;
        //}

        //public Process(IEventAggregator eventAggregator,
        //    IDialogService dialogService) : base(eventAggregator, dialogService)
        //{
        //    var i = 0;
        //    _eventAggregator = eventAggregator;
        //    _dialogService = dialogService;
        //}

        public string id { get; set; }
        public string description { get; set; }
        public bool isDefault { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string name { get; set; }

        public RESTResult<Process> Results { get; set; } = new RESTResult<Process>();

        public async Task<RESTResult<Process>> GetList(GetProcessesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Process)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                //GET https://dev.azure.com/{organization}/_apis/process/processes?api-version=6.1-preview.1
                //GET https://dev.azure.com/{organization}/_apis/process/processes/{processId}?api-version=6.1-preview.1

                var requestUri = $"{args.Organization.Uri}/_apis/"
                    + "process/processes?"
                    + "api-version=6.0-preview.1";

               var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    ProcessesRoot resultRoot = JsonConvert.DeserializeObject<ProcessesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Process>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count();
                }

                Log.DOMAIN("Exit(Process)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
