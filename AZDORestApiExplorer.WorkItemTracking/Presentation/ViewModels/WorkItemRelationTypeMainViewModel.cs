using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain.WorkItemTracking;
using AZDORestApiExplorer.Presentation.ViewModels;
using AZDORestApiExplorer.WorkItemTracking.Core.Events;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Net;
using VNC.Core.Services;

namespace AZDORestApiExplorer.WorkItemTracking.Presentation.ViewModels
{
    public class WorkItemRelationTypeMainViewModel : GridViewModelBase, IInstanceCountVM
    {

        #region Constructors, Initialization, and Load

        public WorkItemRelationTypeMainViewModel(
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

            EventAggregator.GetEvent<GetWorkItemRelationTypesEvent>().Subscribe(GetWorkItemRelationTypes);

            this.Results.PropertyChanged += PublishSelectionChanged;

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<WorkItemRelationType> Results { get; set; } = new RESTResult<WorkItemRelationType>();


        private WorkItemRelationType.Attributes _aStuff;

        public WorkItemRelationType.Attributes AStuff
        {
            get => _aStuff;
            set
            {
                if (_aStuff == value)
                    return;
                _aStuff = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Event Handlers

        public override void CollectionChanged(SelectedCollectionChangedEventArgs args)
        {
            OutputFileNameAndPath = $@"C:\temp\{args.Collection.Name}-WorkItemRelationTypes";
        }

        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private async void GetWorkItemRelationTypes(GetWorkItemRelationTypesEventArgs args)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Results.InitializeHttpClient(client, args.Organization.PAT);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/_apis/"
                        + "wit/workitemrelationtypes"
                        + "?api-version=4.1";

                    var exchange = Results.InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        Results.RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(outJson);

                        WorkItemRelationTypesRoot resultRoot = JsonConvert.DeserializeObject<WorkItemRelationTypesRoot>(outJson);

                        Results.ResultItems = new ObservableCollection<WorkItemRelationType>(resultRoot.value);

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

            if (Results.SelectedItem is null)
            {
                return;
            }

            EventAggregator.GetEvent<SelectedWorkItemRelationTypeChangedEvent>().Publish(Results.SelectedItem);
            AStuff = Results.SelectedItem.attributes;

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
