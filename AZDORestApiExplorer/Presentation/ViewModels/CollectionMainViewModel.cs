using System;
using System.Collections.ObjectModel;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class CollectionMainViewModel : EventViewModelBase, IInstanceCountVM //, ICollectionMainViewModel
    {

        #region Constructors, Initialization, and Load

        public CollectionMainViewModel(
            IEventAggregator eventAggregator,
            DialogService dialogService) : base(eventAggregator, dialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }
        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            InstanceCountVM++;
            LoadCollections();

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }


        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        private string _title = "GoGoGo";

        private static string _PAT_BD_STS_PROD = "";

        private static string _PAT_BD_STS_QA2 = "";

        private static string _PAT_BDTechnologySolutions = "";

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
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<SelectedCollectionChangedEvent>().Publish(new SelectedCollectionChangedEventArgs()
            {
                Collection = SelectedCollection
            });

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
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

        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private void LoadCollections()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            // TODO(crhodes)
            // Load this from App.Config

            AvailableCollections.Add(
                new AvailableCollection
                {
                    Name = "BD_STS_PROD",
                    Organization = new Organization
                    {
                        Uri = @"https://dev.azure.com/BD-STS-PROD",
                        PAT = _PAT_BD_STS_PROD
                    }
                });

            AvailableCollections.Add(
                new AvailableCollection
                {
                    Name = "BD_STS_QA2",
                    Organization = new Organization
                    {
                        Uri = @"https://dev.azure.com/BD-STS-QA2",
                        PAT = _PAT_BD_STS_QA2
                    }
                });

            AvailableCollections.Add(
                new AvailableCollection
                {
                    Name = "BDTechnologySolutions",
                    Organization = new Organization
                    {
                        Uri = @"https://dev.azure.com/BDTechnologySolutions",
                        PAT = _PAT_BDTechnologySolutions
                    }
                });

            AvailableCollections.Add(
                new AvailableCollection
                {
                    Name = "VNC-Development",
                    Organization = new Organization
                    {
                        Uri = @"https://dev.azure.com/VNC-Development",
                        PAT = _PAT_VNC_Development
                    }
                });

            //AvailableCollections.Add(
            //    new AvailableCollection
            //    {
            //        Name = "VNC-Development Limited",
            //        Details = new Organization
            //        {
            //            Uri = @"https://dev.azure.com/VNC-Development",
            //            PAT = _PAT_VNC_DevelopmentLimited
            //        }
            //    });

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
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
