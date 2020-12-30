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

using Prism.Commands;
using Prism.Events;

using VNC;
using VNC.Core.Events;
using VNC.Core.Mvvm;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class TeamMainViewModel : EventViewModelBase, ITeamMainViewModel, IInstanceCountVM
    {

        #region Constructors, Initialization, and Load

        public TeamMainViewModel(
            //ITeamNavigationViewModel TeamNavigationViewModel,
            //Func<ITeamDetailViewModel> TeamDetailViewModelCreator,
            //Func<IDetailViewModel> DetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            //NavigationViewModel = TeamNavigationViewModel;
            //_TeamDetailViewModelCreator = TeamDetailViewModelCreator;
            //_DetailViewModelCreator = DetailViewModelCreator;

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            InstanceCountVM++;

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

            EventAggregator.GetEvent<GetTeamsEvent>().Subscribe(GetTeams);

            this.Teams.PropertyChanged += PublishSelectedTeamChanged;

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void PublishSelectedTeamChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<SelectedTeamChangedEvent>().Publish(Teams.SelectedItem);

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<Domain.Team> Teams { get; set; } = new RESTResult<Domain.Team>();

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

        //private Func<ITeamDetailViewModel> _TeamDetailViewModelCreator;
        //private Func<IDetailViewModel> _DetailViewModelCreator;

        //private IDetailViewModel _selectedDetailViewModel;

        //public ICommand CreateNewDetailCommand { get; }

        //public ICommand OpenSingleDetailViewCommand { get; }

        //// N.B. This is public so View.Xaml can bind to it.
        //public ITeamNavigationViewModel NavigationViewModel { get; }

        //public ObservableCollection<IDetailViewModel> DetailViewModels { get; }

        //private int _nextNewItemId = 0;

        //public IDetailViewModel SelectedDetailViewModel
        //{
        //    get
        //    {
        //        return _selectedDetailViewModel;
        //    }
        //    set
        //    {
        //        _selectedDetailViewModel = value;
        //        // NOTE(crhodes)
        //        // We can check if different and skip notificaiton,
        //        // however, raisign the event will cause the tab to be selected
        //        // which will draw user to tab if another is selected.
        //        OnPropertyChanged();
        //    }
        //}

        #endregion

        #region Event Handlers

        //void OnOpenSingleDetailExecute(Type viewModelType)
        //{
        //    Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

        //    OnOpenDetailView(
        //        new OpenDetailViewEventArgs
        //        {
        //            Id = -1,
        //            ViewModelName = viewModelType.Name
        //        });

        //    Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private void OnCreateNewDetailExecute(Type viewModelType)
        //{
        //    Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

        //    OnOpenDetailView(
        //        new OpenDetailViewEventArgs
        //        {
        //            Id = _nextNewItemId--,  // Ids in DB > 0.  Can now create multiple new items
        //            ViewModelName = viewModelType.Name
        //        });

        //    Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        //{
        //    Int64 startTicks = Log.EVENT_HANDLER($"(TeamMainViewModel) Enter Id:({args.Id}(", Common.LOG_APPNAME);

        //    var detailViewModel = DetailViewModels
        //            .SingleOrDefault(vm => vm.Id == args.Id
        //            && vm.GetType().Name == args.ViewModelName);

        //    if (detailViewModel == null)
        //    {
        //        switch (args.ViewModelName)
        //        {
        //            case nameof(TeamDetailViewModel):
        //                detailViewModel = (IDetailViewModel)_TeamDetailViewModelCreator();
        //                break;

        //            //case nameof(MeetingDetailViewModel):
        //            //    detailViewModel = _meetingDetailViewModelCreator();
        //            //    break;

        //            case nameof(DetailViewModel):
        //                detailViewModel = _DetailViewModelCreator();
        //                break;

        //            // Ignore event if we don't handle
        //            default:
        //                return;
        //        }

        //        // Verify item still exists (may have been deleted)

        //        try
        //        {
        //            await detailViewModel.LoadAsync(args.Id);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageDialogService.ShowInfoDialog($"Cannot load the entity ({ex})" +
        //                "It may have been deleted by another user.  Updating Navigation");

        //            await NavigationViewModel.LoadAsync();

        //            return;
        //        }

        //        DetailViewModels.Add(detailViewModel);
        //    }

        //    SelectedDetailViewModel = detailViewModel;

        //    Log.VIEWMODEL("(TeamMainViewModel) Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        //{
        //    Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

        //    RemoveDetailViewModel(args.Id, args.ViewModelName);

        //    Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //void AfterDetailClosed(AfterDetailClosedEventArgs args)
        //{
        //    Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

        //    RemoveDetailViewModel(args.Id, args.ViewModelName);

        //    Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private void RemoveDetailViewModel(int id, string viewModelName)
        //{
        //    Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

        //    var detailViewModel = DetailViewModels
        //        .SingleOrDefault(vm => vm.Id == id
        //        && vm.GetType().Name == viewModelName);

        //    if (detailViewModel != null)
        //    {
        //        DetailViewModels.Remove(detailViewModel);
        //    }

        //    Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        //}

        #endregion

        #region Public Methods

        public async Task LoadAsync()
        {
            Int64 startTicks = Log.VIEWMODEL("TeamMainViewModel) Enter", Common.LOG_APPNAME);

            //await NavigationViewModel.LoadAsync();

            Log.VIEWMODEL("TeamMainViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods


        private async void GetTeams(CollectionDetails collection)
        {
            string jsonResult = default;
            StringBuilder sb = new StringBuilder();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Helpers.InitializeHttpClient(collection, client);

                    RequestUri = $"{collection.Uri}/_apis/teams?api-version=6.1-preview.3";

                    RequestResponseInfo exchange = InitializeExchange(client, RequestUri);

                    using (HttpResponseMessage response = await client.GetAsync(RequestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();
                        //sb.Append(outJson);

                        //JObject o = JObject.Parse(outJson);

                        TeamsRoot resultRoot = JsonConvert.DeserializeObject<TeamsRoot>(outJson);

                        Teams.ResultItems = new ObservableCollection<Domain.Team>(resultRoot.value);

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

                                //JObject oC = JObject.Parse(outJson2);

                                //Rootobject projectsC = JsonConvert.DeserializeObject<Rootobject>(outJson2);
                                //var projectsarrayC = projectsC.value;

                                TeamsRoot resultRootC = JsonConvert.DeserializeObject<TeamsRoot>(outJson2);
                                var resultArray2C = resultRootC.value;

                                Teams.ResultItems.AddRange(resultArray2C);

                                hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                            }
                        }

                        jsonResult = sb.ToString();

                        Teams.Count = Teams.ResultItems.Count();
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
