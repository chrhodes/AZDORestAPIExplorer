using System;

using AZDORestApiExplorer.Core.Events;

using AZDORestApiExplorer.Domain.Core;

using AZDORestApiExplorer.WorkItemTracking.Core.Events;

using Prism.Commands;
using Prism.Events;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class PutCommandsViewModel : EventViewModelBase
    {

        #region Constructors, Initialization, and Load

        public PutCommandsViewModel(
            ICollectionMainViewModel collectionMainViewModel,
            ContextMainViewModel contextMainViewModel,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            _collectionMainViewModel = (CollectionMainViewModel)collectionMainViewModel;
            _contextMainViewModel = (ContextMainViewModel)contextMainViewModel;

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

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

            PostWorkItemTypeCommand = new DelegateCommand(PostWorkItemTypeExecute, PostWorkItemTypeCanExecute);

            #endregion

            #region Work Item Tracking Process Category

            #endregion

            EventAggregator.GetEvent<SelectedCollectionChangedEvent>().Subscribe(RaiseCollectionChanged);
            EventAggregator.GetEvent<Core.Events.Core.SelectedProcessChangedEvent>().Subscribe(RaiseProcessChanged);
            EventAggregator.GetEvent<Core.Events.Core.SelectedProjectChangedEvent>().Subscribe(RaiseProjectChanged);

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        // NOTE(crhodes)
        // RaiseCanExecuteChanged for any Command that is dependent on Context.
        // N.B. Need to add to each Context Item for button to be enabled.

        // Add Commands that only depend on Organization Context here
        // Other commands that depend on more do not need to be added 
        // as the check is in all CanExecute methods

        private void RaiseCollectionChanged()
        {

            // Work Item Tracking
            PostWorkItemTypeCommand.RaiseCanExecuteChanged();


            // Work Item Tracking Process

        }

        private void RaiseProcessChanged(Domain.Core.Process process)
        {

        }

        private void RaiseProjectChanged(Project project)
        {
            // Work Item Tracking

            PostWorkItemTypeCommand.RaiseCanExecuteChanged();

        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

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

        #region GetArtifactLinkTypes Command

        public DelegateCommand PostWorkItemTypeCommand { get; set; }
        public string PostWorkItemTypeContent { get; set; } = "GetArtifactLinkTypes";
        public string GetArtifactLinkTypesToolTip { get; set; } = "GetArtifactLinkTypes ToolTip";

        public void PostWorkItemTypeExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            // TODO(crhodes)
            // What do we want to happen here?
            // Let's start with creating a new window and loading a UI that will serve two purposes
            // 1. Create new WorkItems
            // 2. Update existing WorkItem

            EventAggregator.GetEvent<Core.Events.WorkItemTracking.GetArtifactLinkTypesEvent>().Publish(
                new Core.Events.WorkItemTracking.GetArtifactLinkTypesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool PostWorkItemTypeCanExecute()
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

    }
}
