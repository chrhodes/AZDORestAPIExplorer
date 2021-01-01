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

            Get_Processes_Command = new DelegateCommand(OnGetProcessesExecute, OnGetProcessesCanExecute);
            Get_Projects_Command = new DelegateCommand(OnGet_Projects_Execute, OnGet_Projects_CanExecute);
            Get_Teams_Command = new DelegateCommand(OnGetTeamsExecute, OnGetTeamsCanExecute);
            GetWorkItemTypesCommand = new DelegateCommand(OnGetWorkItemTypesExecute, OnGetWorkItemTypesCanExecute);
            GetStatesCommand = new DelegateCommand(OnGetStatesExecute, OnGetStatesCanExecute);
            GetFieldsCommand = new DelegateCommand(OnGetFieldsExecute, OnGetFieldsCanExecute);
            GetListsCommand = new DelegateCommand(OnGetListsExecute, OnGetListsCanExecute);
            GetDashboardsCommand = new DelegateCommand(OnGetDashboardsExecute, OnGetDashboardsCanExecute);
            GetWidgetsCommand = new DelegateCommand(OnGetWidgetsExecute, OnGetWidgetsCanExecute);
            GetBehaviorsCommand = new DelegateCommand(OnGetBehaviorsExecute, OnGetBehaviorsCanExecute);
            GetSystemControlsCommand = new DelegateCommand(OnGetSystemControlsExecute, OnGetSystemControlsCanExecute);
            GetRulesCommand = new DelegateCommand(OnGetRulesExecute, OnGetRulesCanExecute);

            EventAggregator.GetEvent<SelectedCollectionChangedEvent>().Subscribe(RaiseCollectionChanged);

            EventAggregator.GetEvent<SelectedProcessChangedEvent>().Subscribe(RaiseProcessChanged);
            EventAggregator.GetEvent<SelectedWorkItemTypeChangedEvent>().Subscribe(RaiseWorkItemTypeChanged);
            EventAggregator.GetEvent<Selected_Project_ChangedEvent>().Subscribe(Raise_Project_Changed);
            EventAggregator.GetEvent<SelectedTeamChangedEvent>().Subscribe(RaiseTeamChanged);

            EventAggregator.GetEvent<SelectedDashboardChangedEvent>().Subscribe(RaiseDashboardChanged);

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        // NOTE(crhodes)
        // RaiseCanExecuteChanged for any Command that is dependent on Context.
        // N.B. Need to add to each Context Item for button to be enabled.

        private void RaiseCollectionChanged()
        {
            Get_Projects_Command.RaiseCanExecuteChanged();
            Get_Teams_Command.RaiseCanExecuteChanged();
            Get_Processes_Command.RaiseCanExecuteChanged();
            GetWidgetsCommand.RaiseCanExecuteChanged();
            GetBehaviorsCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();
        }

        private void RaiseProcessChanged(Process process)
        {
            GetWorkItemTypesCommand.RaiseCanExecuteChanged();
            GetListsCommand.RaiseCanExecuteChanged();
            GetBehaviorsCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();
        }

        private void RaiseWorkItemTypeChanged(WorkItemType workItemType)
        {
            GetStatesCommand.RaiseCanExecuteChanged();
            GetFieldsCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();
        }

        private void Raise_Project_Changed(Project project)
        {
            Get_Projects_Command.RaiseCanExecuteChanged();
            GetDashboardsCommand.RaiseCanExecuteChanged();
            GetWidgetsCommand.RaiseCanExecuteChanged();
        }

        private void RaiseTeamChanged(Team team)
        {
            Get_Teams_Command.RaiseCanExecuteChanged();
            GetDashboardsCommand.RaiseCanExecuteChanged();
            GetWidgetsCommand.RaiseCanExecuteChanged();
        }

        private void RaiseDashboardChanged(Dashboard dashboard)
        {
            GetWidgetsCommand.RaiseCanExecuteChanged();
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

        #region Core


        #region Get_Core_Projects Command

        public DelegateCommand Get_Projects_Command { get; set; }
        public string Get_Projects_Content { get; set; } = "Get Projects";
        public string Get_Projects_ToolTip { get; set; } = "Get Projects ToolTip";

        // Can get fancy and use Resources
        //public string Get_Core_Projects_Content { get; set; } = "ViewName_Get_Core_ProjectsContent";
        //public string Get_Core_Projects_ToolTip { get; set; } = "ViewName_Get_Core_ProjectsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_Get_Core_ProjectsContent">Get_Core_Projects</system:String>
        //    <system:String x:Key="ViewName_Get_Core_ProjectsContentToolTip">Get_Core_Projects ToolTip</system:String>  

        public void OnGet_Projects_Execute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<Get_Projects_Event>().Publish(
                new Get_Projects_EventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGet_Projects_CanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #endregion

        // TODO(crhodes)
        // Decide if these need to be public

        #region GetBehaviors Command

        public DelegateCommand GetBehaviorsCommand { get; set; }
        public string GetBehaviorsContent { get; set; } = "GetBehaviors";
        public string GetBehaviorsToolTip { get; set; } = "GetBehaviors ToolTip";

        // Can get fancy and use Resources
        //public string BehaviorContent { get; set; } = "ViewName_BehaviorContent";
        //public string BehaviorToolTip { get; set; } = "ViewName_BehaviorContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_BehaviorContent">Behavior</system:String>
        //    <system:String x:Key="ViewName_BehaviorContentToolTip">Behavior ToolTip</system:String>  

        public void OnGetBehaviorsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetBehaviorsEvent>().Publish(
                new GetBehaviorsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    , Process = _contextMainViewModel.Context.SelectedProcess
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetBehaviorsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetDashboards Command

        public DelegateCommand GetDashboardsCommand { get; set; }
        public string GetDashboardsContent { get; set; } = "GetDashboards";
        public string GetDashboardsToolTip { get; set; } = "GetDashboards ToolTip";

        // Can get fancy and use Resources
        //public string GetDashboardsContent { get; set; } = "ViewName_GetDashboardsContent";
        //public string GetDashboardsToolTip { get; set; } = "ViewName_GetDashboardsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetDashboardsContent">GetDashboards</system:String>
        //    <system:String x:Key="ViewName_GetDashboardsContentToolTip">GetDashboards ToolTip</system:String>  

        public void OnGetDashboardsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetDashboardsEvent>().Publish(
                new GetDashboardsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    , Project = _contextMainViewModel.Context.SelectedProject
                    , Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetDashboardsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedTeam is null)
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
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetFieldsCanExecute()
        {
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
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetListsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetProcesses Command

        public DelegateCommand Get_Processes_Command { get; set; }
        public string Get_Processes_Content { get; set; } = "Get Processes";
        public string Get_Processes_ToolTip { get; set; } = "Get Processes ToolTip";

        // Can get fancy and use Resources
        //public string GetProcessesContent { get; set; } = "ViewName_GetProcessesContent";
        //public string GetProcessesToolTip { get; set; } = "ViewName_GetProcessesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProcessesContent">GetProcesses</system:String>
        //    <system:String x:Key="ViewName_GetProcessesContentToolTip">GetProcesses ToolTip</system:String>  

        public void OnGetProcessesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetProcessesEvent>().Publish(
                new GetProcessesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetProcessesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetRules Command

        public DelegateCommand GetRulesCommand { get; set; }
        public string GetRulesContent { get; set; } = "GetRules";
        public string GetRulesToolTip { get; set; } = "GetRules ToolTip";

        // Can get fancy and use Resources
        //public string GetRulesContent { get; set; } = "ViewName_GetRulesContent";
        //public string GetRulesToolTip { get; set; } = "ViewName_GetRulesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetRulesContent">GetRules</system:String>
        //    <system:String x:Key="ViewName_GetRulesContentToolTip">GetRules ToolTip</system:String>  

        public void OnGetRulesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetRulesEvent>().Publish(
                new GetRulesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetRulesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemType is null)
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
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetStatesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemType is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetSystemControls Command

        public DelegateCommand GetSystemControlsCommand { get; set; }
        public string GetSystemControlsContent { get; set; } = "GetSystemControls";
        public string GetSystemControlsToolTip { get; set; } = "GetSystemControls ToolTip";

        // Can get fancy and use Resources
        //public string GetSystemControlsContent { get; set; } = "ViewName_GetSystemControlsContent";
        //public string GetSystemControlsToolTip { get; set; } = "ViewName_GetSystemControlsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetSystemControlsContent">GetSystemControls</system:String>
        //    <system:String x:Key="ViewName_GetSystemControlsContentToolTip">GetSystemControls ToolTip</system:String>  

        public void OnGetSystemControlsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetSystemControlsEvent>().Publish(
                new GetSystemControlsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    , Process = _contextMainViewModel.Context.SelectedProcess
                    , WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetSystemControlsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemType is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetTeams Command

        public DelegateCommand Get_Teams_Command { get; set; }
        public string Get_Teams_Content { get; set; } = "GetTeams";
        public string Get_Teams_ToolTip { get; set; } = "GetTeams ToolTip";

        // Can get fancy and use Resources
        //public string GetTeamsContent { get; set; } = "ViewName_GetTeamsContent";
        //public string GetTeamsToolTip { get; set; } = "ViewName_GetTeamsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTeamsContent">GetTeams</system:String>
        //    <system:String x:Key="ViewName_GetTeamsContentToolTip">GetTeams ToolTip</system:String>  

        public void OnGetTeamsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetTeamsEvent>().Publish(
                new GetTeamsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetTeamsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetWidgets Command

        public DelegateCommand GetWidgetsCommand { get; set; }
        public string GetWidgetsContent { get; set; } = "GetWidgets";
        public string GetWidgetsToolTip { get; set; } = "GetWidgets ToolTip";

        // Can get fancy and use Resources
        //public string GetWidgetsContent { get; set; } = "ViewName_GetWidgetsContent";
        //public string GetWidgetsToolTip { get; set; } = "ViewName_GetWidgetsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWidgetsContent">GetWidgets</system:String>
        //    <system:String x:Key="ViewName_GetWidgetsContentToolTip">GetWidgets ToolTip</system:String>  

        public void OnGetWidgetsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetWidgetsEvent>().Publish(
                new GetWidgetsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetWidgetsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedTeam is null)
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

            EventAggregator.GetEvent<GetWorkItemTypesEvent>().Publish(
                new GetWorkItemTypesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetWorkItemTypesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #endregion


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods


        #endregion

    }
}
