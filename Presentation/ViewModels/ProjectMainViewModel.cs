using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ProjectMainViewModel : EventViewModelBase, IProjectMainViewModel, IInstanceCountVM
    {

        #region Constructors, Initialization, and Load

        public ProjectMainViewModel(
            //IProjectNavigationViewModel ProjectNavigationViewModel,
            //Func<IProjectDetailViewModel> ProjectDetailViewModelCreator,
            //Func<IDetailViewModel> DetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            //InstanceCountVM++;

            //_ProjectDetailViewModelCreator = ProjectDetailViewModelCreator;
            //_DetailViewModelCreator = DetailViewModelCreator;

            //NavigationViewModel = ProjectNavigationViewModel;

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

            //SelectedCollection = new CollectionDetails();
            ////var foo = AvailableCollections.First().Key;
            //CB1.SelectedItem = AvailableCollections.First();

            EventAggregator.GetEvent<GetProjectsEvent>().Subscribe(GetProjects);

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties


        public RESTResult<Domain.Project> Projects { get; set; } = new RESTResult<Domain.Project>();

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

        private Func<IProjectDetailViewModel> _ProjectDetailViewModelCreator;
        private Func<IDetailViewModel> _DetailViewModelCreator;

        private IDetailViewModel _selectedDetailViewModel;

        public ICommand CreateNewDetailCommand { get; }

        public ICommand OpenSingleDetailViewCommand { get; }

        // N.B. This is public so View.Xaml can bind to it.
        public IProjectNavigationViewModel NavigationViewModel { get; }

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
                // We can check if different and skip notificaiton,
                // however, raisign the event will cause the tab to be selected
                // which will draw user to tab if another is selected.
                OnPropertyChanged();
            }
        }

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
            Int64 startTicks = Log.EVENT_HANDLER($"(ProjectMainViewModel) Enter Id:({args.Id}(", Common.LOG_APPNAME);

            var detailViewModel = DetailViewModels
                    .SingleOrDefault(vm => vm.Id == args.Id
                    && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel == null)
            {
                switch (args.ViewModelName)
                {
                    case nameof(ProjectDetailViewModel):
                        detailViewModel = (IDetailViewModel)_ProjectDetailViewModelCreator();
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

            Log.VIEWMODEL("(ProjectMainViewModel) Exit", Common.LOG_APPNAME, startTicks);
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


        public async void GetProjects(CollectionDetails collection)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Helpers.InitializeHttpClient(collection, client);

                    RequestUri = $"{collection.Uri}/_apis/projects?api-version=6.1-preview.4";

                    RequestResponseInfo exchange = InitializeExchange(client, RequestUri);

                    using (HttpResponseMessage response = await client.GetAsync(RequestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();
                        sb.Append(outJson);

                        ProjectsRoot resultRoot = JsonConvert.DeserializeObject<ProjectsRoot>(outJson);

                        //Projects = new ObservableCollection<Project>(resultRoot.value);
                        Projects.ResultItems = new ObservableCollection<Project>(resultRoot.value);

                        //JObject o = JObject.Parse(outJson);
                        //foreach (var p in o)
                        //{
                        //    var foo = p.Key;

                        //    if (foo == "count")
                        //    {
                        //        Debug.WriteLine(p.Value);
                        //    }

                        //    if (foo == "value")
                        //    {
                        //        foreach (var t in p.Value)
                        //        {
                        //            Debug.WriteLine(t.ToString());
                        //        }

                        //    }
                        //    var bar = p.Value;
                        //}

                        //dynamic jsonArray = JArray.Parse(outJson);

                        //dynamic json = JValue.Parse(outJson);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        while (hasContinuationToken)
                        {
                            RequestResponseInfo exchange2 = new RequestResponseInfo();

                            string continueToken = continuationHeaders.First();

                            string requestUri2 = $"{collection.Uri}/_apis/projects?api-version=6.1-preview.4&continuationToken={continueToken}";

                            exchange2.Uri = requestUri2;
                            exchange2.RequestHeadersX.AddRange(client.DefaultRequestHeaders);

                            using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                            {
                                //ResponseHeaders.AddRange(response2.Headers);

                                exchange2.Response = response2;
                                exchange2.ResponseHeadersX.AddRange(response2.Headers);

                                RequestResponseExchange.Add(exchange2);

                                response2.EnsureSuccessStatusCode();
                                string outJson2 = await response2.Content.ReadAsStringAsync();
                                sb.Append(outJson2);

                                JObject oC = JObject.Parse(outJson2);

                                //Rootobject projectsC = JsonConvert.DeserializeObject<Rootobject>(outJson2);
                                //var projectsarrayC = projectsC.value;

                                ProjectsRoot projects2C = JsonConvert.DeserializeObject<ProjectsRoot>(outJson2);
                                //var projectsarray2C = projects2C.value;

                                //Projects.AddRange(projectsarray2C);
                                Projects.ResultItems.AddRange(projects2C.value);

                                hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                            }
                        }

                        jsonResult = sb.ToString();

                        //ProjectsCount = Projects.Count();
                        Projects.Count = Projects.ResultItems.Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            RequestResponseExchangeCount = RequestResponseExchange.Count();

            //return jsonResult;
        }

        #endregion

        #region Public Methods

        public async Task LoadAsync()
        {
            Int64 startTicks = Log.VIEWMODEL("ProjectMainViewModel) Enter", Common.LOG_APPNAME);

            //await NavigationViewModel.LoadAsync();

            Log.VIEWMODEL("ProjectMainViewModel) Exit", Common.LOG_APPNAME, startTicks);
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
