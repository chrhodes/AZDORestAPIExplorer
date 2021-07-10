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
using AZDORestApiExplorer.WorkItemTracking.Presentation.Views;

using DevExpress.XtraPrinting;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Services;
using VNC.HttpHelper;

namespace AZDORestApiExplorer.WorkItemTracking.Presentation.ViewModels
{
    public class FieldMainViewModel : GridViewModelBase, IFieldMainViewModel, IInstanceCountVM
    {

        #region Constructors, Initialization, and Load

        public FieldMainViewModel(
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

            EventAggregator.GetEvent<GetFieldsWITEvent>().Subscribe(GetFields);

            this.Results.PropertyChanged += PublishSelectionChanged;

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<Field> Results { get; set; } = new RESTResult<Field>();

        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        #region Commands

        #endregion

        private async void GetFields(GetFieldsWITEventArgs args)
        {
            try
            {
                string requestUri;

                using (HttpClient client = new HttpClient())
                {
                    Helpers.InitializeHttpClient(args.Organization, client);

                    if (args.Project is null)
                    {
                        requestUri = $"{args.Organization.Uri}/_apis"
                            + "/wit/fields"
                            + "?api-version=4.1";
                    }
                    else
                    {
                        requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis"
                            + "/wit/fields"
                            + "?api-version=4.1";
                    }

                    RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(outJson);

                        FieldsRoot resultRoot = JsonConvert.DeserializeObject<FieldsRoot>(outJson);

                        Results.ResultItems = new ObservableCollection<Field>(resultRoot.value);

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

            EventAggregator.GetEvent<SelectedFieldWITChangedEvent>().Publish(Results.SelectedItem);

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
