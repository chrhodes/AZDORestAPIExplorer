using System;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Core.Events.WorkItemTracking;
using AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess;
using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess;
using AZDORestApiExplorer.WorkItemTracking.Core.Events;

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

            GetArtifactLinkTypesCommand = new DelegateCommand(GetArtifactLinkTypesExecute, GetArtifactLinkTypesCanExecute);
            GetFieldsWITCommand = new DelegateCommand(GetFieldsWITExecute, GetFieldsWITCanExecute);
            GetQueriesCommand = new DelegateCommand(GetQueriesExecute, GetQueriesCanExecute);
            GetTagsCommand = new DelegateCommand(GetTagsExecute, GetTagsCanExecute);
            GetTemplatesCommand = new DelegateCommand(GetTemplatesExecute, GetTemplatesCanExecute);
            GetWorkItemIconsCommand = new DelegateCommand(GetWorkItemIconsExecute, GetWorkItemIconsCanExecute);
            GetWorkItemRelationTypesCommand = new DelegateCommand(GetWorkItemRelationTypesExecute, GetWorkItemRelationTypesCanExecute);
            GetWorkItemTypeCategoriesCommand = new DelegateCommand(GetWorkItemTypeCategoriesExecute, GetWorkItemTypeCategoriesCanExecute);
            GetStatesWITCommand = new DelegateCommand(GetStatesWITExecute, GetStatesWITCanExecute);
            GetWorkItemTypesWITCommand = new DelegateCommand(GetWorkItemTypesWITExecute, GetWorkItemTypesWITCanExecute);
            GetWorkItemTypesFieldsCommand = new DelegateCommand(GetWorkItemTypesFieldsExecute, GetWorkItemTypesFieldsCanExecute);

            #endregion

            #region Work Item Tracking Process Category

            GetBehaviorsWITPCommand = new DelegateCommand(GetBehaviorsWITPExecute, GetBehaviorsWITPCanExecute);

            GetFieldsWITPCommand = new DelegateCommand(GetFieldsWITPExecute, GetFieldsWITPCanExecute);

            GetListsCommand = new DelegateCommand(GetListsExecute, GetListsCanExecute);

            GetProcessesWITPCommand = new DelegateCommand(GetProcessesWITPExecute, GetProcessesWITPCanExecute);

            GetRulesCommand = new DelegateCommand(GetRulesExecute, GetRulesCanExecute);

            GetStatesWITPCommand = new DelegateCommand(GetStatesWITPExecute, GetStatesWITPCanExecute);

            GetSystemControlsCommand = new DelegateCommand(GetSystemControlsExecute, GetSystemControlsCanExecute);

            GetWorkItemTypesWITPCommand = new DelegateCommand(GetWorkItemTypesWITPExecute, GetWorkItemTypesWITPCanExecute);

            GetWorkItemTypesBehaviorsCommand = new DelegateCommand(GetWorkItemTypesBehaviorsExecute, GetWorkItemTypesBehaviorsCanExecute);

            #endregion

            EventAggregator.GetEvent<SelectedCollectionChangedEvent>().Subscribe(RaiseCollectionChanged);

            EventAggregator.GetEvent<Core.Events.Core.SelectedProcessChangedEvent>().Subscribe(RaiseProcessChanged);
            EventAggregator.GetEvent<Core.Events.Core.SelectedProjectChangedEvent>().Subscribe(RaiseProjectChanged);
            EventAggregator.GetEvent<Core.Events.Core.SelectedTeamChangedEvent>().Subscribe(RaiseTeamChanged);

            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.SelectedWorkItemTypeChangedEvent>().Subscribe(RaiseWorkItemTypeChanged);

            EventAggregator.GetEvent<Core.Events.Dashboard.SelectedDashboardChangedEvent>().Subscribe(RaiseDashboardChanged);

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
            GetCoreProcessesCommand.RaiseCanExecuteChanged();
            GetProjectsCommand.RaiseCanExecuteChanged();
            GetTeamsCommand.RaiseCanExecuteChanged();

            GetWidgetsCommand.RaiseCanExecuteChanged();

            GetBehaviorsWITPCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();

            GetProcessesWITPCommand.RaiseCanExecuteChanged();

            // Work Item Tracking
            GetArtifactLinkTypesCommand.RaiseCanExecuteChanged();
            GetWorkItemIconsCommand.RaiseCanExecuteChanged();
            GetWorkItemRelationTypesCommand.RaiseCanExecuteChanged();

            // Work Item Tracking Process
            GetListsCommand.RaiseCanExecuteChanged();
        }

        private void RaiseProcessChanged(Domain.Core.Process process)
        {
            GetWorkItemTypesWITPCommand.RaiseCanExecuteChanged();

            GetBehaviorsWITPCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();
        }

        private void RaiseWorkItemTypeChanged(WorkItemType workItemType)
        {
            GetFieldsWITPCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();
            GetStatesWITPCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetWorkItemTypesBehaviorsCommand.RaiseCanExecuteChanged();

            // Work Item Tracking
            GetWorkItemTypesFieldsCommand.RaiseCanExecuteChanged();
            GetStatesWITCommand.RaiseCanExecuteChanged();
        }

        private void RaiseProjectChanged(Project project)
        {
            GetProjectsCommand.RaiseCanExecuteChanged();
            GetDashboardsCommand.RaiseCanExecuteChanged();
            GetWidgetsCommand.RaiseCanExecuteChanged();

            // Work Item Tracking
            GetFieldsWITCommand.RaiseCanExecuteChanged();
            GetQueriesCommand.RaiseCanExecuteChanged();
            GetTagsCommand.RaiseCanExecuteChanged();
            GetTemplatesCommand.RaiseCanExecuteChanged();
            GetWorkItemRelationTypesCommand.RaiseCanExecuteChanged();
            GetWorkItemTypeCategoriesCommand.RaiseCanExecuteChanged();
            GetStatesWITCommand.RaiseCanExecuteChanged();
            GetWorkItemTypesWITCommand.RaiseCanExecuteChanged();
            GetWorkItemTypesFieldsCommand.RaiseCanExecuteChanged();
        }

        private void RaiseTeamChanged(Team team)
        {
            GetTeamsCommand.RaiseCanExecuteChanged();
            GetDashboardsCommand.RaiseCanExecuteChanged();
            GetWidgetsCommand.RaiseCanExecuteChanged();

            // Work Item Tracking
            GetTemplatesCommand.RaiseCanExecuteChanged();
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
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

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
        public string GetCoreProcessesContent { get; set; } = "Get Processes";
        public string GetCoreProcessesToolTip { get; set; } = "Get Processes ToolTip";

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

        #region GetArtifactLinkTypes Command

        public DelegateCommand GetArtifactLinkTypesCommand { get; set; }
        public string GetArtifactLinkTypesContent { get; set; } = "GetArtifactLinkTypes";
        public string GetArtifactLinkTypesToolTip { get; set; } = "GetArtifactLinkTypes ToolTip";

        // Can get fancy and use Resources
        //public string GetArtifactLinkTypesContent { get; set; } = "ViewName_GetArtifactLinkTypesContent";
        //public string GetArtifactLinkTypesToolTip { get; set; } = "ViewName_GetArtifactLinkTypesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetArtifactLinkTypesContent">GetArtifactLinkTypes</system:String>
        //    <system:String x:Key="ViewName_GetArtifactLinkTypesContentToolTip">GetArtifactLinkTypes ToolTip</system:String>  

        public void GetArtifactLinkTypesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetArtifactLinkTypesEvent>().Publish(
                new GetArtifactLinkTypesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetArtifactLinkTypesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetFields Command

        public DelegateCommand GetFieldsWITCommand { get; set; }
        public string GetFieldsWITContent { get; set; } = "GetFields";
        public string GetFieldsWITToolTip { get; set; } = "GetFields ToolTip";

        // Can get fancy and use Resources
        //public string GetFieldsContent { get; set; } = "ViewName_GetFieldsContent";
        //public string GetFieldsToolTip { get; set; } = "ViewName_GetFieldsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetFieldsContent">GetFields</system:String>
        //    <system:String x:Key="ViewName_GetFieldsContentToolTip">GetFields ToolTip</system:String>  

        public void GetFieldsWITExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<Core.Events.WorkItemTracking.GetFieldsWITEvent>().Publish(
                new Core.Events.WorkItemTracking.GetFieldsWITEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetFieldsWITCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetQueries Command

        public DelegateCommand GetQueriesCommand { get; set; }
        public string GetQueriesContent { get; set; } = "GetQueries";
        public string GetQueriesToolTip { get; set; } = "GetQueries ToolTip";

        // Can get fancy and use Resources
        //public string GetQueriesContent { get; set; } = "ViewName_GetQueriesContent";
        //public string GetQueriesToolTip { get; set; } = "ViewName_GetQueriesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetQueriesContent">GetQueries</system:String>
        //    <system:String x:Key="ViewName_GetQueriesContentToolTip">GetQueries ToolTip</system:String>  

        public void GetQueriesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetQueriesEvent>().Publish(
                new GetQueriesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    , Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetQueriesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetTags Command

        public DelegateCommand GetTagsCommand { get; set; }
        public string GetTagsContent { get; set; } = "GetTags";
        public string GetTagsToolTip { get; set; } = "GetTags ToolTip";

        // Can get fancy and use Resources
        //public string GetTagsContent { get; set; } = "ViewName_GetTagsContent";
        //public string GetTagsToolTip { get; set; } = "ViewName_GetTagsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTagsContent">GetTags</system:String>
        //    <system:String x:Key="ViewName_GetTagsContentToolTip">GetTags ToolTip</system:String>  

        public void GetTagsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetTagsEvent>().Publish(
                new GetTagsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    , Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetTagsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetTemplates Command

        public DelegateCommand GetTemplatesCommand { get; set; }
        public string GetTemplatesContent { get; set; } = "GetTemplates";
        public string GetTemplatesToolTip { get; set; } = "GetTemplates ToolTip";

        // Can get fancy and use Resources
        //public string GetTemplatesContent { get; set; } = "ViewName_GetTemplatesContent";
        //public string GetTemplatesToolTip { get; set; } = "ViewName_GetTemplatesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTemplatesContent">GetTemplates</system:String>
        //    <system:String x:Key="ViewName_GetTemplatesContentToolTip">GetTemplates ToolTip</system:String>  

        public void GetTemplatesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetTemplatesEvent>().Publish(
                new GetTemplatesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    , Project = _contextMainViewModel.Context.SelectedProject
                    , Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetTemplatesCanExecute()
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

        #region GetWorkItemIcons Command

        public DelegateCommand GetWorkItemIconsCommand { get; set; }
        public string GetWorkItemIconsContent { get; set; } = "GetWorkItemIcons";
        public string GetWorkItemIconsToolTip { get; set; } = "GetWorkItemIcons ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkItemIconsContent { get; set; } = "ViewName_GetWorkItemIconsContent";
        //public string GetWorkItemIconsToolTip { get; set; } = "ViewName_GetWorkItemIconsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkItemIconsContent">GetWorkItemIcons</system:String>
        //    <system:String x:Key="ViewName_GetWorkItemIconsContentToolTip">GetWorkItemIcons ToolTip</system:String>  

        public void GetWorkItemIconsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetWorkItemIconsEvent>().Publish(
                new GetWorkItemIconsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetWorkItemIconsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetWorkRelationTypes Command

        public DelegateCommand GetWorkItemRelationTypesCommand { get; set; }
        public string GetWorkItemRelationTypesContent { get; set; } = "GetWorItemRelationTypes";
        public string GetWorkItemRelationTypesToolTip { get; set; } = "GetWorItemRelationTypes ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkRelationTypesContent { get; set; } = "ViewName_GetWorkRelationTypesContent";
        //public string GetWorkRelationTypesToolTip { get; set; } = "ViewName_GetWorkRelationTypesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkRelationTypesContent">GetWorkRelationTypes</system:String>
        //    <system:String x:Key="ViewName_GetWorkRelationTypesContentToolTip">GetWorkRelationTypes ToolTip</system:String>  

        public void GetWorkItemRelationTypesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetWorkItemRelationTypesEvent>().Publish(
                new GetWorkItemRelationTypesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetWorkItemRelationTypesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetWorkItemTypeCategories Command

        public DelegateCommand GetWorkItemTypeCategoriesCommand { get; set; }
        public string GetWorkItemTypeCategoriesContent { get; set; } = "GetWorkItemTypeCategories";
        public string GetWorkItemTypeCategoriesToolTip { get; set; } = "GetWorkItemTypeCategories ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkItemTypeCategoriesContent { get; set; } = "ViewName_GetWorkItemTypeCategoriesContent";
        //public string GetWorkItemTypeCategoriesToolTip { get; set; } = "ViewName_GetWorkItemTypeCategoriesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkItemTypeCategoriesContent">GetWorkItemTypeCategories</system:String>
        //    <system:String x:Key="ViewName_GetWorkItemTypeCategoriesContentToolTip">GetWorkItemTypeCategories ToolTip</system:String>  

        public void GetWorkItemTypeCategoriesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetWorkItemTypeCategoriesEvent>().Publish(
                new GetWorkItemTypeCategoriesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    , Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetWorkItemTypeCategoriesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetStates Command

        public DelegateCommand GetStatesWITCommand { get; set; }
        public string GetStatesWITContent { get; set; } = "GetStates (WIT)";
        public string GetStatesWITToolTip { get; set; } = "GetStates (WIT) ToolTip";

        // Can get fancy and use Resources
        //public string GetStatesWITPContent { get; set; } = "ViewName_GetStatesWITContent";
        //public string GetStatesWITPToolTip { get; set; } = "ViewName_GetStatesWITContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetStatesContent">GetStates (WIT)</system:String>
        //    <system:String x:Key="ViewName_GetStatesContentToolTip">GetStates (WIT) ToolTip</system:String>  

        public void GetStatesWITExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetStatesWITEvent>().Publish(
                new GetStatesWITEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetStatesWITCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedWorkItemType is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetWorkItemTypesWIT Command

        public DelegateCommand GetWorkItemTypesWITCommand { get; set; }
        public string GetWorkItemTypesWITContent { get; set; } = "Get WorkItemTypes (WIT)";
        public string GetWorkItemTypesWITToolTip { get; set; } = "Get WorkItemTypes (WIT) ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkItemTypesWITContent { get; set; } = "ViewName_GetWorkItemTypesWITContent";
        //public string GetWorkItemTypesWITToolTip { get; set; } = "ViewName_GetWorkItemTypesWITContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkItemTypesWITContent">GetWorkItemTypesWIT</system:String>
        //    <system:String x:Key="ViewName_GetWorkItemTypesWITContentToolTip">GetWorkItemTypesWIT ToolTip</system:String>  

        public void GetWorkItemTypesWITExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetWorkItemTypesWITEvent>().Publish(
                new GetWorkItemTypesWITEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetWorkItemTypesWITCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetWorkItemTypesFields Command

        public DelegateCommand GetWorkItemTypesFieldsCommand { get; set; }
        public string GetWorkItemTypesFieldsContent { get; set; } = "Get WorkItemTypesFields";
        public string GetWorkItemTypesFieldsToolTip { get; set; } = "Get WorkItemTypesFields ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkItemTypesFieldsContent { get; set; } = "ViewName_GetWorkItemTypesFieldsContent";
        //public string GetWorkItemTypesFieldsToolTip { get; set; } = "ViewName_GetWorkItemTypesFieldsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkItemTypesFieldsContent">GetWorkItemTypesFields</system:String>
        //    <system:String x:Key="ViewName_GetWorkItemTypesFieldsContentToolTip">GetWorkItemTypesFields ToolTip</system:String>  

        public void GetWorkItemTypesFieldsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetWorkItemTypesFieldsWITEvent>().Publish(
                new GetWorkItemTypesFieldsWITEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    , Project = _contextMainViewModel.Context.SelectedProject
                    , WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetWorkItemTypesFieldsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedWorkItemType is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #endregion

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

            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.GetBehaviorsEvent>().Publish(
                new GetBehaviorsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Process = _contextMainViewModel.Context.SelectedProcess
                });

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
        public string GetFieldsWITPContent { get; set; } = "Get Fields (WITP)";
        public string GetFieldsWITPToolTip { get; set; } = "Get Fields (WITP) ToolTip";

        // Can get fancy and use Resources
        //public string GetFieldsContent { get; set; } = "ViewName_GetFieldsContent";
        //public string GetFieldsToolTip { get; set; } = "ViewName_GetFieldsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetFieldsContent">GetFields</system:String>
        //    <system:String x:Key="ViewName_GetFieldsContentToolTip">GetFields ToolTip</system:String>  

        public void GetFieldsWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetFieldsWITPEvent>().Publish(
                new GetFieldsWITPEventArgs()
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
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetProcesses Command

        public DelegateCommand GetProcessesWITPCommand { get; set; }
        public string GetProcessesWITPContent { get; set; } = "Get Processes (WITP)";
        public string GetProcessesWITPToolTip { get; set; } = "Get Processes (WITP) ToolTip";

        // Can get fancy and use Resources
        //public string GetProcessesContent { get; set; } = "ViewName_GetProcessesContent";
        //public string GetProcessesToolTip { get; set; } = "ViewName_GetProcessesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProcessesContent">GetProcesses</system:String>
        //    <system:String x:Key="ViewName_GetProcessesContentToolTip">GetProcesses ToolTip</system:String>  

        public void GetProcessesWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetProcessesEvent>().Publish(
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

            EventAggregator.GetEvent<GetRulesEvent>().Publish(
                new GetRulesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
                });

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

        public DelegateCommand GetStatesWITPCommand { get; set; }
        public string GetStatesWITPContent { get; set; } = "GetStates (WITP)";
        public string GetStatesWITPToolTip { get; set; } = "GetStates (WITP) ToolTip";

        // Can get fancy and use Resources
        //public string GetStatesWITPContent { get; set; } = "ViewName_GetStatesWITPContent";
        //public string GetStatesWITPToolTip { get; set; } = "ViewName_GetStatesWITPContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetStatesContent">GetStates (WITP)</system:String>
        //    <system:String x:Key="ViewName_GetStatesContentToolTip">GetStates (WITP) ToolTip</system:String>  

        public void GetStatesWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetStatesWITPEvent>().Publish(
                new GetStatesWITPEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetStatesWITPCanExecute()
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

              EventAggregator.GetEvent<GetSystemControlsEvent>().Publish(
                new GetSystemControlsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Process = _contextMainViewModel.Context.SelectedProcess
                    ,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
                });

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

            EventAggregator.GetEvent<GetWorkItemTypesWITPEvent>().Publish(
                new GetWorkItemTypesWITPEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess
                });

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

        #region GetWorkItemTypesBehaviors Command

        public DelegateCommand GetWorkItemTypesBehaviorsCommand { get; set; }
        public string GetWorkItemTypesBehaviorsContent { get; set; } = "GetWorkItemTypesBehaviors";
        public string GetWorkItemTypesBehaviorsToolTip { get; set; } = "GetWorkItemTypesBehaviors ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkItemTypesBehaviorsContent { get; set; } = "ViewName_GetWorkItemTypesBehaviorsContent";
        //public string GetWorkItemTypesBehaviorsToolTip { get; set; } = "ViewName_GetWorkItemTypesBehaviorsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkItemTypesBehaviorsContent">GetWorkItemTypesBehaviors</system:String>
        //    <system:String x:Key="ViewName_GetWorkItemTypesBehaviorsContentToolTip">GetWorkItemTypesBehaviors ToolTip</system:String>  

        public void GetWorkItemTypesBehaviorsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetWorkItemTypesBehaviorsEvent>().Publish(
                new GetWorkItemTypesBehaviorsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemType
                });

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool GetWorkItemTypesBehaviorsCanExecute()
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

        #endregion

        #region Work Item Tracking Process Template Category

        #endregion


        #endregion


        #region Protected Methods


        #endregion

        #region Private Methods


        #endregion

    }
}
