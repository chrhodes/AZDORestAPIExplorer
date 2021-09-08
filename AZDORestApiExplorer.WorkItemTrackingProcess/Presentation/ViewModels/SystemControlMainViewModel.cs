using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess;
using AZDORestApiExplorer.Presentation.ViewModels;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Net;
using VNC.Core.Services;

namespace AZDORestApiExplorer.WorkItemTrackingProcess.Presentation.ViewModels
{
    public class SystemControlMainViewModel : GridViewModelBase, IInstanceCountVM
    {

        #region Constructors, Initialization, and Load

        public SystemControlMainViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService) : base(eventAggregator, dialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetSystemControlsEvent>().Subscribe(GetSystemControls);

            this.Results.PropertyChanged += PublishSelectionChanged;

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<SystemControl> Results { get; set; } = new RESTResult<SystemControl>();

        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private async void GetSystemControls(GetSystemControlsEventArgs args)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Results.InitializeHttpClient(client, args.Organization.PAT);

                    var requestUri = $"{args.Organization.Uri}/_apis"
                        + $"/work/processes/{args.Process.id}"
                        + $"/workItemTypes/{args.WorkItemType.referenceName}"
                        + "/layout/systemcontrols"
                        + "?api-version=6.1-preview.1";

                    var exchange = Results.InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        Results.RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(outJson);

                        SystemControlsRoot resultRoot = JsonConvert.DeserializeObject<SystemControlsRoot>(outJson);

                        Results.ResultItems = new ObservableCollection<SystemControl>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        Results.Count = Results.ResultItems.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, Common.LOG_CATEGORY);
                ExceptionDialogService.DisplayExceptionDialog(DialogService, ex);
            }

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(Results.RequestResponseExchange);
        }

        private void PublishSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<SelectedSystemControlChangedEvent>().Publish(Results.SelectedItem);

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region IInstanceCount

        private static int _instanceCountVM;

        public int InstanceCountVM
        {
            get => _instanceCountVM;
            set => _instanceCountVM = value;
        }

        #endregion

    }
}
