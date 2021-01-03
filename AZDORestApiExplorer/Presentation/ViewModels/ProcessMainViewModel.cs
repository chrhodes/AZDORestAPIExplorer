using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Core.Events.Core;
using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Core;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;

using VNC;
using VNC.Core.Services;
using VNC.HttpHelper;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class ProcessMainViewModel : HTTPExchangeBase, IProcessMainViewModel
    {
        #region Constructors, Initialization, and Load

        public ProcessMainViewModel(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetProcessesEvent>().Subscribe(GetProcesses);

            this.Processes.PropertyChanged += PublishSelectedProcessChanged;

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void PublishSelectedProcessChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<SelectedProcessChangedEvent>().Publish(Processes.SelectedItem);

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<Process> Processes { get; set; } = new RESTResult<Process>();

        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods

        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private async void GetProcesses(GetProcessesEventArgs args)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Core.Helpers.InitializeHttpClient(args.Organization, client);

                    var requestUri = $"{args.Organization.Uri}/_apis/"
                        +"process/processes?"
                        + "api-version=6.0-preview.1";

                    RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(outJson);

                        ProcessesRoot resultRoot = JsonConvert.DeserializeObject<ProcessesRoot>(outJson);

                        Processes.ResultItems = new ObservableCollection<Process>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        Processes.Count = Processes.ResultItems.Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, Common.LOG_APPNAME);
                MessageDialogService.ShowInfoDialog($"Error ({ex})");
            }

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(RequestResponseExchange);
        }

        #endregion

    }
}
