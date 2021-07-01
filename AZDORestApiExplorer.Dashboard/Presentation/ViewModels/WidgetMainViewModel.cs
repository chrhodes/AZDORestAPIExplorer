using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Dashboard;
using AZDORestApiExplorer.Presentation.ViewModels;

using Newtonsoft.Json.Linq;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Services;
using VNC.HttpHelper;

namespace AZDORestApiExplorer.Dashboard.Presentation.ViewModels
{
    public class WidgetMainViewModel : GridViewModelBase, IWidgetMainViewModel
    {

        #region Constructors, Initialization, and Load

        public WidgetMainViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService) : base(eventAggregator, dialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            // TODO(crhodes)
            // Save constructor parameters here

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            InstanceCountVM++;


            EventAggregator.GetEvent<GetWidgetsEvent>().Subscribe(GetWidgets);

            this.Results.PropertyChanged += PublishSelectionChanged;

            // TODO(crhodes)
            //

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<Widget> Results { get; set; } = new RESTResult<Widget>();


        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private async void GetWidgets(GetWidgetsEventArgs args)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Core.Helpers.InitializeHttpClient(args.Organization, client);

                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/{args.Team.id}/_apis/dashboard/dashboards/{args.Dashboard}?api-version=6.1-preview.1";

                    RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(outJson);

                        //WidgetsRoot resultRoot = JsonConvert.DeserializeObject<WidgetsRoot>(outJson);

                        //Widgets.ResultItems = new ObservableCollection<Domain.Widget>(resultRoot.value);

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

            EventAggregator.GetEvent<SelectedWidgetChangedEvent>().Publish(Results.SelectedItem);

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

    }
}
