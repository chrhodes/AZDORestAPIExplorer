using System;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess;
using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess;

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

            #region Core Category

            GetCoreProcessesCommand = new DelegateCommand(GetCoreProcessesExecute, GetCoreProcessesCanExecute);
            GetProjectsCommand = new DelegateCommand(GetProjectsExecute, GetProjectsCanExecute);
            GetTeamsCommand = new DelegateCommand(GetTeamsExecute, GetTeamsCanExecute);

            #endregion

            #region Accounts Category

            GetAccountsCommand = new DelegateCommand(GetAccountsExecute, GetAccountsCanExecute);

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

            GetDashboardsCommand = new DelegateCommand(GetDashboardsExecute, GetDashboardsCanExecute);

            GetWidgetsCommand = new DelegateCommand(GetWidgetsExecute, GetWidgetsCanExecute);

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

            #endregion

            #region Work Item Tracking Process Category

            GetBehaviorsWITPCommand = new DelegateCommand(GetBehaviorsWITPExecute, GetBehaviorsWITPCanExecute);

            GetFieldsWITPCommand = new DelegateCommand(GetFieldsWITPExecute, GetFieldsWITPCanExecute);

            GetListsCommand = new DelegateCommand(GetListsExecute, GetListsCanExecute);

            GetProcessesWITPCommand = new DelegateCommand(GetProcessesWITPExecute, GetProcessesWITPCanExecute);

            GetRulesCommand = new DelegateCommand(GetRulesExecute, GetRulesCanExecute);

            GetStatesCommand = new DelegateCommand(GetStatesExecute, GetStatesCanExecute);

            GetSystemControlsCommand = new DelegateCommand(GetSystemControlsExecute, GetSystemControlsCanExecute);

            GetWorkItemTypesWITPCommand = new DelegateCommand(GetWorkItemTypesWITPExecute, GetWorkItemTypesWITPCanExecute);

            #endregion


            EventAggregator.GetEvent<SelectedCollectionChangedEvent>().Subscribe(RaiseCollectionChanged);

            EventAggregator.GetEvent<Core.Events.Core.SelectedProcessChangedEvent>().Subscribe(RaiseProcessChanged);
            EventAggregator.GetEvent<Core.Events.Core.SelectedProjectChangedEvent>().Subscribe(Raise_Project_Changed);
            EventAggregator.GetEvent<Core.Events.Core.SelectedTeamChangedEvent>().Subscribe(RaiseTeamChanged);

            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.SelectedWorkItemTypeChangedEvent>().Subscribe(RaiseWorkItemTypeChanged);

            EventAggregator.GetEvent<Core.Events.Dashboard.SelectedDashboardChangedEvent>().Subscribe(RaiseDashboardChanged);


            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        // NOTE(crhodes)
        // RaiseCanExecuteChanged for any Command that is dependent on Context.
        // N.B. Need to add to each Context Item for button to be enabled.

        private void RaiseCollectionChanged()
        {
            GetProjectsCommand.RaiseCanExecuteChanged();
            GetTeamsCommand.RaiseCanExecuteChanged();
            GetCoreProcessesCommand.RaiseCanExecuteChanged();
            GetWidgetsCommand.RaiseCanExecuteChanged();
            GetBehaviorsWITPCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();

            GetProcessesWITPCommand.RaiseCanExecuteChanged();
        }

        private void RaiseProcessChanged(Domain.Core.Process process)
        {
            GetWorkItemTypesWITPCommand.RaiseCanExecuteChanged();
            GetListsCommand.RaiseCanExecuteChanged();
            GetBehaviorsWITPCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();
        }

        private void RaiseWorkItemTypeChanged(WorkItemType workItemType)
        {
            GetStatesCommand.RaiseCanExecuteChanged();
            GetFieldsWITPCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();
        }

        private void Raise_Project_Changed(Project project)
        {
            GetProjectsCommand.RaiseCanExecuteChanged();
            GetDashboardsCommand.RaiseCanExecuteChanged();
            GetWidgetsCommand.RaiseCanExecuteChanged();
        }

        private void RaiseTeamChanged(Team team)
        {
            GetTeamsCommand.RaiseCanExecuteChanged();
            GetDashboardsCommand.RaiseCanExecuteChanged();
            GetWidgetsCommand.RaiseCanExecuteChanged();
        }

        private void RaiseDashboardChanged(Domain.Dashboard.Dashboard dashboard)
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

        // TODO(crhodes)
        // Decide if these need to be public, perhaps just private

        #region Accounts Category

        #region GetAccounts Command

        public DelegateCommand GetAccountsCommand { get; set; }
        public string GetAccountsContent { get; set; } = "GetAccounts";
        public string GetAccountsToolTip { get; set; } = "GetAccounts ToolTip";

        // Can get fancy and use Resources
        //public string GetAccountsContent { get; set; } = "ViewName_GetAccountsContent";
        //public string GetAccountsToolTip { get; set; } = "ViewName_GetAccountsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetAccountsContent">GetAccounts</system:String>
        //    <system:String x:Key="ViewName_GetAccountsContentToolTip">GetAccounts ToolTip</system:String>  

        public void GetAccountsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<Core.Events.Accounts.GetAccountsEvent>().Publish(
                new Core.Events.Accounts.GetAccountsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    //, Project = _contextMainViewModel.Context.SelectedProject
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetAccountsCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            return true;
        }

        #endregion

        #endregion

        #region Artifacts Category

        #endregion

        #region Build Category


        #endregion

        #region Cloud Load Test Category


        #endregion

        #region Core

        #region GetCoreProcesses Command

        public DelegateCommand GetCoreProcessesCommand { get; set; }
        public string GetCoreProcessesContent { get; set; } = "Get Core Processes";
        public string GetCoreProcessesToolTip { get; set; } = "Get Core Processes ToolTip";

        // Can get fancy and use Resources
        //public string GetProcessesContent { get; set; } = "ViewName_GetProcessesContent";
        //public string GetProcessesToolTip { get; set; } = "ViewName_GetProcessesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProcessesContent">GetProcesses</system:String>
        //    <system:String x:Key="ViewName_GetProcessesContentToolTip">GetProcesses ToolTip</system:String>  

        public void GetCoreProcessesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<Core.Events.Core.GetProcessesEvent>().Publish(
                new Core.Events.Core.GetProcessesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetCoreProcessesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetCoreProjects Command

        public DelegateCommand GetProjectsCommand { get; set; }
        public string GetProjectsContent { get; set; } = "Get Projects";
        public string GetProjectsToolTip { get; set; } = "Get Projects ToolTip";

        // Can get fancy and use Resources
        //public string Get_CoreProjectsContent { get; set; } = "ViewName_Get_Core_ProjectsContent";
        //public string Get_CoreProjectsToolTip { get; set; } = "ViewName_Get_Core_ProjectsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_Get_Core_ProjectsContent">Get_Core_Projects</system:String>
        //    <system:String x:Key="ViewName_Get_Core_ProjectsContentToolTip">Get_Core_Projects ToolTip</system:String>  

        public void GetProjectsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetProjectsEvent>().Publish(
                new GetProjectsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetProjectsCanExecute()
        {
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

        public void GetTeamsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetTeamsEvent>().Publish(
                new GetTeamsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetTeamsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #endregion

        #region Dashboard Category

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

        public void GetDashboardsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<Core.Events.Dashboard.GetDashboardsEvent>().Publish(
                new Core.Events.Dashboard.GetDashboardsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    , Project = _contextMainViewModel.Context.SelectedProject
                    , Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetDashboardsCanExecute()
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

        public void GetWidgetsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetWidgetsEvent>().Publish(
                new GetWidgetsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetWidgetsCanExecute()
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

        #endregion 

        #region Work Item Tracking Process Category

        #region GetBehaviors Command

        public DelegateCommand GetBehaviorsWITPCommand { get; set; }
        public string GetBehaviorsWITPContent { get; set; } = "GetBehaviors (WITP)";
        public string GetBehaviorsWITPToolTip { get; set; } = "GetBehaviors (WITP) ToolTip";

        // Can get fancy and use Resources
        //public string BehaviorContent { get; set; } = "ViewName_BehaviorContent";
        //public string BehaviorToolTip { get; set; } = "ViewName_BehaviorContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_BehaviorContent">Behavior</system:String>
        //    <system:String x:Key="ViewName_BehaviorContentToolTip">Behavior ToolTip</system:String>  

        public void GetBehaviorsWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            // HACK(crhodes)
            // Uncomment this once get Process figure out.

            //EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.GetBehaviorsEvent>().Publish(
            //    new Core.Events.WorkItemTrackingProcess.GetBehaviorsEventArgs()
            //    {
            //        Organization = _collectionMainViewModel.SelectedCollection.Organization
            //        , Process = _contextMainViewModel.Context.SelectedProcess
            //    });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetBehaviorsWITPCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null)
            {
                return false;
            }

            return true;
        }

        #endregion
        
        #region GetFields Command

        public DelegateCommand GetFieldsWITPCommand { get; set; }
        public string GetFieldsWITPContent { get; set; } = "GetFields (WITP)";
        public string GetFieldsWITPToolTip { get; set; } = "GetFields (WITP) ToolTip";

        // Can get fancy and use Resources
        //public string GetFieldsContent { get; set; } = "ViewName_GetFieldsContent";
        //public string GetFieldsToolTip { get; set; } = "ViewName_GetFieldsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetFieldsContent">GetFields</system:String>
        //    <system:String x:Key="ViewName_GetFieldsContentToolTip">GetFields ToolTip</system:String>  

        public void GetFieldsWITPExecute()
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

        public bool GetFieldsWITPCanExecute()
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

        public void GetListsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetListsEvent>().Publish(
                new GetListsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetListsCanExecute()
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

        public DelegateCommand GetProcessesWITPCommand { get; set; }
        public string GetProcessesWITPContent { get; set; } = "Get WITP Processes (WITP)";
        public string GetProcessesWITPToolTip { get; set; } = "Get WITP Processes (WITP) ToolTip";

        // Can get fancy and use Resources
        //public string GetProcessesContent { get; set; } = "ViewName_GetProcessesContent";
        //public string GetProcessesToolTip { get; set; } = "ViewName_GetProcessesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProcessesContent">GetProcesses</system:String>
        //    <system:String x:Key="ViewName_GetProcessesContentToolTip">GetProcesses ToolTip</system:String>  

        public void GetProcessesWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.GetProcessesEvent>().Publish(
                new Core.Events.WorkItemTrackingProcess.GetProcessesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetProcessesWITPCanExecute()
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

        public void GetRulesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            // HACK(crhodes)
            // Uncomment once get Process Figured out.

            //EventAggregator.GetEvent<GetRulesEvent>().Publish(
            //    new GetRulesEventArgs()
            //    {
            //        Organization = _collectionMainViewModel.SelectedCollection.Organization,
            //        Process = _contextMainViewModel.Context.SelectedProcess,
            //        WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
            //    });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetRulesCanExecute()
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

        public void GetStatesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            // HACK(crhodes)
            // Uncomment once get Process Figured out.

            //EventAggregator.GetEvent<GetStatesEvent>().Publish(
            //    new GetStatesEventArgs()
            //    {
            //        Organization = _collectionMainViewModel.SelectedCollection.Organization,
            //        Process = _contextMainViewModel.Context.SelectedProcess,
            //        WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
            //    });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetStatesCanExecute()
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

        public void GetSystemControlsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            // HACK(crhodes)
            // Uncomment once get Process Figured out.

            //EventAggregator.GetEvent<GetSystemControlsEvent>().Publish(
            //    new GetSystemControlsEventArgs()
            //    {
            //        Organization = _collectionMainViewModel.SelectedCollection.Organization
            //        , Process = _contextMainViewModel.Context.SelectedProcess
            //        , WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
            //    });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetSystemControlsCanExecute()
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

        #region GetWorkItemTypes Command

        public DelegateCommand GetWorkItemTypesWITPCommand { get; set; }
        public string GetWorkItemTypesWITPContent { get; set; } = "GetWorkItemTypes (WITP)";
        public string GetWorkItemTypesWITPToolTip { get; set; } = "GetWorkItemTypes (WITP) ToolTip";

        // Can get fancy and use Resources
        //public string GetProcessesContent { get; set; } = "ViewName_GetProcessesContent";
        //public string GetProcessesToolTip { get; set; } = "ViewName_GetProcessesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProcessesContent">GetProcesses</system:String>
        //    <system:String x:Key="ViewName_GetProcessesContentToolTip">GetProcesses ToolTip</system:String>  

        public void GetWorkItemTypesWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            // HACK(crhodes)
            // Uncomment this once get Process understood

            //EventAggregator.GetEvent<GetWorkItemTypesEvent>().Publish(
            //    new GetWorkItemTypesEventArgs()
            //    {
            //        Organization = _collectionMainViewModel.SelectedCollection.Organization,
            //        Process = _contextMainViewModel.Context.SelectedProcess
            //    });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetWorkItemTypesWITPCanExecute()
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
