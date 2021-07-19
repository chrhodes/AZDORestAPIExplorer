using System;

using AZDORestApiExplorer.Core.Events;

using AZDORestApiExplorer.Domain.Core;

using AZDORestApiExplorer.WorkItemTracking.Core.Events;

using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Services;
using VNC.WPF.Presentation.Dx.Views;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class CommandsPutViewModel : EventViewModelBase, IInstanceCountVM
    {

        #region Constructors, Initialization, and Load

        public CommandsPutViewModel(
            ICollectionMainViewModel collectionMainViewModel,
            IContextMainViewModel contextMainViewModel,
            IShellService shellService,
            IEventAggregator eventAggregator,
            DialogService dialogService) : base(eventAggregator, dialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _collectionMainViewModel = (CollectionMainViewModel)collectionMainViewModel;
            _contextMainViewModel = (ContextMainViewModel)contextMainViewModel;
            _shellService = shellService;

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            #region Core Category

            #endregion

            #region Accounts Category

            #endregion

            #region Artifacts Category

            #endregion

            #region Artifacts Package Types Category

            #endregion

            #region Audit Category

            #endregion

            #region Build Category

            #endregion

            #region Cloud Load Test Category

            #endregion

            #region Dashboard Category

            #endregion

            #region Distributed Task Category

            #endregion

            #region Extension Management Category

            #endregion

            #region Git Category

            #endregion

            #region Graph Category

            #endregion

            #region Identities Category

            #endregion

            #region Member Entitlement Management Category

            #endregion

            #region Notification Category

            #endregion

            #region Operations Category

            #endregion

            #region Permissions Report Category

            #endregion

            #region Pipelines Category

            #endregion

            #region Policy Category

            #endregion

            #region Profile Category

            #endregion

            #region Relese Category

            #endregion

            #region Search Category

            #endregion

            #region Security Category

            #endregion

            #region Service Endpoint Category

            #endregion

            #region Service Hooks Category

            #endregion

            #region Status Category

            #endregion

            #region Symbol Category

            #endregion

            #region Test Category

            #endregion

            #region Test Plan Category

            #endregion

            #region Test Results Category

            #endregion

            #region Tfvc Category

            #endregion

            #region Token Admin Category

            #endregion

            #region Work Category

            #endregion

            #region Work Item Tracking Category

            PutWorkItemTypeCommand = new DelegateCommand(PutWorkItemTypeExecute, PutWorkItemTypeCanExecute);

            #endregion

            #region Work Item Tracking Process Category

            #endregion

            EventAggregator.GetEvent<SelectedCollectionChangedEvent>().Subscribe(RaiseCollectionChanged);
            EventAggregator.GetEvent<Core.Events.Core.xSelectedProcessChangedEvent>().Subscribe(RaiseProcessChanged);
            EventAggregator.GetEvent<Core.Events.Core.xSelectedProjectChangedEvent>().Subscribe(RaiseProjectChanged);

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // NOTE(crhodes)
        // RaiseCanExecuteChanged for any Command that is dependent on Context.
        // N.B. Need to add to each Context Item for button to be enabled.

        // Add Commands that depend only on Organization Context here
        // Other commands that depend on more do not need to be added 
        // as the check is in all CanExecute methods

        private void RaiseCollectionChanged(SelectedCollectionChangedEventArgs args)
        {
            PutWorkItemTypeCommand.RaiseCanExecuteChanged();
        }

        private void RaiseProcessChanged(Domain.Core.Process process)
        {

            // Work Item Tracking

            // Work Item Tracking Process
        }

        private void RaiseProjectChanged(Project project)
        {

            // Work Item Tracking

            // Work Item Tracking Process

        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        IShellService _shellService;

        CollectionMainViewModel _collectionMainViewModel;
        ContextMainViewModel _contextMainViewModel;

        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods

        #region Commands

        // TODO(crhodes)
        // Decide if these need to be public, perhaps just private

        #region Accounts Category

        #endregion

        #region Artifacts Category

        #endregion

        #region Build Category

        #endregion

        #region Cloud Load Test Category

        #endregion

        #region Core

        #endregion

        #region Dashboard Category

        #endregion

        #region Distributed Task Category

        #endregion

        #region Extension Management Category

        #endregion

        #region Git Category

        #endregion

        #region Graph Category

        #endregion

        #region Identities Category

        #endregion

        #region Member Entitlement Category

        #endregion

        #region Notification Category

        #endregion

        #region Operations Category

        #endregion

        #region Permissions Report Category

        #endregion

        #region Pipelines Category

        #endregion

        #region Policy Category

        #endregion Region

        #region Profile Category

        #endregion

        #region Release Category

        #endregion

        #region Search Category

        #endregion

        #region Service Endpoint Category

        #endregion

        #region Service Hooks Category

        #endregion

        #region Status Category

        #endregion

        #region Symbol Category

        #endregion

        #region Test Category

        #endregion Region

        #region Tfvc Category

        #endregion

        #region Token Admin Category

        #endregion

        #region Work Category

        #endregion

        #region Work Item Tracking Category

        #region PostWorkItemType Command

        public DelegateCommand PutWorkItemTypeCommand { get; set; }
        public string PutWorkItemTypeContent { get; set; } = "PUT WorkItemType";
        public string PutWorkItemTypeToolTip { get; set; } = "PUT WorkItemType ToolTip";

        public static DxThemedWindowHost vncMVVM_V1_Modal_Host = null;

        public void PutWorkItemTypeExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            // TODO(crhodes)
            // What do we want to happen here?
            // Let's start with creating a new window and loading a UI that will serve two purposes
            // 1. Create new WorkItems
            // 2. Update existing WorkItem

            _shellService.ShowShell();


            //DxThemedWindowHost.DisplayUserControlInHost(ref vncMVVM_V1_Modal_Host,
            //    "MVVM View First (View is passed ViewModel) Modal",
            //    600, 450,
            //    //Common.DEFAULT_WINDOW_WIDTH, Common.DEFAULT_WINDOW_HEIGHT,
            //    DxThemedWindowHost.ShowWindowMode.Modal,  Container. new CreateWorkItemMain());

            //EventAggregator.GetEvent<Core.Events.WorkItemTracking.GetArtifactLinkTypesEvent>().Publish(
            //    new Core.Events.WorkItemTracking.GetArtifactLinkTypesEventArgs()
            //    {
            //        Organization = _collectionMainViewModel.SelectedCollection.Organization,
            //        Project = _contextMainViewModel.Context.SelectedProject
            //    });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        //public static DxThemedWindowHost vncMVVM_V1_Modal_Host = null;

        //private void btnVNC_MVVM_V1_Modal_Click(object sender, RibbonControlEventArgs e)
        //{
        //    long startTicks = Log.EVENT_HANDLER("Enter", Common.PROJECT_NAME);

        //    DxThemedWindowHost.DisplayUserControlInHost(ref vncMVVM_V1_Modal_Host,
        //    "MVVM View First (View is passed ViewModel) Modal",
        //    600, 450,
        //    //Common.DEFAULT_WINDOW_WIDTH, Common.DEFAULT_WINDOW_HEIGHT,
        //    DxThemedWindowHost.ShowWindowMode.Modal,
        //    new Cat(new CatViewModel()));

        //    Log.EVENT_HANDLER("Exit", Common.PROJECT_NAME, startTicks);
        //}

        //public static DxThemedWindowHost vncMVVM_VM1_Modal_Host = null;

        //private void btnVNC_MVVM_VM1_Modal_Click(object sender, RibbonControlEventArgs e)
        //{
        //    long startTicks = Log.EVENT_HANDLER("Enter", Common.PROJECT_NAME);

        //    DxThemedWindowHost.DisplayUserControlInHost(ref vncMVVM_VM1_Modal_Host,
        //    "MVVM ViewModel First (ViewModel is passed View) Modal",
        //    800, 600,
        //    //Common.DEFAULT_WINDOW_WIDTH, Common.DEFAULT_WINDOW_HEIGHT,
        //    DxThemedWindowHost.ShowWindowMode.Modal,
        //    new CatViewModel(new Cat()));

        //    Log.EVENT_HANDLER("Exit", Common.PROJECT_NAME, startTicks);
        //}

        public bool PutWorkItemTypeCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #endregion

        #region Work Item Tracking Process Category

        #endregion

        #region Work Item Tracking Process Template Category

        #endregion

        #endregion

        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods


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
