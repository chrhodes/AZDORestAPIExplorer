using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Core.Events.Test;
using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Test;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Services;
using VNC.HttpHelper;

namespace AZDORestApiExplorer.Test.Presentation.ViewModels
{
    public class TestSuiteMainViewModel : HTTPExchangeBase, ITestSuiteMainViewModel, IInstanceCountVM
    {

        #region Constructors, Initialization, and Load

        public TestSuiteMainViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService) : base(eventAggregator, dialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetTestSuitesEvent>().Subscribe(GetTestSuites);

            this.Results.PropertyChanged += PublishSelectionChanged;

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<TestSuite> Results { get; set; } = new RESTResult<TestSuite>();

        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private async void GetTestSuites(GetTestSuitesEventArgs args)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Helpers.InitializeHttpClient(args.Organization, client);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + $"testplan/plans/{args.TestPlan.id}/suites"
                        + "?api-version=6.1-preview.1";

                    RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(outJson);

                        TestSuitesRoot resultRoot = JsonConvert.DeserializeObject<TestSuitesRoot>(outJson);

                        Results.ResultItems = new ObservableCollection<TestSuite>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        Results.Count = Results.ResultItems.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, Common.LOG_APPNAME);
                ExceptionDialogService.DisplayExceptionDialog(DialogService, ex);
            }

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(RequestResponseExchange);
        }

        private void PublishSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<SelectedTestSuiteChangedEvent>().Publish(Results.SelectedItem);

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
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
