using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess;
using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;

using VNC;

using VNC.Core.Services;
using VNC.HttpHelper;

namespace AZDORestApiExplorer.WorkItemTrackingProcess.Presentation.ViewModels
{
    public class WorkItemTypeMainViewModel : HTTPExchangeBase, IWorkItemTypeMainViewModel
    {
        #region Constructors, Initialization, and Load

        public WorkItemTypeMainViewModel(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetWorkItemTypesWITPEvent>().Subscribe(GetWorkItemTypes);

            this.WorkItemTypes.PropertyChanged += PublishSelectionChanged;

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void PublishSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<SelectedWorkItemTypeWITPChangedEvent>().Publish(WorkItemTypes.SelectedItem);

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<WorkItemType> WorkItemTypes { get; set; } = new RESTResult<WorkItemType>();

        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private async void GetWorkItemTypes(GetWorkItemTypesWITPEventArgs args)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Helpers.InitializeHttpClient(args.Organization, client);

                    var requestUri = $"{args.Organization.Uri}/_apis/"
                        + $"/work/processes/{args.Process.id}"
                        + "/workitemtypes"
                        + "?api-version=6.1-preview.2";

                    RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(outJson);

                        WorkItemTypesRoot resultRoot = JsonConvert.DeserializeObject<WorkItemTypesRoot>(outJson);

                        WorkItemTypes.ResultItems = new ObservableCollection<WorkItemType>(resultRoot.value); ;
                        WorkItemTypes.Count = WorkItemTypes.ResultItems.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, Common.LOG_CATEGORY);
                ExceptionDialogService.DisplayExceptionDialog(DialogService, ex);
            }

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(RequestResponseExchange);

        }

        #endregion
    }
}
