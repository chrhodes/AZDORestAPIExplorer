using System;

using AZDORestApiExplorer.Core.Events;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Git;
using AZDORestApiExplorer.WorkItemTracking.Core.Events;

using Prism.Commands;
using Prism.Events;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class CommandsGetViewModel : EventViewModelBase
    {
        #region Constructors, Initialization, and Load

        public CommandsGetViewModel(
            ICollectionMainViewModel collectionMainViewModel,
            ContextMainViewModel contextMainViewModel,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _collectionMainViewModel = (CollectionMainViewModel)collectionMainViewModel;
            _contextMainViewModel = (ContextMainViewModel)contextMainViewModel;

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            #region Core Category

            GetCoreProcessesCommand = new DelegateCommand(GetCoreProcessesExecute, GetCoreProcessesCanExecute);
            GetProjectsCommand = new DelegateCommand(GetProjectsExecute, GetProjectsCanExecute);
            GetTeamsCommand = new DelegateCommand(GetTeamsExecute, GetTeamsCanExecute);

            #endregion Core Category

            #region Accounts Category

            GetAccountsCommand = new DelegateCommand(GetAccountsExecute, GetAccountsCanExecute);

            #endregion Accounts Category



            #region Dashboard Category

            GetDashboardsCommand = new DelegateCommand(GetDashboardsExecute, GetDashboardsCanExecute);

            GetWidgetsCommand = new DelegateCommand(GetWidgetsExecute, GetWidgetsCanExecute);

            #endregion Dashboard Category



            #region Git Category

            GetRepositoriesCommand = new DelegateCommand(GetRepositoriesExecute, GetRepositoriesCanExecute);
            GetProjectRepositoriesCommand = new DelegateCommand(GetProjectRepositoriesExecute, GetProjectRepositoriesCanExecute);

            GetBlobsCommand = new DelegateCommand(GetBlobs, GetBlobsCanExecute);
            GetCommitsCommand = new DelegateCommand(GetCommits, GetCommitsCanExecute);
            GetImportRequestsCommand = new DelegateCommand(GetImportRequests, GetImportRequestsCanExecute);
            GetItemsCommand = new DelegateCommand(GetItems, GetItemsCanExecute);
            GetMergesCommand = new DelegateCommand(GetMerges, GetMergesCanExecute);
            GetPushesCommand = new DelegateCommand(GetPushes, GetPushesCanExecute); GetPullRequestsCommand = new DelegateCommand(GetPullRequests, GetPullRequestsCanExecute);
            GetStatsCommand = new DelegateCommand(GetStats, GetStatsCanExecute);
            GetRefsCommand = new DelegateCommand(GetRefs, GetRefsCanExecute);

            #endregion Git Category



            #region Work Item Tracking Category

            // Organization Level

            GetOrganizationFieldsWITCommand = new DelegateCommand(GetOrganizationFieldsWITExecute, GetOrganizationFieldsWITCanExecute);

            // Organization + Project Level

            GetArtifactLinkTypesCommand = new DelegateCommand(GetArtifactLinkTypesExecute, GetArtifactLinkTypesCanExecute);
            GetClassificationNodesCommand = new DelegateCommand(GetClassificationNodesExecute, GetClassificationNodesCanExecute);
            GetProjectFieldsWITCommand = new DelegateCommand(GetProjectFieldsWITExecute, GetProjectFieldsWITCanExecute);
            GetQueriesCommand = new DelegateCommand(GetQueriesExecute, GetQueriesCanExecute);
            GetTagsCommand = new DelegateCommand(GetTagsExecute, GetTagsCanExecute);
            GetTemplatesCommand = new DelegateCommand(GetTemplatesExecute, GetTemplatesCanExecute);
            GetWorkItemIconsCommand = new DelegateCommand(GetWorkItemIconsExecute, GetWorkItemIconsCanExecute);
            GetWorkItemRelationTypesCommand = new DelegateCommand(GetWorkItemRelationTypesExecute, GetWorkItemRelationTypesCanExecute);
            GetWorkItemTypeCategoriesCommand = new DelegateCommand(GetWorkItemTypeCategoriesExecute, GetWorkItemTypeCategoriesCanExecute);
            GetStatesWITCommand = new DelegateCommand(GetStatesWITExecute, GetStatesWITCanExecute);
            GetWorkItemTypesWITCommand = new DelegateCommand(GetWorkItemTypesWITExecute, GetWorkItemTypesWITCanExecute);
            GetWorkItemTypesFieldsCommand = new DelegateCommand(GetWorkItemTypesFieldsExecute, GetWorkItemTypesFieldsCanExecute);

            #endregion Work Item Tracking Category

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

            #endregion Work Item Tracking Process Category

            EventAggregator.GetEvent<SelectedCollectionChangedEvent>().Subscribe(RaiseCollectionChanged);

            EventAggregator.GetEvent<Core.Events.Core.SelectedProcessChangedEvent>().Subscribe(RaiseProcessChanged);
            EventAggregator.GetEvent<Core.Events.Core.SelectedProjectChangedEvent>().Subscribe(RaiseProjectChanged);
            EventAggregator.GetEvent<Core.Events.Core.SelectedTeamChangedEvent>().Subscribe(RaiseTeamChanged);

            EventAggregator.GetEvent<Core.Events.Git.SelectedRepositoryChangedEvent>().Subscribe(RaiseRepositoryChanged);

            EventAggregator.GetEvent<Core.Events.WorkItemTracking.SelectedWorkItemTypeWITChangedEvent>().Subscribe(RaiseWorkItemTypeWITChanged);
            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.SelectedWorkItemTypeWITPChangedEvent>().Subscribe(RaiseWorkItemTypeWITPChanged);

            EventAggregator.GetEvent<Core.Events.Dashboard.SelectedDashboardChangedEvent>().Subscribe(RaiseDashboardChanged);

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
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

            // Git

            GetRepositoriesCommand.RaiseCanExecuteChanged();

            // Work Item Tracking
            GetArtifactLinkTypesCommand.RaiseCanExecuteChanged();
            GetOrganizationFieldsWITCommand.RaiseCanExecuteChanged();
            GetWorkItemIconsCommand.RaiseCanExecuteChanged();
            GetWorkItemRelationTypesCommand.RaiseCanExecuteChanged();

            // Work Item Tracking Process
            GetListsCommand.RaiseCanExecuteChanged();
        }

        private void RaiseDashboardChanged(Domain.Dashboard.Dashboard dashboard)
        {
            GetWidgetsCommand.RaiseCanExecuteChanged();
        }

        private void RaiseProcessChanged(Domain.Core.Process process)
        {
            GetWorkItemTypesWITPCommand.RaiseCanExecuteChanged();

            GetBehaviorsWITPCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();
        }

        private void RaiseProjectChanged(Project project)
        {
            GetProjectsCommand.RaiseCanExecuteChanged();
            GetDashboardsCommand.RaiseCanExecuteChanged();
            GetWidgetsCommand.RaiseCanExecuteChanged();

            // Git

            GetProjectRepositoriesCommand.RaiseCanExecuteChanged();
            GetPullRequestsCommand.RaiseCanExecuteChanged();

            GetBlobsCommand.RaiseCanExecuteChanged();
            GetCommitsCommand.RaiseCanExecuteChanged();
            GetImportRequestsCommand.RaiseCanExecuteChanged();
            GetItemsCommand.RaiseCanExecuteChanged();
            GetMerges.RaiseCanExecuteChanged();
            GetPushesCommand.RaiseCanExecuteChanged();
            GetRefsCommand.RaiseCanExecuteChanged();
            GetStatsCommand.RaiseCanExecuteChanged();

            // Work Item Tracking

            GetClassificationNodesCommand.RaiseCanExecuteChanged();
            GetProjectFieldsWITCommand.RaiseCanExecuteChanged();
            GetQueriesCommand.RaiseCanExecuteChanged();
            GetTagsCommand.RaiseCanExecuteChanged();
            GetTemplatesCommand.RaiseCanExecuteChanged();
            GetWorkItemRelationTypesCommand.RaiseCanExecuteChanged();
            GetWorkItemTypeCategoriesCommand.RaiseCanExecuteChanged();
            GetStatesWITCommand.RaiseCanExecuteChanged();
            GetWorkItemTypesWITCommand.RaiseCanExecuteChanged();
            GetWorkItemTypesFieldsCommand.RaiseCanExecuteChanged();
        }

        private void RaiseRepositoryChanged(Repository repository)
        {
            GetPullRequestsCommand.RaiseCanExecuteChanged();

            GetBlobsCommand.RaiseCanExecuteChanged();
            GetCommitsCommand.RaiseCanExecuteChanged();
            GetImportRequestsCommand.RaiseCanExecuteChanged();
            GetItemsCommand.RaiseCanExecuteChanged();
            GetMerges.RaiseCanExecuteChanged();
            GetPushesCommand.RaiseCanExecuteChanged();
            GetRefsCommand.RaiseCanExecuteChanged();
            GetStatsCommand.RaiseCanExecuteChanged();
        }

        private void RaiseTeamChanged(Team team)
        {
            GetTeamsCommand.RaiseCanExecuteChanged();
            GetDashboardsCommand.RaiseCanExecuteChanged();
            GetWidgetsCommand.RaiseCanExecuteChanged();

            // Work Item Tracking

            GetTemplatesCommand.RaiseCanExecuteChanged();
        }

        private void RaiseWorkItemTypeWITChanged(Domain.WorkItemTracking.WorkItemType workItemType)
        {
            GetProjectFieldsWITCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();
            GetStatesWITPCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();

            // Work Item Tracking

            GetWorkItemTypesFieldsCommand.RaiseCanExecuteChanged();
            GetStatesWITCommand.RaiseCanExecuteChanged();
        }

        private void RaiseWorkItemTypeWITPChanged(Domain.WorkItemTrackingProcess.WorkItemType workItemType)
        {
            GetFieldsWITPCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();
            GetStatesWITPCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetWorkItemTypesBehaviorsCommand.RaiseCanExecuteChanged();
        }

        #endregion Constructors, Initialization, and Load



        #region Fields and Properties

        private CollectionMainViewModel _collectionMainViewModel;
        private ContextMainViewModel _contextMainViewModel;

        #endregion Fields and Properties



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

        public bool GetAccountsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetAccountsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Accounts.GetAccountsEvent>().Publish(
                new Core.Events.Accounts.GetAccountsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    //, Project = _contextMainViewModel.Context.SelectedProject
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetAccounts Command

        #endregion Accounts Category



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

        public bool GetCoreProcessesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetCoreProcessesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Core.GetProcessesEvent>().Publish(
                new Core.Events.Core.GetProcessesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetCoreProcesses Command

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

        public bool GetProjectsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetProjectsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetProjectsEvent>().Publish(
                new GetProjectsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetCoreProjects Command

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

        public bool GetTeamsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetTeamsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetTeamsEvent>().Publish(
                new GetTeamsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetTeams Command

        #endregion Core

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

        public void GetDashboardsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Dashboard.GetDashboardsEvent>().Publish(
                new Core.Events.Dashboard.GetDashboardsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetDashboards Command

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

        public void GetWidgetsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetWidgetsEvent>().Publish(
                new GetWidgetsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetWidgets Command

        #endregion Dashboard Category



        #region Git Category

        #region GetRepositories Command

        public DelegateCommand GetRepositoriesCommand { get; set; }
        public string GetRepositoriesContent { get; set; } = "Get Repositories";
        public string GetRepositoriesToolTip { get; set; } = "Get Repositories ToolTip";

        // Can get fancy and use Resources
        //public string GetAccountsContent { get; set; } = "ViewName_GetAccountsContent";
        //public string GetAccountsToolTip { get; set; } = "ViewName_GetAccountsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetAccountsContent">GetAccounts</system:String>
        //    <system:String x:Key="ViewName_GetAccountsContentToolTip">GetAccounts ToolTip</system:String>

        public bool GetRepositoriesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetRepositoriesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Git.GetRepositoriesEvent>().Publish(
                new Core.Events.Git.GetRepositoriesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    //, Project = _contextMainViewModel.Context.SelectedProject
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetRepositories Command

        #region GetProjectRepositories Command

        public DelegateCommand GetProjectRepositoriesCommand { get; set; }
        public string GetProjectRepositoriesContent { get; set; } = "Get Project Repositories";
        public string GetProjectRepositoriesToolTip { get; set; } = "Get Project Repositories ToolTip";

        // Can get fancy and use Resources
        //public string GetAccountsContent { get; set; } = "ViewName_GetAccountsContent";
        //public string GetAccountsToolTip { get; set; } = "ViewName_GetAccountsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetAccountsContent">GetAccounts</system:String>
        //    <system:String x:Key="ViewName_GetAccountsContentToolTip">GetAccounts ToolTip</system:String>

        public bool GetProjectRepositoriesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        public void GetProjectRepositoriesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Git.GetProjectRepositoriesEvent>().Publish(
                new Core.Events.Git.GetProjectRepositoriesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetProjectRepositories Command

        #region GetBlobs Command

        public DelegateCommand GetBlobsCommand { get; set; }
        public string GetBlobsContent { get; set; } = "Get Blobs";
        public string GetBlobsToolTip { get; set; } = "Get Blobs ToolTip";

        // Can get fancy and use Resources
        //public string GetBlobsContent { get; set; } = "ViewName_GetBlobsContent";
        //public string GetBlobsToolTip { get; set; } = "ViewName_GetBlobsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetBlobsContent">GetBlobs</system:String>
        //    <system:String x:Key="ViewName_GetBlobsContentToolTip">GetBlobs ToolTip</system:String>

        public void GetBlobs()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Git.GetBlobsEvent>().Publish(
                new Core.Events.Git.GetBlobsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetBlobsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetBlobs Command

        #region GetCommits Command

        public DelegateCommand GetCommitsCommand { get; set; }
        public string GetCommitsContent { get; set; } = "GetCommits";
        public string GetCommitsToolTip { get; set; } = "GetCommits ToolTip";

        // Can get fancy and use Resources
        //public string GetCommitsContent { get; set; } = "ViewName_GetCommitsContent";
        //public string GetCommitsToolTip { get; set; } = "ViewName_GetCommitsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetCommitsContent">GetCommits</system:String>
        //    <system:String x:Key="ViewName_GetCommitsContentToolTip">GetCommits ToolTip</system:String>

        public void GetCommits()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Git.GetCommitsEvent>().Publish(
                new Core.Events.Git.GetCommitsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetCommitsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetCommits Command

        #region GetImportRequests Command

        public DelegateCommand GetImportRequestsCommand { get; set; }
        public string GetImportRequestsContent { get; set; } = "GetImportRequests";
        public string GetImportRequestsToolTip { get; set; } = "GetImportRequests ToolTip";

        // Can get fancy and use Resources
        //public string GetImportRequestsContent { get; set; } = "ViewName_GetImportRequestsContent";
        //public string GetImportRequestsToolTip { get; set; } = "ViewName_GetImportRequestsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetImportRequestsContent">GetImportRequests</system:String>
        //    <system:String x:Key="ViewName_GetImportRequestsContentToolTip">GetImportRequests ToolTip</system:String>

        public void GetImportRequests()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Git.GetImportRequestsEvent>().Publish(
                new Core.Events.Git.GetImportRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetImportRequestsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetImportRequests Command

        #region GetItems Command

        public DelegateCommand GetItemsCommand { get; set; }
        public string GetItemsContent { get; set; } = "GetItems";
        public string GetItemsToolTip { get; set; } = "GetItems ToolTip";

        // Can get fancy and use Resources
        //public string GetItemsContent { get; set; } = "ViewName_GetItemsContent";
        //public string GetItemsToolTip { get; set; } = "ViewName_GetItemsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetItemsContent">GetItems</system:String>
        //    <system:String x:Key="ViewName_GetItemsContentToolTip">GetItems ToolTip</system:String>

        public void GetItems()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Git.GetItemsEvent>().Publish(
                new Core.Events.Git.GetItemsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetItemsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetItems Command

        #region GetMerges Command

        public DelegateCommand GetMergesCommand { get; set; }
        public string GetMergesContent { get; set; } = "GetMerges";
        public string GetMergesToolTip { get; set; } = "GetMerges ToolTip";

        // Can get fancy and use Resources
        //public string GetMergesContent { get; set; } = "ViewName_GetMergesContent";
        //public string GetMergesToolTip { get; set; } = "ViewName_GetMergesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetMergesContent">GetMerges</system:String>
        //    <system:String x:Key="ViewName_GetMergesContentToolTip">GetMerges ToolTip</system:String>

        public void GetMerges()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Git.GetMergesEvent>().Publish(
                new Core.Events.Git.GetMergesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetMergesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetMerges Command

        #region GetPullRequests Command

        public DelegateCommand GetPullRequestsCommand { get; set; }
        public string GetPullRequestsContent { get; set; } = "GetPullRequests";
        public string GetPullRequestsToolTip { get; set; } = "GetPullRequests ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestsContent { get; set; } = "ViewName_GetPullRequestsContent";
        //public string GetPullRequestsToolTip { get; set; } = "ViewName_GetPullRequestsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestsContent">GetPullRequests</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestsContentToolTip">GetPullRequests ToolTip</system:String>

        public void GetPullRequests()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Git.GetPullRequestsEvent>().Publish(
                new Core.Events.Git.GetPullRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetPullRequestsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetPullRequests Command

        #region GetPushes Command

        public DelegateCommand GetPushesCommand { get; set; }
        public string GetPushesContent { get; set; } = "Get Pushes";
        public string GetPushesToolTip { get; set; } = "Get Pushes ToolTip";

        // Can get fancy and use Resources
        //public string GetPushesContent { get; set; } = "ViewName_GetPushesContent";
        //public string GetPushesToolTip { get; set; } = "ViewName_GetPushesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPushesContent">GetPushes</system:String>
        //    <system:String x:Key="ViewName_GetPushesContentToolTip">GetPushes ToolTip</system:String>

        public void GetPushes()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Git.GetPushesEvent>().Publish(
                new Core.Events.Git.GetPushesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetPushesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetPushes Command

        // End Cut One

        #region GetRefs Command

        public DelegateCommand GetRefsCommand { get; set; }
        public string GetRefsContent { get; set; } = "GetRefs";
        public string GetRefsToolTip { get; set; } = "GetRefs ToolTip";

        // Can get fancy and use Resources
        //public string GetRefsContent { get; set; } = "ViewName_GetRefsContent";
        //public string GetRefsToolTip { get; set; } = "ViewName_GetRefsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetRefsContent">GetRefs</system:String>
        //    <system:String x:Key="ViewName_GetRefsContentToolTip">GetRefs ToolTip</system:String>

        public void GetRefs()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Git.GetRefsEvent>().Publish(
                new Core.Events.Git.GetRefsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetRefsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetRefs Command

        // End Cut One

        #region GetStats Command

        public DelegateCommand GetStatsCommand { get; set; }
        public string GetStatsContent { get; set; } = "Get Stats";
        public string GetStatsToolTip { get; set; } = "Get Stats ToolTip";

        // Can get fancy and use Resources
        //public string GetStatsContent { get; set; } = "ViewName_GetStatsContent";
        //public string GetStatsToolTip { get; set; } = "ViewName_GetStatsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetStatsContent">GetStats</system:String>
        //    <system:String x:Key="ViewName_GetStatsContentToolTip">GetStats ToolTip</system:String>

        public void GetStats()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.Git.GetStatsEvent>().Publish(
                new Core.Events.Git.GetStatsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetStatsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedRepository is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetStats Command

        // End Cut One

        #endregion Git Category



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

        public bool GetArtifactLinkTypesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetArtifactLinkTypesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTracking.GetArtifactLinkTypesEvent>().Publish(
                new Core.Events.WorkItemTracking.GetArtifactLinkTypesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetArtifactLinkTypes Command

        #region GetClassificationNodes Command

        public DelegateCommand GetClassificationNodesCommand { get; set; }
        public string GetClassificationNodesContent { get; set; } = "Get Classification Nodes";
        public string GetClassificationNodesToolTip { get; set; } = "Get Classification Nodes ToolTip";

        // Can get fancy and use Resources
        //public string GetClassificationNodesContent { get; set; } = "ViewName_GetClassificationNodesContent";
        //public string GetClassificationNodesToolTip { get; set; } = "ViewName_GetClassificationNodesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetClassificationNodesContent">GetClassificationNodes</system:String>
        //    <system:String x:Key="ViewName_GetClassificationNodesContentToolTip">GetClassificationNodes ToolTip</system:String>

        public bool GetClassificationNodesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        public void GetClassificationNodesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTracking.GetClassificationNodesEvent>().Publish(
                new Core.Events.WorkItemTracking.GetClassificationNodesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetClassificationNodes Command

        #region GetFields Command

        public DelegateCommand GetOrganizationFieldsWITCommand { get; set; }
        public string GetOrganizationFieldsWITContent { get; set; } = "GetFields (Organization)";
        public string GetOrganizationFieldsWITToolTip { get; set; } = "GetFields (Organization) ToolTip";

        // Can get fancy and use Resources
        //public string GetOrganizationFieldsContent { get; set; } = "ViewName_GetFieldsContent";
        //public string GetOrganizationFieldsToolTip { get; set; } = "ViewName_GetFieldsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetFieldsContent">GetFields</system:String>
        //    <system:String x:Key="ViewName_GetFieldsContentToolTip">GetFields ToolTip</system:String>

        public DelegateCommand GetProjectFieldsWITCommand { get; set; }

        public string GetProjectFieldsWITContent { get; set; } = "GetFields (Project)";

        public string GetProjectFieldsWITToolTip { get; set; } = "GetFields (Project) ToolTip";

        public bool GetOrganizationFieldsWITCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetOrganizationFieldsWITExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTracking.GetFieldsWITEvent>().Publish(
                new Core.Events.WorkItemTracking.GetFieldsWITEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // Can get fancy and use Resources
        //public string GetFieldsContent { get; set; } = "ViewName_GetFieldsContent";
        //public string GetFieldsToolTip { get; set; } = "ViewName_GetFieldsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetFieldsContent">GetFields</system:String>
        //    <system:String x:Key="ViewName_GetFieldsContentToolTip">GetFields ToolTip</system:String>

        public bool GetProjectFieldsWITCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        public void GetProjectFieldsWITExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTracking.GetFieldsWITEvent>().Publish(
                new Core.Events.WorkItemTracking.GetFieldsWITEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetFields Command

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

        public bool GetQueriesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        public void GetQueriesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTracking.GetQueriesEvent>().Publish(
                new Core.Events.WorkItemTracking.GetQueriesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetQueries Command

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

        public bool GetTagsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        public void GetTagsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetTagsEvent>().Publish(
                new GetTagsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetTags Command

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

        public void GetTemplatesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetTemplatesEvent>().Publish(
                new GetTemplatesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetTemplates Command

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

        public bool GetWorkItemIconsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetWorkItemIconsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetWorkItemIconsEvent>().Publish(
                new GetWorkItemIconsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetWorkItemIcons Command

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

        public bool GetWorkItemRelationTypesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetWorkItemRelationTypesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetWorkItemRelationTypesEvent>().Publish(
                new GetWorkItemRelationTypesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetWorkRelationTypes Command

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

        public bool GetWorkItemTypeCategoriesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        public void GetWorkItemTypeCategoriesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTracking.GetWorkItemTypeCategoriesEvent>().Publish(
                new Core.Events.WorkItemTracking.GetWorkItemTypeCategoriesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetWorkItemTypeCategories Command

        #region GetStates Command

        public DelegateCommand GetStatesWITCommand { get; set; }
        public string GetStatesWITContent { get; set; } = "GetStates";
        public string GetStatesWITToolTip { get; set; } = "GetStates ToolTip";

        // Can get fancy and use Resources
        //public string GetStatesWITPContent { get; set; } = "ViewName_GetStatesWITContent";
        //public string GetStatesWITPToolTip { get; set; } = "ViewName_GetStatesWITContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetStatesContent">GetStates</system:String>
        //    <system:String x:Key="ViewName_GetStatesContentToolTip">GetStates ToolTip</system:String>

        public bool GetStatesWITCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWIT is null)
            {
                return false;
            }

            return true;
        }

        public void GetStatesWITExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTracking.GetStatesWITEvent>().Publish(
                new Core.Events.WorkItemTracking.GetStatesWITEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWIT
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetStates Command

        #region GetWorkItemTypesWIT Command

        public DelegateCommand GetWorkItemTypesWITCommand { get; set; }
        public string GetWorkItemTypesWITContent { get; set; } = "Get WorkItemTypes";
        public string GetWorkItemTypesWITToolTip { get; set; } = "Get WorkItemTypes ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkItemTypesWITContent { get; set; } = "ViewName_GetWorkItemTypesWITContent";
        //public string GetWorkItemTypesWITToolTip { get; set; } = "ViewName_GetWorkItemTypesWITContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkItemTypesWITContent">GetWorkItemTypesWIT</system:String>
        //    <system:String x:Key="ViewName_GetWorkItemTypesWITContentToolTip">GetWorkItemTypesWIT ToolTip</system:String>

        public bool GetWorkItemTypesWITCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        public void GetWorkItemTypesWITExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTracking.GetWorkItemTypesWITEvent>().Publish(
                new Core.Events.WorkItemTracking.GetWorkItemTypesWITEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetWorkItemTypesWIT Command

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

        public bool GetWorkItemTypesFieldsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWIT is null)
            {
                return false;
            }

            return true;
        }

        public void GetWorkItemTypesFieldsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTracking.GetWorkItemTypesFieldsWITEvent>().Publish(
                new Core.Events.WorkItemTracking.GetWorkItemTypesFieldsWITEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWIT
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetWorkItemTypesFields Command

        #endregion Work Item Tracking Category

        #endregion Commands

        #region Work Item Tracking Process Category

        #region GetBehaviors Command

        public DelegateCommand GetBehaviorsWITPCommand { get; set; }
        public string GetBehaviorsWITPContent { get; set; } = "GetBehaviors";
        public string GetBehaviorsWITPToolTip { get; set; } = "GetBehaviors ToolTip";

        // Can get fancy and use Resources
        //public string BehaviorContent { get; set; } = "ViewName_BehaviorContent";
        //public string BehaviorToolTip { get; set; } = "ViewName_BehaviorContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_BehaviorContent">Behavior</system:String>
        //    <system:String x:Key="ViewName_BehaviorContentToolTip">Behavior ToolTip</system:String>

        public bool GetBehaviorsWITPCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null)
            {
                return false;
            }

            return true;
        }

        public void GetBehaviorsWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.GetBehaviorsEvent>().Publish(
                new Core.Events.WorkItemTrackingProcess.GetBehaviorsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Process = _contextMainViewModel.Context.SelectedProcess
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetBehaviors Command

        #region GetFields Command

        public DelegateCommand GetFieldsWITPCommand { get; set; }
        public string GetFieldsWITPContent { get; set; } = "Get Fields";
        public string GetFieldsWITPToolTip { get; set; } = "Get Fields ToolTip";

        // Can get fancy and use Resources
        //public string GetFieldsContent { get; set; } = "ViewName_GetFieldsContent";
        //public string GetFieldsToolTip { get; set; } = "ViewName_GetFieldsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetFieldsContent">GetFields</system:String>
        //    <system:String x:Key="ViewName_GetFieldsContentToolTip">GetFields ToolTip</system:String>

        public bool GetFieldsWITPCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWITP is null)
            {
                return false;
            }

            return true;
        }

        public void GetFieldsWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.GetFieldsWITPEvent>().Publish(
                new Core.Events.WorkItemTrackingProcess.GetFieldsWITPEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWITP
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetFields Command

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

        public bool GetListsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetListsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.GetListsEvent>().Publish(
                new Core.Events.WorkItemTrackingProcess.GetListsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetLists Command

        #region GetProcesses Command

        public DelegateCommand GetProcessesWITPCommand { get; set; }
        public string GetProcessesWITPContent { get; set; } = "Get Processes";
        public string GetProcessesWITPToolTip { get; set; } = "Get Processes ToolTip";

        // Can get fancy and use Resources
        //public string GetProcessesContent { get; set; } = "ViewName_GetProcessesContent";
        //public string GetProcessesToolTip { get; set; } = "ViewName_GetProcessesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProcessesContent">GetProcesses</system:String>
        //    <system:String x:Key="ViewName_GetProcessesContentToolTip">GetProcesses ToolTip</system:String>

        public bool GetProcessesWITPCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetProcessesWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.GetProcessesEvent>().Publish(
                new Core.Events.WorkItemTrackingProcess.GetProcessesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetProcesses Command

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

        public bool GetRulesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWITP is null)
            {
                return false;
            }

            return true;
        }

        public void GetRulesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.GetRulesEvent>().Publish(
                new Core.Events.WorkItemTrackingProcess.GetRulesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWITP
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetRules Command

        #region GetStates Command

        public DelegateCommand GetStatesWITPCommand { get; set; }
        public string GetStatesWITPContent { get; set; } = "GetStates";
        public string GetStatesWITPToolTip { get; set; } = "GetStates ToolTip";

        // Can get fancy and use Resources
        //public string GetStatesWITPContent { get; set; } = "ViewName_GetStatesWITPContent";
        //public string GetStatesWITPToolTip { get; set; } = "ViewName_GetStatesWITPContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetStatesContent">GetStates</system:String>
        //    <system:String x:Key="ViewName_GetStatesContentToolTip">GetStates ToolTip</system:String>

        public bool GetStatesWITPCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWITP is null)
            {
                return false;
            }

            return true;
        }

        public void GetStatesWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.GetStatesWITPEvent>().Publish(
                new Core.Events.WorkItemTrackingProcess.GetStatesWITPEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWITP
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetStates Command

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

        public bool GetSystemControlsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWITP is null)
            {
                return false;
            }

            return true;
        }

        public void GetSystemControlsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.GetSystemControlsEvent>().Publish(
              new Core.Events.WorkItemTrackingProcess.GetSystemControlsEventArgs()
              {
                  Organization = _collectionMainViewModel.SelectedCollection.Organization
                  ,
                  Process = _contextMainViewModel.Context.SelectedProcess
                  ,
                  WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWITP
              });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetSystemControls Command

        #region GetWorkItemTypes Command

        public DelegateCommand GetWorkItemTypesWITPCommand { get; set; }
        public string GetWorkItemTypesWITPContent { get; set; } = "GetWorkItemTypes";
        public string GetWorkItemTypesWITPToolTip { get; set; } = "GetWorkItemTypes ToolTip";

        // Can get fancy and use Resources
        //public string GetProcessesContent { get; set; } = "ViewName_GetProcessesContent";
        //public string GetProcessesToolTip { get; set; } = "ViewName_GetProcessesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProcessesContent">GetProcesses</system:String>
        //    <system:String x:Key="ViewName_GetProcessesContentToolTip">GetProcesses ToolTip</system:String>

        public bool GetWorkItemTypesWITPCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null)
            {
                return false;
            }

            return true;
        }

        public void GetWorkItemTypesWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.GetWorkItemTypesWITPEvent>().Publish(
                new Core.Events.WorkItemTrackingProcess.GetWorkItemTypesWITPEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetWorkItemTypes Command

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

        public bool GetWorkItemTypesBehaviorsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWITP is null)
            {
                return false;
            }

            return true;
        }

        public void GetWorkItemTypesBehaviorsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Core.Events.WorkItemTrackingProcess.GetWorkItemTypesBehaviorsEvent>().Publish(
                new Core.Events.WorkItemTrackingProcess.GetWorkItemTypesBehaviorsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWITP
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetWorkItemTypesBehaviors Command

        #endregion Work Item Tracking Process Category

        #endregion Public Methods
    }
}