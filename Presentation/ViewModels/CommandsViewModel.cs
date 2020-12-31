using System;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain;

using Prism.Commands;
using Prism.Events;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class CommandsViewModel : EventViewModelBase
    {

        #region Constructors, Initialization, and Load

        public CommandsViewModel(
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

            //SelectedCollection = new CollectionDetails();
            ////var foo = AvailableCollections.First().Key;
            //CB1.SelectedItem = AvailableCollections.First();

            GetProcessesCommand = new DelegateCommand(OnGetProcessesExecute, OnGetProcessesCanExecute);
            GetProjectsCommand = new DelegateCommand(OnGetProjectsExecute, OnGetProjectsCanExecute);
            GetTeamsCommand = new DelegateCommand(OnGetTeamsExecute, OnGetTeamsCanExecute);
            GetWorkItemTypesCommand = new DelegateCommand(OnGetWorkItemTypesExecute, OnGetWorkItemTypesCanExecute);
            GetStatesCommand = new DelegateCommand(OnGetStatesExecute, OnGetStatesCanExecute);
            GetFieldsCommand = new DelegateCommand(OnGetFieldsExecute, OnGetFieldsCanExecute);
            GetListsCommand = new DelegateCommand(OnGetListsExecute, OnGetListsCanExecute);

            EventAggregator.GetEvent<SelectedCollectionChangedEvent>().Subscribe(RaiseCollectionChanged);

            EventAggregator.GetEvent<SelectedProcessChangedEvent>().Subscribe(RaiseProcessChanged);
            EventAggregator.GetEvent<SelectedWorkItemTypeChangedEvent>().Subscribe(RaiseWorkItemTypeChanged);
            EventAggregator.GetEvent<SelectedProjectChangedEvent>().Subscribe(RaiseProjectChanged);
            EventAggregator.GetEvent<SelectedTeamChangedEvent>().Subscribe(RaiseTeamChanged);

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void RaiseCollectionChanged()
        {
            GetProjectsCommand.RaiseCanExecuteChanged();
            GetTeamsCommand.RaiseCanExecuteChanged();
            GetProcessesCommand.RaiseCanExecuteChanged();
        }

        private void RaiseProcessChanged(Process process)
        {
            GetWorkItemTypesCommand.RaiseCanExecuteChanged();
            GetListsCommand.RaiseCanExecuteChanged();
        }

        private void RaiseWorkItemTypeChanged(WorkItemType workItemType)
        {
            GetStatesCommand.RaiseCanExecuteChanged();
            GetFieldsCommand.RaiseCanExecuteChanged();
        }

        private void RaiseProjectChanged(Project project)
        {
            GetProjectsCommand.RaiseCanExecuteChanged();
        }

        private void RaiseTeamChanged(Team team)
        {
            GetTeamsCommand.RaiseCanExecuteChanged();
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


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods


        #endregion

        #region GetProcesses Command

        public DelegateCommand GetProcessesCommand { get; set; }
        public string GetProcessesContent { get; set; } = "GetProcesses";
        public string GetProcessesToolTip { get; set; } = "GetProcesses ToolTip";

        // Can get fancy and use Resources
        //public string GetProcessesContent { get; set; } = "ViewName_GetProcessesContent";
        //public string GetProcessesToolTip { get; set; } = "ViewName_GetProcessesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProcessesContent">GetProcesses</system:String>
        //    <system:String x:Key="ViewName_GetProcessesContentToolTip">GetProcesses ToolTip</system:String>  

        public void OnGetProcessesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);
            // TODO(crhodes)
            // Do something amazing.
            //Message = "Cool, you called GetProcesses";
            EventAggregator.GetEvent<GetProcessesEvent>().Publish(
                _collectionMainViewModel.SelectedCollection.Details);

            // Start Cut Four

            // Put this in places that listen for event
            //Common.EventAggregator.GetEvent<GetProcessesEvent>().Subscribe(GetProcesses);

            // End Cut Four
            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetProcessesCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetWorkItemTypes Command

        public DelegateCommand GetWorkItemTypesCommand { get; set; }
        public string GetWorkItemTypesContent { get; set; } = "GetWorkItemTypes";
        public string GetWorkItemTypesToolTip { get; set; } = "GetWorkItemTypes ToolTip";

        // Can get fancy and use Resources
        //public string GetProcessesContent { get; set; } = "ViewName_GetProcessesContent";
        //public string GetProcessesToolTip { get; set; } = "ViewName_GetProcessesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProcessesContent">GetProcesses</system:String>
        //    <system:String x:Key="ViewName_GetProcessesContentToolTip">GetProcesses ToolTip</system:String>  

        public void OnGetWorkItemTypesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);
            // TODO(crhodes)
            // Do something amazing.
            //Message = "Cool, you called GetProcesses";
            EventAggregator.GetEvent<GetWorkItemTypesEvent>().Publish(
                new GetWorkItemTypesEventArgs()
                {
                    CollectionDetails = _collectionMainViewModel.SelectedCollection.Details,
                    Process = _contextMainViewModel.Context.SelectedProcess
                });

            // Start Cut Four

            // Put this in places that listen for event
            //Common.EventAggregator.GetEvent<GetProcessesEvent>().Subscribe(GetProcesses);

            // End Cut Four
            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetWorkItemTypesCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            if (_collectionMainViewModel.SelectedCollection is null 
                || _contextMainViewModel.Context.SelectedProcess is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetStates Command

        public DelegateCommand GetStatesCommand { get; set; }
        public string GetStatesContent { get; set; } = "GetStates";
        public string GetStatesToolTip { get; set; } = "GetStates ToolTip";

        // Can get fancy and use Resources
        //public string GetStatesContent { get; set; } = "ViewName_GetStatesContent";
        //public string GetStatesToolTip { get; set; } = "ViewName_GetStatesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetStatesContent">GetStates</system:String>
        //    <system:String x:Key="ViewName_GetStatesContentToolTip">GetStates ToolTip</system:String>  

        public void OnGetStatesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetStatesEvent>().Publish(
                new GetStatesEventArgs()
                {
                    CollectionDetails = _collectionMainViewModel.SelectedCollection.Details,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetStatesCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemType is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetFields Command

        public DelegateCommand GetFieldsCommand { get; set; }
        public string GetFieldsContent { get; set; } = "GetFields";
        public string GetFieldsToolTip { get; set; } = "GetFields ToolTip";

        // Can get fancy and use Resources
        //public string GetFieldsContent { get; set; } = "ViewName_GetFieldsContent";
        //public string GetFieldsToolTip { get; set; } = "ViewName_GetFieldsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetFieldsContent">GetFields</system:String>
        //    <system:String x:Key="ViewName_GetFieldsContentToolTip">GetFields ToolTip</system:String>  

        public void OnGetFieldsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetFieldsEvent>().Publish(
                new GetFieldsEventArgs()
                {
                    CollectionDetails = _collectionMainViewModel.SelectedCollection.Details,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetFieldsCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemType is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetLists Command

        public DelegateCommand GetListsCommand { get; set; }
        public string GetListsContent { get; set; } = "GetLists";
        public string GetListsToolTip { get; set; } = "GetLists ToolTip";

        // Can get fancy and use Resources
        //public string GetListsContent { get; set; } = "ViewName_GetListsContent";
        //public string GetListsToolTip { get; set; } = "ViewName_GetListsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetListsContent">GetLists</system:String>
        //    <system:String x:Key="ViewName_GetListsContentToolTip">GetLists ToolTip</system:String>  

        public void OnGetListsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetListsEvent>().Publish(
                new GetListsEventArgs()
                {
                    CollectionDetails = _collectionMainViewModel.SelectedCollection.Details
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetListsCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null)
            {
                return false;
            }

            return true;
        }

        #endregion


        #region GetProjects Command

        public DelegateCommand GetProjectsCommand { get; set; }
        public string GetProjectsContent { get; set; } = "GetProjects";
        public string GetProjectsToolTip { get; set; } = "GetProjects ToolTip";

        // Can get fancy and use Resources
        //public string GetProjectsContent { get; set; } = "ViewName_GetProjectsContent";
        //public string GetProjectsToolTip { get; set; } = "ViewName_GetProjectsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProjectsContent">GetProjects</system:String>
        //    <system:String x:Key="ViewName_GetProjectsContentToolTip">GetProjects ToolTip</system:String>  

        public void OnGetProjectsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);
            // TODO(crhodes)
            // Do something amazing.
            //Message = "Cool, you called GetProjects";
            EventAggregator.GetEvent<GetProjectsEvent>().Publish(
                _collectionMainViewModel.SelectedCollection.Details);

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetProjectsCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion


        #region GetTeams Command

        public DelegateCommand GetTeamsCommand { get; set; }
        public string GetTeamsContent { get; set; } = "GetTeams";
        public string GetTeamsToolTip { get; set; } = "GetTeams ToolTip";

        // Can get fancy and use Resources
        //public string GetTeamsContent { get; set; } = "ViewName_GetTeamsContent";
        //public string GetTeamsToolTip { get; set; } = "ViewName_GetTeamsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTeamsContent">GetTeams</system:String>
        //    <system:String x:Key="ViewName_GetTeamsContentToolTip">GetTeams ToolTip</system:String>  

        public void OnGetTeamsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);
            // TODO(crhodes)
            // Do something amazing.
            //Message = "Cool, you called GetTeams";
            EventAggregator.GetEvent<GetTeamsEvent>().Publish(
                _collectionMainViewModel.SelectedCollection.Details);

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetTeamsCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

    }
}
