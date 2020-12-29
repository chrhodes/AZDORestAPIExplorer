using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain;

using Prism.Commands;
using Prism.Events;

using VNC;
using VNC.Core.Events;
using VNC.Core.Mvvm;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class CollectionMainViewModel : EventViewModelBase, ICollectionMainViewModel //, IInstanceCountVM
    {

        #region Constructors, Initialization, and Load

        public CollectionMainViewModel(
            //ICollectionNavigationViewModel CollectionNavigationViewModel,
            //Func<ICollectionDetailViewModel> CollectionDetailViewModelCreator,
            //Func<IDetailViewModel> DetailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            InitializeViewModel();

            //_CollectionDetailViewModelCreator = CollectionDetailViewModelCreator;
            //_DetailViewModelCreator = DetailViewModelCreator;

            //NavigationViewModel = CollectionNavigationViewModel;

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

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }
        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            LoadCollections();
            //SelectedCollection = new CollectionDetails();
            ////var foo = AvailableCollections.First().Key;
            //CB1.SelectedItem = AvailableCollections.First();

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME);
        }


        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        private string _title = "GoGoGo";
        private static string _URI_BD_STS_QA2 = @"https://dev.azure.com/BD-STS-QA2";
        private static string _URI_BD_STS_PROD = @"https://dev.azure.com/BD-STS-PROD";
        private static string _URI_VNC_Development = @"https://dev.azure.com/VNC-Development";

        private static string _teamProjectName = "TFS";

        private static string _PAT_BD_STS_PROD = "";

        private static string _PAT_BD_STS_QA2 = "";

        private static string _PAT_VNC_Development = "";

        public ObservableCollection<AvailableCollection> AvailableCollections { get; set; }
            = new ObservableCollection<AvailableCollection>();

        private AvailableCollection _SelectedCollection;

        public AvailableCollection SelectedCollection
        {
            get => _SelectedCollection;
            set
            {
                if (_SelectedCollection == value)
                    return;
                _SelectedCollection = value;
                OnPropertyChanged();
                PublishSelectedCollectionChanged();
            }
        }
        private void PublishSelectedCollectionChanged()
        {
            EventAggregator.GetEvent<SelectedCollectionChangedEvent>().Publish();
        }

        public string Title
        {
            get => _title;
            set
            {
                if (_title == value)
                    return;
                _title = value;
                OnPropertyChanged();
            }
        }
        
        private Func<ICollectionDetailViewModel> _CollectionDetailViewModelCreator;
        private Func<IDetailViewModel> _DetailViewModelCreator;

        private IDetailViewModel _selectedDetailViewModel;

        public ICommand CreateNewDetailCommand { get; }

        public ICommand OpenSingleDetailViewCommand { get; }

        // N.B. This is public so View.Xaml can bind to it.
        public ICollectionNavigationViewModel NavigationViewModel { get; }

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
            Int64 startTicks = Log.EVENT_HANDLER($"(CollectionMainViewModel) Enter Id:({args.Id}(", Common.LOG_APPNAME);

            var detailViewModel = DetailViewModels
                    .SingleOrDefault(vm => vm.Id == args.Id
                    && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel == null)
            {
                switch (args.ViewModelName)
                {
                    case nameof(CollectionDetailViewModel):
                        detailViewModel = (IDetailViewModel)_CollectionDetailViewModelCreator();
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

            Log.VIEWMODEL("(CollectionMainViewModel) Exit", Common.LOG_APPNAME, startTicks);
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
            Int64 startTicks = Log.VIEWMODEL("CollectionMainViewModel) Enter", Common.LOG_APPNAME);

            //await NavigationViewModel.LoadAsync();

            Log.VIEWMODEL("CollectionMainViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private void LoadCollections()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            AvailableCollections.Add(
                new AvailableCollection
                {
                    Name = "BD_STS_PROD",
                    Details = new CollectionDetails
                    {
                        Uri = @"https://dev.azure.com/BD-STS-PROD",
                        PAT = _PAT_BD_STS_PROD
                    }
                });

            AvailableCollections.Add(
                new AvailableCollection
                {
                    Name = "BD_STS_QA2",
                    Details = new CollectionDetails
                    {
                        Uri = @"https://dev.azure.com/BD-STS-QA2",
                        PAT = _PAT_BD_STS_QA2
                    }
                });

            AvailableCollections.Add(
                new AvailableCollection
                {
                    Name = "VNC-Development",
                    Details = new CollectionDetails
                    {
                        Uri = @"https://dev.azure.com/VNC-Development",
                        PAT = _PAT_VNC_Development
                    }
                });

            //AvailableCollections.Add(
            //    new AvailableCollection
            //    {
            //        Name = "VNC-Development Limited",
            //        Details = new CollectionDetails
            //        {
            //            Uri = @"https://dev.azure.com/VNC-Development",
            //            PAT = _PAT_VNC_DevelopmentLimited
            //        }
            //    });

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
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
