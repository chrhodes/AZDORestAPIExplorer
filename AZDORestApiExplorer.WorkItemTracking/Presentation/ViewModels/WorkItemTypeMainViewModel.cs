using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Core.Events.WorkItemTracking;
using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.WorkItemTracking;
using AZDORestApiExplorer.Presentation.ViewModels;
using AZDORestApiExplorer.WorkItemTracking.Core.Events;


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Services;
using VNC.HttpHelper;

namespace AZDORestApiExplorer.WorkItemTracking.Presentation.ViewModels
{
    public class WorkItemTypeMainViewModel : GridViewModelBase, IWorkItemTypeMainViewModel
    {

        #region Constructors, Initialization, and Load

        public WorkItemTypeMainViewModel(
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

            EventAggregator.GetEvent<GetWorkItemTypesWITEvent>().Subscribe(GetWorkItemTypes);

            this.Results.PropertyChanged += PublishSelectionChanged;

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<WorkItemType> Results { get; set; } = new RESTResult<WorkItemType>();

        public ObservableCollection<Action> Transitions { get; set; } = new ObservableCollection<Action>();

        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private async void GetWorkItemTypes(GetWorkItemTypesWITEventArgs args)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Helpers.InitializeHttpClient(args.Organization, client);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + "wit/workitemtypes"
                        + "?api-version=4.1";

                    RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(outJson);

                        WorkItemTypesRoot resultRoot = JsonConvert.DeserializeObject<WorkItemTypesRoot>(outJson);

                        Results.ResultItems = new ObservableCollection<WorkItemType>(resultRoot.value);

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

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(RequestResponseExchange);
        }

        private void PublishSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<SelectedWorkItemTypeWITChangedEvent>().Publish(Results.SelectedItem);

            if (Results.SelectedItem != null)
            {
                var transitions = Results.SelectedItem.transitions.ToString();

                //Rootobject output = JsonConvert.DeserializeObject<Rootobject>(transitions);

                ////JArray ja = JArray.Parse(transitions);

                //JObject jo = JObject.Parse(transitions);

                Transitions.Clear();

                foreach (var item in JObject.Parse(transitions))
                {
                    Action action = new Action();

                    Action.ActionTarget[] actrgt = JsonConvert.DeserializeObject<Action.ActionTarget[]>(item.Value.ToString());

                    action.Name = item.Key;
                    action.Target = actrgt;

                    //var v1 = JArray.Parse(item.Value.ToString());

                    Transitions.Add(action);
                }
            }

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

    }
}
