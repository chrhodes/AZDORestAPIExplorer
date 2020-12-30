using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Commands;
using Prism.Events;

using VNC;
using VNC.Core.Events;
using VNC.Core.Mvvm;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class ProcessMainViewModel : EventViewModelBase, IProcessMainViewModel, IInstanceCountVM
    {

        #region Constructors, Initialization, and Load

        public ProcessMainViewModel(
            //IProcessNavigationViewModel ProcessNavigationViewModel,
            //Func<IProcessDetailViewModel> ProcessDetailViewModelCreator,
            //Func<IDetailViewModel> DetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            //InstanceCountVM++;

            //_ProcessDetailViewModelCreator = ProcessDetailViewModelCreator;
            //_DetailViewModelCreator = DetailViewModelCreator;

            //NavigationViewModel = ProcessNavigationViewModel;

            //DetailViewModels = new ObservableCollection<IDetailViewModel>();

            //EventAggregator.GetEvent<OpenDetailViewEvent>()
            //    .Subscribe(OnOpenDetailView);

            //EventAggregator.GetEvent<AfterDetailDeletedEvent>()
            //    .Subscribe(AfterDetailDeleted);

            //EventAggregator.GetEvent<AfterDetailClosedEvent>()
            //    .Subscribe(AfterDetailClosed);

            //CreateNewDetailCommand = new DelegateCommand<Type>(
            //    OnCreateNewDetailExecute);

            //OpenSingleDetailViewCommand = new DelegateCommand<Type>(
            //    OnOpenSingleDetailExecute);

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

        public RESTResult<Domain.Process> Processes { get; set; } = new RESTResult<Domain.Process>();

        private string _requestUri;

        public string RequestUri
        {
            get => _requestUri;
            set
            {
                if (_requestUri == value)
                    return;
                _requestUri = value;
                OnPropertyChanged();
            }
        }

        private Func<IProcessDetailViewModel> _ProcessDetailViewModelCreator;
        private Func<IDetailViewModel> _DetailViewModelCreator;

        private IDetailViewModel _selectedDetailViewModel;

        public ICommand CreateNewDetailCommand { get; }

        public ICommand OpenSingleDetailViewCommand { get; }

        // N.B. This is public so View.Xaml can bind to it.
        public IProcessNavigationViewModel NavigationViewModel { get; }

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }

        private int _nextNewItemId = 0;

        public IDetailViewModel SelectedDetailViewModel
        {
            get
            {
                return _selectedDetailViewModel;
            }
            set
            {
                _selectedDetailViewModel = value;
                // NOTE(crhodes)
                // We can check if different and skip notification,
                // however, raising the event will cause the tab to be selected
                // which will draw user to tab if another is selected.
                OnPropertyChanged();
            }
        }

        private HttpResponseMessage _response;
        public HttpResponseMessage Response
        {
            get => _response;
            set
            {
                if (_response == value)
                    return;
                _response = value;
                OnPropertyChanged();
            }
        }

        private int _requestResponseExchangeCount;

        public int RequestResponseExchangeCount
        {
            get => _requestResponseExchangeCount;
            set
            {
                if (_requestResponseExchangeCount == value)
                    return;
                _requestResponseExchangeCount = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<RequestResponseInfo> RequestResponseExchange { get; set; }
            = new ObservableCollection<RequestResponseInfo>();

        #endregion

        #region Event Handlers

        void OnOpenSingleDetailExecute(Type viewModelType)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

            OnOpenDetailView(
                new OpenDetailViewEventArgs
                {
                    Id = -1,
                    ViewModelName = viewModelType.Name
                });

            Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            OnOpenDetailView(
                new OpenDetailViewEventArgs
                {
                    Id = _nextNewItemId--,  // Ids in DB > 0.  Can now create multiple new items
                    ViewModelName = viewModelType.Name
                });

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            Int64 startTicks = Log.EVENT_HANDLER($"(ProcessMainViewModel) Enter Id:({args.Id}(", Common.LOG_APPNAME);

            var detailViewModel = DetailViewModels
                    .SingleOrDefault(vm => vm.Id == args.Id
                    && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel == null)
            {
                switch (args.ViewModelName)
                {
                    case nameof(ProcessDetailViewModel):
                        detailViewModel = (IDetailViewModel)_ProcessDetailViewModelCreator();
                        break;

                    //case nameof(MeetingDetailViewModel):
                    //    detailViewModel = _meetingDetailViewModelCreator();
                    //    break;

                    //case nameof(DetailViewModel):
                    //    detailViewModel = _DetailViewModelCreator();
                    //    break;

                    // Ignore event if we don't handle
                    default:
                        return;
                }

                // Verify item still exists (may have been deleted)

                try
                {
                    await detailViewModel.LoadAsync(args.Id);
                }
                catch (Exception ex)
                {
                    MessageDialogService.ShowInfoDialog($"Cannot load the entity ({ex})" +
                        "It may have been deleted by another user.  Updating Navigation");

                    await NavigationViewModel.LoadAsync();

                    return;
                }

                DetailViewModels.Add(detailViewModel);
            }

            SelectedDetailViewModel = detailViewModel;

            Log.VIEWMODEL("(ProcessMainViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

            RemoveDetailViewModel(args.Id, args.ViewModelName);

            Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        }

        void AfterDetailClosed(AfterDetailClosedEventArgs args)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

            RemoveDetailViewModel(args.Id, args.ViewModelName);

            Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void RemoveDetailViewModel(int id, string viewModelName)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            var detailViewModel = DetailViewModels
                .SingleOrDefault(vm => vm.Id == id
                && vm.GetType().Name == viewModelName);

            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Public Methods

        public async Task LoadAsync()
        {
            Int64 startTicks = Log.VIEWMODEL("ProcessMainViewModel) Enter", Common.LOG_APPNAME);

            //await NavigationViewModel.LoadAsync();

            Log.VIEWMODEL("ProcessMainViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        public async void GetProcesses(CollectionDetails collection)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Helpers.InitializeHttpClient(collection, client);

                    RequestUri = $"{collection.Uri}/_apis/work/processes?api-version=6.0-preview.2";

                    RequestResponseInfo exchange = InitializeExchange(client, RequestUri);

                    using (HttpResponseMessage response = await client.GetAsync(RequestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();
                        //sb.Append(outJson);

                        JObject o = JObject.Parse(outJson);

                        ProcessesRoot resultRoot = JsonConvert.DeserializeObject<ProcessesRoot>(outJson);
                        //Processes = new ObservableCollection<WpfAppCore.Domain.Process>(resultRoot.value);
                        //ProcessCount = Processes.Count();

                        Processes.ResultItems = new ObservableCollection<Domain.Process>(resultRoot.value);
                        jsonResult = outJson;

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        Processes.Count = Processes.ResultItems.Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            RequestResponseExchangeCount = RequestResponseExchange.Count();
            // return jsonResult;
        }

        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private RequestResponseInfo InitializeExchange(HttpClient client, string requestUri)
        {
            RequestResponseExchange.Clear();
            RequestResponseInfo exchange = new RequestResponseInfo();
            //RequestHeaders.Clear();
            //ResponseHeaders.Clear();

            //RequestHeaders.AddRange(client.DefaultRequestHeaders);

            exchange.Uri = requestUri;
            exchange.RequestHeadersX.AddRange(client.DefaultRequestHeaders);

            return exchange;
        }

        private void RecordExchangeResponse(HttpResponseMessage response, RequestResponseInfo exchange)
        {
            Response = response;

            var statusCode = response.StatusCode;
            //var statusCodeT = response.StatusCode.GetType();
            //var statusCodeTC = response.StatusCode.GetTypeCode();
            var statusCode2 = (int)response.StatusCode;

            //ResponseHeaders.AddRange(response.Headers);

            exchange.Response = response;
            exchange.ResponseStatusCode = statusCode2;

            exchange.ResponseContentHeaders.AddRange(response.Content.Headers);
            exchange.ResponseHeadersX.AddRange(response.Headers);

            RequestResponseExchange.Add(exchange);
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
