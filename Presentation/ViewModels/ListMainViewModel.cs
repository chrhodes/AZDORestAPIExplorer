using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;

using VNC;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class ListMainViewModel : HTTPExchangeBase, IListMainViewModel
    {

        #region Constructors, Initialization, and Load

        public ListMainViewModel(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            // TODO(crhodes)
            // Save constructor parameters here

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            InstanceCountVM++;

            EventAggregator.GetEvent<GetListsEvent>().Subscribe(GetLists);

            this.Lists.PropertyChanged += PublishSelectionChanged;

            // TODO(crhodes)
            //

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<Domain.List> Lists { get; set; } = new RESTResult<Domain.List>();


        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        // TODO(crhodes)
        // Put Request Handler Here - Check Parameters
        // Update GetListEventArgs as needed
        private async void GetLists(GetListsEventArgs args)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Helpers.InitializeHttpClient(args.CollectionDetails, client);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.CollectionDetails.Uri}/_apis/work/processes/lists?api-version=6.0-preview.1";

                    RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(outJson);

                        ListsRoot resultRoot = JsonConvert.DeserializeObject<ListsRoot>(outJson);

                        Lists.ResultItems = new ObservableCollection<Domain.List>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        Lists.Count = Lists.ResultItems.Count;
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

        private void PublishSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<SelectedListChangedEvent>().Publish(Lists.SelectedItem);

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
