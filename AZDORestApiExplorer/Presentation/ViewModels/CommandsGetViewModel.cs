using System;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain.Build.Events;
using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Core.Events;
using AZDORestApiExplorer.Domain.Dashboard.Events;
using AZDORestApiExplorer.Domain.Git;
using AZDORestApiExplorer.Domain.Test;
using AZDORestApiExplorer.Domain.Tokens.Events;
using AZDORestApiExplorer.Domain.WorkItemTracking.Events;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess.Events;

using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class CommandsGetViewModel : EventViewModelBase, IInstanceCountVM
    {
        #region Constructors, Initialization, and Load

        public CommandsGetViewModel(
            CollectionMainViewModel collectionMainViewModel,
            ContextMainViewModel contextMainViewModel,
            IEventAggregator eventAggregator,
            DialogService dialogService) : base(eventAggregator, dialogService)
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

            InstanceCountVM++;

            #region Core Area

            GetCoreProcessesCommand = new DelegateCommand(GetCoreProcessesExecute, GetCoreProcessesCanExecute);
            GetProjectsCommand = new DelegateCommand(GetProjectsExecute, GetProjectsCanExecute);
            GetTeamsCommand = new DelegateCommand(GetTeamsExecute, GetTeamsCanExecute);

            #endregion Core Category

            #region Accounts Area

            GetAccountsCommand = new DelegateCommand(GetAccountsExecute, GetAccountsCanExecute);

            #endregion Accounts Category

            #region Artifacts Area

            GetFeedsCommand = new DelegateCommand(GetFeeds, GetFeedsCanExecute);

            #endregion

            #region Build Area
            
            GetAuthorizedResourcesCommand = new DelegateCommand(GetAuthorizedResources, GetAuthorizedResourcesCanExecute);
            GetBuildsCommand = new DelegateCommand(GetBuilds, GetBuildsCanExecute);
            GetControllersCommand = new DelegateCommand(GetControllers, GetControllersCanExecute);
            GetDefinitionsCommand = new DelegateCommand(GetDefinitions, GetDefinitionsCanExecute);
            GetGeneralSettingsCommand = new DelegateCommand(GetGeneralSettings, GetGeneralSettingsCanExecute);
            GetOptionsCommand = new DelegateCommand(GetOptions, GetOptionsCanExecute);
            GetResourcesCommand = new DelegateCommand(GetResources, GetResourcesCanExecute);
            GetSettingsCommand = new DelegateCommand(GetSettings, GetSettingsCanExecute);

            #endregion

            #region Dashboard Area

            GetDashboardsCommand = new DelegateCommand(GetDashboardsExecute, GetDashboardsCanExecute);

            GetWidgetsCommand = new DelegateCommand(GetWidgetsExecute, GetWidgetsCanExecute);

            #endregion Dashboard Category

            #region Git Area

            GetRepositoriesCommand = new DelegateCommand(GetRepositoriesExecute, GetRepositoriesCanExecute);
            GetProjectRepositoriesCommand = new DelegateCommand(GetProjectRepositoriesExecute, GetProjectRepositoriesCanExecute);

            GetBlobsCommand = new DelegateCommand(GetBlobs, GetBlobsCanExecute);
            GetCommitsCommand = new DelegateCommand(GetCommits, GetCommitsCanExecute);
            GetCommitChangesCommand = new DelegateCommand(GetCommitChanges, GetCommitChangesCanExecute);

            GetImportRequestsCommand = new DelegateCommand(GetImportRequests, GetImportRequestsCanExecute);
            GetItemsCommand = new DelegateCommand(GetItems, GetItemsCanExecute);
            GetMergesCommand = new DelegateCommand(GetMerges, GetMergesCanExecute);

            GetPullRequestsCommand = new DelegateCommand(GetPullRequests, GetPullRequestsCanExecute);

            GetPullRequestAttachmentsCommand = new DelegateCommand(GetPullRequestAttachments, GetPullRequestInfoCanExecute);
            GetPullRequestCommitsCommand = new DelegateCommand(GetPullRequestCommits, GetPullRequestInfoCanExecute);
            GetPullRequestIterationsCommand = new DelegateCommand(GetPullRequestIterations, GetPullRequestInfoCanExecute);
            GetPullRequestLabelsCommand = new DelegateCommand(GetPullRequestLabels, GetPullRequestInfoCanExecute);
            GetPullRequestPropertiesCommand = new DelegateCommand(GetPullRequestProperties, GetPullRequestInfoCanExecute);
            GetPullRequestReviewersCommand = new DelegateCommand(GetPullRequestReviewers, GetPullRequestInfoCanExecute);
            GetPullRequestStatusesCommand = new DelegateCommand(GetPullRequestStatuses, GetPullRequestInfoCanExecute);
            GetPullRequestThreadsCommand = new DelegateCommand(GetPullRequestThreads, GetPullRequestInfoCanExecute);
            GetPullRequestWorkItemsCommand = new DelegateCommand(GetPullRequestWorkItems, GetPullRequestInfoCanExecute);


            GetPushesCommand = new DelegateCommand(GetPushes, GetPushesCanExecute);
            GetStatsCommand = new DelegateCommand(GetStats, GetStatsCanExecute);
            GetRefsCommand = new DelegateCommand(GetRefs, GetRefsCanExecute);

            #endregion Git Category

            #region Test Category

            GetTestPlansCommand = new DelegateCommand(GetTestPlans, GetTestPlansCanExecute);
            GetTestSuitesCommand = new DelegateCommand(GetTestSuites, GetTestSuitesCanExecute);
            GetTestCasesCommand = new DelegateCommand(GetTestCases, GetTestCasesCanExecute);

            GetTestPointsCommand = new DelegateCommand(GetTestPoints, GetTestPointsCanExecute);

            #endregion Test Category

            #region Tokens Area

            GetPatsCommand = new DelegateCommand(GetPats, GetPatsCanExecute);

            #endregion

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

            GetWorkItemCommand = new DelegateCommand(GetWorkItem, GetWorkItemCanExecute);

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

            #region Context Enablement

            EventAggregator.GetEvent<SelectedCollectionChangedEvent>().Subscribe(RaiseCollectionChanged);

            EventAggregator.GetEvent<SelectedProcessChangedEvent>().Subscribe(RaiseProcessChanged);
            EventAggregator.GetEvent<SelectedProjectChangedEvent>().Subscribe(RaiseProjectChanged);
            EventAggregator.GetEvent<SelectedTeamChangedEvent>().Subscribe(RaiseTeamChanged);

            EventAggregator.GetEvent<SelectedBuildChangedEvent>().Subscribe(RaiseBuildChanged);

            EventAggregator.GetEvent<SelectedDefinitionChangedEvent>().Subscribe(RaiseDefinitionChanged);

            EventAggregator.GetEvent<Domain.Git.Events.SelectedRepositoryChangedEvent>().Subscribe(RaiseRepositoryChanged);
            EventAggregator.GetEvent<Domain.Git.Events.SelectedCommitChangedEvent>().Subscribe(RaiseCommitChanged);
            EventAggregator.GetEvent<Domain.Git.Events.SelectedPullRequestChangedEvent>().Subscribe(RaisePullRequestChanged);

            EventAggregator.GetEvent<Domain.Test.Events.SelectedTestPlanChangedEvent>().Subscribe(RaiseTestPlanChanged);
            EventAggregator.GetEvent<Domain.Test.Events.SelectedTestSuiteChangedEvent>().Subscribe(RaiseTestSuiteChanged);
            EventAggregator.GetEvent<Domain.Test.Events.SelectedTestCaseChangedEvent>().Subscribe(RaiseTestCaseChanged);

            EventAggregator.GetEvent<SelectedWorkItemTypeWITChangedEvent>().Subscribe(RaiseWorkItemTypeWITChanged);
            EventAggregator.GetEvent<SelectedWorkItemTypeWITPChangedEvent>().Subscribe(RaiseWorkItemTypeWITPChanged);

            EventAggregator.GetEvent<SelectedDashboardChangedEvent>().Subscribe(RaiseDashboardChanged);

            #endregion

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // NOTE(crhodes)
        // RaiseCanExecuteChanged for any Command that is dependent on Context.
        // N.B. Need to add to each Context Item for button to be enabled.

        //++ Add Commands that only depend on Organization Context here

        // Other commands that depend on more do not need to be added
        // as the check is in all CanExecute methods

        private void RaiseCollectionChanged(SelectedCollectionChangedEventArgs args)
        {
            Int64 startTicks = Log.EVENT_HANDLER($"Enter: ({args.GetType()}) {args.Collection.Name}", Common.LOG_CATEGORY);

            GetCoreProcessesCommand.RaiseCanExecuteChanged();
            GetProjectsCommand.RaiseCanExecuteChanged();
            GetTeamsCommand.RaiseCanExecuteChanged();

            // Accounts

            GetAccountsCommand.RaiseCanExecuteChanged();

            // Approvals and Checks

            // Artifacts

            // Artifacts Package Types

            // Audit

            // Build

            GetControllersCommand.RaiseCanExecuteChanged();

            // Cloud Load Test

            // Dashboard

            GetDashboardsCommand.RaiseCanExecuteChanged();
            GetWidgetsCommand.RaiseCanExecuteChanged();

            // TODO(crhodes)
            // Move these under appropriate Category

            GetWidgetsCommand.RaiseCanExecuteChanged();

            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();

            // Git

            GetRepositoriesCommand.RaiseCanExecuteChanged();

            // Work Item Tracking (WIT)

            GetArtifactLinkTypesCommand.RaiseCanExecuteChanged();
            GetOrganizationFieldsWITCommand.RaiseCanExecuteChanged();
            GetWorkItemIconsCommand.RaiseCanExecuteChanged();
            GetWorkItemRelationTypesCommand.RaiseCanExecuteChanged();

            // Work Item Tracking Process (WITP)

            // TODO(crhodes)
            // Decide if want to have a naming convention
            // VERBAreaResource
            // e.g. GetWITPBehaviorsCommand
            // GetWITPListsCommand
            // Get WITPProcessesCommand

            GetBehaviorsWITPCommand.RaiseCanExecuteChanged();
            GetListsCommand.RaiseCanExecuteChanged();
            GetProcessesWITPCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaisePullRequestChanged(PullRequest pullRequest)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetPullRequestAttachmentsCommand.RaiseCanExecuteChanged();
            GetPullRequestCommitsCommand.RaiseCanExecuteChanged();
            GetPullRequestIterationsCommand.RaiseCanExecuteChanged();
            GetPullRequestLabelsCommand.RaiseCanExecuteChanged();
            GetPullRequestPropertiesCommand.RaiseCanExecuteChanged();
            GetPullRequestReviewersCommand.RaiseCanExecuteChanged();
            GetPullRequestStatusesCommand.RaiseCanExecuteChanged();
            GetPullRequestThreadsCommand.RaiseCanExecuteChanged();
            GetPullRequestWorkItemsCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseCommitChanged(Commit commit)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetCommitChangesCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseDashboardChanged(Domain.Dashboard.Dashboard dashboard)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetWidgetsCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseProcessChanged(Domain.Core.Process process)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetWorkItemTypesWITPCommand.RaiseCanExecuteChanged();

            GetBehaviorsWITPCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseProjectChanged(Project project)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetProjectsCommand.RaiseCanExecuteChanged();

            GetDashboardsCommand.RaiseCanExecuteChanged();
            GetWidgetsCommand.RaiseCanExecuteChanged();

            // Build

            GetAuthorizedResourcesCommand.RaiseCanExecuteChanged();
            GetBuildsCommand.RaiseCanExecuteChanged();
            GetDefinitionsCommand.RaiseCanExecuteChanged();
            GetGeneralSettingsCommand.RaiseCanExecuteChanged();
            GetOptionsCommand.RaiseCanExecuteChanged();
            GetResourcesCommand.RaiseCanExecuteChanged();
            GetSettingsCommand.RaiseCanExecuteChanged();

            // Git

            GetProjectRepositoriesCommand.RaiseCanExecuteChanged();
            GetPullRequestsCommand.RaiseCanExecuteChanged();

            GetBlobsCommand.RaiseCanExecuteChanged();
            GetCommitsCommand.RaiseCanExecuteChanged();
            GetImportRequestsCommand.RaiseCanExecuteChanged();
            GetItemsCommand.RaiseCanExecuteChanged();
            GetMergesCommand.RaiseCanExecuteChanged();
            GetPushesCommand.RaiseCanExecuteChanged();
            GetRefsCommand.RaiseCanExecuteChanged();
            GetStatsCommand.RaiseCanExecuteChanged();

            // Test

            GetTestPlansCommand.RaiseCanExecuteChanged();

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

            GetWorkItemCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseBuildChanged(Domain.Build.Build build)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseDefinitionChanged(Domain.Build.Definition definition)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetResourcesCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseRepositoryChanged(Repository repository)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetPullRequestsCommand.RaiseCanExecuteChanged();
            GetPullRequestAttachmentsCommand.RaiseCanExecuteChanged();
            GetPullRequestCommitsCommand.RaiseCanExecuteChanged();
            GetPullRequestIterationsCommand.RaiseCanExecuteChanged();
            GetPullRequestLabelsCommand.RaiseCanExecuteChanged();
            GetPullRequestPropertiesCommand.RaiseCanExecuteChanged();
            GetPullRequestReviewersCommand.RaiseCanExecuteChanged();
            GetPullRequestStatusesCommand.RaiseCanExecuteChanged();
            GetPullRequestThreadsCommand.RaiseCanExecuteChanged();
            GetPullRequestWorkItemsCommand.RaiseCanExecuteChanged();

            GetBlobsCommand.RaiseCanExecuteChanged();
            GetCommitsCommand.RaiseCanExecuteChanged();
            GetImportRequestsCommand.RaiseCanExecuteChanged();
            GetItemsCommand.RaiseCanExecuteChanged();
            GetMergesCommand.RaiseCanExecuteChanged();
            GetPushesCommand.RaiseCanExecuteChanged();
            GetRefsCommand.RaiseCanExecuteChanged();
            GetStatsCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseTeamChanged(Team team)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetTeamsCommand.RaiseCanExecuteChanged();
            GetDashboardsCommand.RaiseCanExecuteChanged();
            GetWidgetsCommand.RaiseCanExecuteChanged();

            // Work Item Tracking

            GetTemplatesCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseTestPlanChanged(TestPlan testPlan)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetTestSuitesCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseTestSuiteChanged(TestSuite testSuite)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetTestCasesCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseTestCaseChanged(TestCase testCase)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            // TODO(crhodes)
            // Add as things that depend on TestCase get added

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseWorkItemTypeWITChanged(Domain.WorkItemTracking.WorkItemType workItemType)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetProjectFieldsWITCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();
            GetStatesWITPCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();

            // Work Item Tracking

            GetWorkItemTypesFieldsCommand.RaiseCanExecuteChanged();
            GetStatesWITCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseWorkItemTypeWITPChanged(Domain.WorkItemTrackingProcess.WorkItemType workItemType)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetFieldsWITPCommand.RaiseCanExecuteChanged();
            GetRulesCommand.RaiseCanExecuteChanged();
            GetStatesWITPCommand.RaiseCanExecuteChanged();
            GetSystemControlsCommand.RaiseCanExecuteChanged();
            GetWorkItemTypesBehaviorsCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion Constructors, Initialization, and Load

        #region Fields and Properties

        private CollectionMainViewModel _collectionMainViewModel;
        private ContextMainViewModel _contextMainViewModel;

        #region Git.Items

        private string _ScopePath = "/";

        public string ScopePath
        {
            get => _ScopePath;
            set
            {
                if (_ScopePath == value) return;
                _ScopePath = value;
                OnPropertyChanged();
            }
        }

        public string ScopePathToolTip { get; set; }

        private string _RecursionLevel;

        public string RecursionLevel
        {
            get => _RecursionLevel;
            set
            {
                if (_RecursionLevel == value) return;
                _RecursionLevel = value;
                OnPropertyChanged();
            }
        }

        public string RecursionLevelToolTip { get; set; }

        #endregion

        #endregion Fields and Properties

        #region Public Methods

        #region Commands

        // TODO(crhodes)
        // Decide if these need to be public, perhaps just private

        #region Accounts Area

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

            EventAggregator.GetEvent<Domain.Accounts.Events.GetAccountsEvent>().Publish(
                new Domain.Accounts.Events.GetAccountsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    //, Project = _contextMainViewModel.Context.SelectedProject
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetAccounts Command

        #endregion Accounts Category

        #region Artifacts Area

        #region GetFeeds Command



        public DelegateCommand GetFeedsCommand { get; set; }
        public string GetFeedsContent { get; set; } = "GetFeeds";
        public string GetFeedsToolTip { get; set; } = "GetFeeds ToolTip";

        // Can get fancy and use Resources
        //public string GetFeedsContent { get; set; } = "ViewName_GetFeedsContent";
        //public string GetFeedsToolTip { get; set; } = "ViewName_GetFeedsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetFeedsContent">GetFeeds</system:String>
        //    <system:String x:Key="ViewName_GetFeedsContentToolTip">GetFeeds ToolTip</system:String>  

        public void GetFeeds()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Artifacts.Events.GetFeedsEvent>().Publish(
                new Domain.Artifacts.Events.GetFeedsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetFeedsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion


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

            EventAggregator.GetEvent<Domain.Core.Events.GetProcessesEvent>().Publish(
                new Domain.Core.Events.GetProcessesEventArgs()
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

            //EventAggregator.GetEvent<GetProjectsEvent>().Publish(
            //    new GetProjectsEventArgs()
            //    {
            //        Organization = _collectionMainViewModel.SelectedCollection.Organization
            //    });

            EventAggregator.GetEvent<Domain.Core.Events.GetProjectsEvent>().Publish(
                new Domain.Core.Events.GetProjectsEventArgs()
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

            EventAggregator.GetEvent<Domain.Core.Events.GetTeamsEvent>().Publish(
                new Domain.Core.Events.GetTeamsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetTeams Command

        #endregion Core

        #region Build Area

        #region GetAuthorizedResources Command

        public DelegateCommand GetAuthorizedResourcesCommand { get; set; }
        public string GetAuthorizedResourcesContent { get; set; } = "GetAuthorizedResources";
        public string GetAuthorizedResourcesToolTip { get; set; } = "GetAuthorizedResources ToolTip";

        // Can get fancy and use Resources
        //public string GetAuthorizedResourcesContent { get; set; } = "ViewName_GetAuthorizedResourcesContent";
        //public string GetAuthorizedResourcesToolTip { get; set; } = "ViewName_GetAuthorizedResourcesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetAuthorizedResourcesContent">GetAuthorizedResources</system:String>
        //    <system:String x:Key="ViewName_GetAuthorizedResourcesContentToolTip">GetAuthorizedResources ToolTip</system:String>  

        public void GetAuthorizedResources()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);
            // TODO(crhodes)
            // Do something amazing.
            //Message = "Cool, you called GetAuthorizedResources";

            // Uncomment this if you are telling someone else to handle this

            // Common.EventAggregator.GetEvent<GetAuthorizedResourcesEvent>().Publish();

            // May want EventArgs

            EventAggregator.GetEvent<GetAuthorizedResourcesEvent>().Publish(
                new GetAuthorizedResourcesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            // Start Cut Three - Put this in PrismEvents

            // public class GetAuthorizedResourcesEvent : PubSubEvent { }

            // End Cut Three

            // Start Cut Four - Put this in places that listen for event

            //Common.EventAggregator.GetEvent<GetAuthorizedResourcesEvent>().Subscribe(GetAuthorizedResources);

            // End Cut Four

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetAuthorizedResourcesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetBuilds Command

        public DelegateCommand GetBuildsCommand { get; set; }
        public string GetBuildsContent { get; set; } = "GetBuilds";
        public string GetBuildsToolTip { get; set; } = "GetBuilds ToolTip";

        // Can get fancy and use Resources
        //public string GetBuildsContent { get; set; } = "ViewName_GetBuildsContent";
        //public string GetBuildsToolTip { get; set; } = "ViewName_GetBuildsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetBuildsContent">GetBuilds</system:String>
        //    <system:String x:Key="ViewName_GetBuildsContentToolTip">GetBuilds ToolTip</system:String>  

        public void GetBuilds()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);
            // TODO(crhodes)
            // Do something amazing.
            //Message = "Cool, you called GetBuilds";

            // Uncomment this if you are telling someone else to handle this

            // Common.EventAggregator.GetEvent<GetBuildsEvent>().Publish();

            // May want EventArgs

            EventAggregator.GetEvent<GetBuildsEvent>().Publish(
                new GetBuildsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            // Start Cut Three - Put this in PrismEvents

            // public class GetBuildsEvent : PubSubEvent { }

            // End Cut Three

            // Start Cut Four - Put this in places that listen for event

            //Common.EventAggregator.GetEvent<GetBuildsEvent>().Subscribe(GetBuilds);

            // End Cut Four

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetBuildsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetControllers Command

        public DelegateCommand GetControllersCommand { get; set; }
        public string GetControllersContent { get; set; } = "GetControllers";
        public string GetControllersToolTip { get; set; } = "GetControllers ToolTip";

        // Can get fancy and use Resources
        //public string GetControllersContent { get; set; } = "ViewName_GetControllersContent";
        //public string GetControllersToolTip { get; set; } = "ViewName_GetControllersContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetControllersContent">GetControllers</system:String>
        //    <system:String x:Key="ViewName_GetControllersContentToolTip">GetControllers ToolTip</system:String>  

        public void GetControllers()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);
            // TODO(crhodes)
            // Do something amazing.
            // Message = "Cool, you called GetControllers";

            // Uncomment this if you are telling someone else to handle this

            // Common.EventAggregator.GetEvent<GetControllersEvent>().Publish();

            // May want EventArgs

            EventAggregator.GetEvent<GetControllersEvent>().Publish(
                new GetControllersEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            // Start Cut Three - Put this in PrismEvents

            // public class GetControllersEvent : PubSubEvent { }

            // End Cut Three

            // Start Cut Four - Put this in places that listen for event

            //Common.EventAggregator.GetEvent<GetControllersEvent>().Subscribe(GetControllers);

            // End Cut Four

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetControllersCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetDefinitions Command

        public DelegateCommand GetDefinitionsCommand { get; set; }
        public string GetDefinitionsContent { get; set; } = "GetDefinitions";
        public string GetDefinitionsToolTip { get; set; } = "GetDefinitions ToolTip";

        // Can get fancy and use Resources
        //public string GetDefinitionsContent { get; set; } = "ViewName_GetDefinitionsContent";
        //public string GetDefinitionsToolTip { get; set; } = "ViewName_GetDefinitionsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetDefinitionsContent">GetDefinitions</system:String>
        //    <system:String x:Key="ViewName_GetDefinitionsContentToolTip">GetDefinitions ToolTip</system:String>  

        public void GetDefinitions()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);
            // TODO(crhodes)
            // Do something amazing.
            //Message = "Cool, you called GetDefinitions";

            // Uncomment this if you are telling someone else to handle this

            // Common.EventAggregator.GetEvent<GetDefinitionsEvent>().Publish();

            // May want EventArgs

            EventAggregator.GetEvent<GetDefinitionsEvent>().Publish(
                new GetDefinitionsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            // Start Cut Three - Put this in PrismEvents

            // public class GetDefinitionsEvent : PubSubEvent { }

            // End Cut Three

            // Start Cut Four - Put this in places that listen for event

            //Common.EventAggregator.GetEvent<GetDefinitionsEvent>().Subscribe(GetDefinitions);

            // End Cut Four

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetDefinitionsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetGeneralSettings Command

        public DelegateCommand GetGeneralSettingsCommand { get; set; }
        public string GetGeneralSettingsContent { get; set; } = "GetGeneralSettings";
        public string GetGeneralSettingsToolTip { get; set; } = "GetGeneralSettings ToolTip";

        // Can get fancy and use Resources
        //public string GetGeneralSettingsContent { get; set; } = "ViewName_GetGeneralSettingsContent";
        //public string GetGeneralSettingsToolTip { get; set; } = "ViewName_GetGeneralSettingsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetGeneralSettingsContent">GetGeneralSettings</system:String>
        //    <system:String x:Key="ViewName_GetGeneralSettingsContentToolTip">GetGeneralSettings ToolTip</system:String>  

        public void GetGeneralSettings()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);
            // TODO(crhodes)
            // Do something amazing.
            // Message = "Cool, you called GetGeneralSettings";

            // Uncomment this if you are telling someone else to handle this

            // Common.EventAggregator.GetEvent<GetGeneralSettingsEvent>().Publish();

            // May want EventArgs

            EventAggregator.GetEvent<GetGeneralSettingsEvent>().Publish(
                new GetGeneralSettingsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            // Start Cut Three - Put this in PrismEvents

            // public class GetGeneralSettingsEvent : PubSubEvent { }

            // End Cut Three

            // Start Cut Four - Put this in places that listen for event

            //Common.EventAggregator.GetEvent<GetGeneralSettingsEvent>().Subscribe(GetGeneralSettings);

            // End Cut Four

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetGeneralSettingsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetOptions Command

        public DelegateCommand GetOptionsCommand { get; set; }
        public string GetOptionsContent { get; set; } = "GetOptions";
        public string GetOptionsToolTip { get; set; } = "GetOptions ToolTip";

        // Can get fancy and use Resources
        //public string GetOptionsContent { get; set; } = "ViewName_GetOptionsContent";
        //public string GetOptionsToolTip { get; set; } = "ViewName_GetOptionsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetOptionsContent">GetOptions</system:String>
        //    <system:String x:Key="ViewName_GetOptionsContentToolTip">GetOptions ToolTip</system:String>  

        public void GetOptions()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);
            // TODO(crhodes)
            // Do something amazing.
            // Message = "Cool, you called GetOptions";

            // Uncomment this if you are telling someone else to handle this

            // Common.EventAggregator.GetEvent<GetOptionsEvent>().Publish();

            // May want EventArgs

            EventAggregator.GetEvent<GetOptionsEvent>().Publish(
                new GetOptionsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            // Start Cut Three - Put this in PrismEvents

            // public class GetOptionsEvent : PubSubEvent { }

            // End Cut Three

            // Start Cut Four - Put this in places that listen for event

            //Common.EventAggregator.GetEvent<GetOptionsEvent>().Subscribe(GetOptions);

            // End Cut Four

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetOptionsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetResources Command

        public DelegateCommand GetResourcesCommand { get; set; }
        public string GetResourcesContent { get; set; } = "GetResources";
        public string GetResourcesToolTip { get; set; } = "GetResources ToolTip";

        // Can get fancy and use Resources
        //public string GetResourcesContent { get; set; } = "ViewName_GetResourcesContent";
        //public string GetResourcesToolTip { get; set; } = "ViewName_GetResourcesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetResourcesContent">GetResources</system:String>
        //    <system:String x:Key="ViewName_GetResourcesContentToolTip">GetResources ToolTip</system:String>  

        public void GetResources()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);
            // TODO(crhodes)
            // Do something amazing.
            // Message = "Cool, you called GetResources";

            // Uncomment this if you are telling someone else to handle this

            // Common.EventAggregator.GetEvent<GetResourcesEvent>().Publish();

            // May want EventArgs

            EventAggregator.GetEvent<GetResourcesEvent>().Publish(
                new GetResourcesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Definition = _contextMainViewModel.Context.SelectedDefinition
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetResourcesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedDefinition is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetSettings Command

        public DelegateCommand GetSettingsCommand { get; set; }
        public string GetSettingsContent { get; set; } = "GetSettings";
        public string GetSettingsToolTip { get; set; } = "GetSettings ToolTip";

        // Can get fancy and use Resources
        //public string GetSettingsContent { get; set; } = "ViewName_GetSettingsContent";
        //public string GetSettingsToolTip { get; set; } = "ViewName_GetSettingsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetSettingsContent">GetSettings</system:String>
        //    <system:String x:Key="ViewName_GetSettingsContentToolTip">GetSettings ToolTip</system:String>  

        public void GetSettings()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetSettingsEvent>().Publish(
                new GetSettingsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetSettingsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
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

            EventAggregator.GetEvent<Domain.Dashboard.Events.GetDashboardsEvent>().Publish(
                new Domain.Dashboard.Events.GetDashboardsEventArgs()
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
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetWidgetsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Dashboard.Events.GetWidgetsEvent>().Publish(
                new Domain.Dashboard.Events.GetWidgetsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetWidgets Command

        #endregion Dashboard Category

        #region Git Area

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

            EventAggregator.GetEvent<Domain.Git.Events.GetRepositoriesEvent>().Publish(
                new Domain.Git.Events.GetRepositoriesEventArgs()
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

            EventAggregator.GetEvent<Domain.Git.Events.GetRepositoriesEvent>().Publish(
                new Domain.Git.Events.GetRepositoriesEventArgs()
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

            EventAggregator.GetEvent<Domain.Git.Events.GetBlobsEvent>().Publish(
                new Domain.Git.Events.GetBlobsEventArgs()
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

            EventAggregator.GetEvent<Domain.Git.Events.GetCommitsEvent>().Publish(
                new Domain.Git.Events.GetCommitsEventArgs()
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

        #region GetCommitChanges Command

        public DelegateCommand GetCommitChangesCommand { get; set; }
        public string GetCommitChangesContent { get; set; } = "GetCommitChanges";
        public string GetCommitChangesToolTip { get; set; } = "GetCommitChanges ToolTip";

        // Can get fancy and use Resources
        //public string GetCommitChangesContent { get; set; } = "ViewName_GetCommitChangesContent";
        //public string GetCommitChangesToolTip { get; set; } = "ViewName_GetCommitChangesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetCommitChangesContent">GetCommitChanges</system:String>
        //    <system:String x:Key="ViewName_GetCommitChangesContentToolTip">GetCommitChanges ToolTip</system:String>

        public void GetCommitChanges()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetCommitChangesEvent>().Publish(
                new Domain.Git.Events.GetCommitChangesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    ,
                    Commit = _contextMainViewModel.Context.SelectedCommit
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetCommitChangesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedRepository is null
                || _contextMainViewModel.Context.SelectedCommit is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetCommitChanges Command

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

            EventAggregator.GetEvent<Domain.Git.Events.GetImportRequestsEvent>().Publish(
                new Domain.Git.Events.GetImportRequestsEventArgs()
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

            // TODO(crhodes)
            // Make ScopePath and RecursionLevel fancier
            // For now just pass strings
            EventAggregator.GetEvent<Domain.Git.Events.GetItemsEvent>().Publish(
                new Domain.Git.Events.GetItemsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    , Project = _contextMainViewModel.Context.SelectedProject
                    , Repository = _contextMainViewModel.Context.SelectedRepository
                    , ScopePath = ScopePath
                    , RecursionLevel = RecursionLevel
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

            EventAggregator.GetEvent<Domain.Git.Events.GetMergesEvent>().Publish(
                new Domain.Git.Events.GetMergesEventArgs()
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

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    , Project = _contextMainViewModel.Context.SelectedProject
                    , Repository = _contextMainViewModel.Context.SelectedRepository
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

        public DelegateCommand GetPullRequestAttachmentsCommand { get; set; }
        public string GetPullRequestAttachmentsContent { get; set; } = "Get PR Attachments";
        public string GetPullRequestAttachmentsToolTip { get; set; } = "Get PR Attachments ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestsContent { get; set; } = "ViewName_GetPullRequestsContent";
        //public string GetPullRequestsToolTip { get; set; } = "ViewName_GetPullRequestsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestsContent">GetPullRequests</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestsContentToolTip">GetPullRequests ToolTip</system:String>

        public void GetPullRequestAttachments()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestAttachmentsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    , Project = _contextMainViewModel.Context.SelectedProject
                    , Repository = _contextMainViewModel.Context.SelectedRepository
                    , PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public DelegateCommand GetPullRequestCommitsCommand { get; set; }
        public string GetPullRequestCommitsContent { get; set; } = "Get PR Commits";
        public string GetPullRequestCommitsToolTip { get; set; } = "Get PR Commits ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestsContent { get; set; } = "ViewName_GetPullRequestsContent";
        //public string GetPullRequestsToolTip { get; set; } = "ViewName_GetPullRequestsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestsContent">GetPullRequests</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestsContentToolTip">GetPullRequests ToolTip</system:String>

        public void GetPullRequestCommits()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestCommitsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    ,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public DelegateCommand GetPullRequestIterationsCommand { get; set; }
        public string GetPullRequestIterationsContent { get; set; } = "Get PR Iterations";
        public string GetPullRequestIterationsToolTip { get; set; } = "Get PR Iterations ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestsContent { get; set; } = "ViewName_GetPullRequestsContent";
        //public string GetPullRequestsToolTip { get; set; } = "ViewName_GetPullRequestsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestsContent">GetPullRequests</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestsContentToolTip">GetPullRequests ToolTip</system:String>

        public void GetPullRequestIterations()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestIterationsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    ,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public DelegateCommand GetPullRequestLabelsCommand { get; set; }
        public string GetPullRequestLabelsContent { get; set; } = "Get PR Labels";
        public string GetPullRequestLabelsToolTip { get; set; } = "Get PR Labels ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestsContent { get; set; } = "ViewName_GetPullRequestsContent";
        //public string GetPullRequestsToolTip { get; set; } = "ViewName_GetPullRequestsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestsContent">GetPullRequests</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestsContentToolTip">GetPullRequests ToolTip</system:String>

        public void GetPullRequestLabels()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestLabelsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    ,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public DelegateCommand GetPullRequestPropertiesCommand { get; set; }
        public string GetPullRequestPropertiesContent { get; set; } = "Get PR Properties";
        public string GetPullRequestPropertiesToolTip { get; set; } = "Get PR Properties ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestsContent { get; set; } = "ViewName_GetPullRequestsContent";
        //public string GetPullRequestsToolTip { get; set; } = "ViewName_GetPullRequestsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestsContent">GetPullRequests</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestsContentToolTip">GetPullRequests ToolTip</system:String>

        public void GetPullRequestProperties()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestPropertiesEvent>().Publish(
                new Domain.Git.Events.GetPullRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    ,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public DelegateCommand GetPullRequestReviewersCommand { get; set; }
        public string GetPullRequestReviewersContent { get; set; } = "Get PR Reviewers";
        public string GetPullRequestReviewersToolTip { get; set; } = "Get PR Reviewers ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestsContent { get; set; } = "ViewName_GetPullRequestsContent";
        //public string GetPullRequestsToolTip { get; set; } = "ViewName_GetPullRequestsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestsContent">GetPullRequests</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestsContentToolTip">GetPullRequests ToolTip</system:String>

        public void GetPullRequestReviewers()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestReviewersEvent>().Publish(
                new Domain.Git.Events.GetPullRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    ,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetPullRequestInfoCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedRepository is null
                || _contextMainViewModel.Context.SelectedPullRequest is null)
            {
                return false;
            }

            return true;
        }

        public DelegateCommand GetPullRequestStatusesCommand { get; set; }
        public string GetPullRequestStatusesContent { get; set; } = "Get PR Statuses";
        public string GetPullRequestStatusesToolTip { get; set; } = "Get PR Statuses ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestsContent { get; set; } = "ViewName_GetPullRequestsContent";
        //public string GetPullRequestsToolTip { get; set; } = "ViewName_GetPullRequestsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestsContent">GetPullRequests</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestsContentToolTip">GetPullRequests ToolTip</system:String>

        public void GetPullRequestStatuses()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestStatusesEvent>().Publish(
                new Domain.Git.Events.GetPullRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    ,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public DelegateCommand GetPullRequestThreadsCommand { get; set; }
        public string GetPullRequestThreadsContent { get; set; } = "Get PR Threads";
        public string GetPullRequestThreadsToolTip { get; set; } = "Get PR Threads ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestsContent { get; set; } = "ViewName_GetPullRequestsContent";
        //public string GetPullRequestsToolTip { get; set; } = "ViewName_GetPullRequestsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestsContent">GetPullRequests</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestsContentToolTip">GetPullRequests ToolTip</system:String>

        public void GetPullRequestThreads()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestThreadsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    ,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public DelegateCommand GetPullRequestWorkItemsCommand { get; set; }
        public string GetPullRequestWorkItemsContent { get; set; } = "Get PR WorkItems";
        public string GetPullRequestWorkItemsToolTip { get; set; } = "Get PR WorkItems ToolTip";

        // Can get fancy and use Resources
        //public string GetPullRequestsContent { get; set; } = "ViewName_GetPullRequestsContent";
        //public string GetPullRequestsToolTip { get; set; } = "ViewName_GetPullRequestsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPullRequestsContent">GetPullRequests</system:String>
        //    <system:String x:Key="ViewName_GetPullRequestsContentToolTip">GetPullRequests ToolTip</system:String>

        public void GetPullRequestWorkItems()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Git.Events.GetPullRequestWorkItemsEvent>().Publish(
                new Domain.Git.Events.GetPullRequestsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Repository = _contextMainViewModel.Context.SelectedRepository
                    ,
                    PullRequest = _contextMainViewModel.Context.SelectedPullRequest
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
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

            EventAggregator.GetEvent<Domain.Git.Events.GetPushesEvent>().Publish(
                new Domain.Git.Events.GetPushesEventArgs()
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

            EventAggregator.GetEvent<Domain.Git.Events.GetRefsEvent>().Publish(
                new Domain.Git.Events.GetRefsEventArgs()
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

            EventAggregator.GetEvent<Domain.Git.Events.GetStatsEvent>().Publish(
                new Domain.Git.Events.GetStatsEventArgs()
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

        #endregion Git Category

        #region Tokens Category

        #region GetPats Command

        public DelegateCommand GetPatsCommand { get; set; }
        public string GetPatsContent { get; set; } = "GetPats";
        public string GetPatsToolTip { get; set; } = "GetPats ToolTip";

        // Can get fancy and use Resources
        //public string GetPatsContent { get; set; } = "ViewName_GetPatsContent";
        //public string GetPatsToolTip { get; set; } = "ViewName_GetPatsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPatsContent">GetPats</system:String>
        //    <system:String x:Key="ViewName_GetPatsContentToolTip">GetPats ToolTip</system:String>  

        public void GetPats()
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);
            // TODO(crhodes)
            // Do something amazing.
            //Message = "Cool, you called GetPats";

            // Uncomment this if you are telling someone else to handle this
            // Common.EventAggregator.GetEvent<GetPatsEvent>().Publish();

            EventAggregator.GetEvent<GetPatsEvent>().Publish(
                new GetPatsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            // Start Cut Three - Put this in PrismEvents

            // public class GetPatsEvent : PubSubEvent { }

            // End Cut Three

            // Start Cut Four - Put this in places that listen for event

            //Common.EventAggregator.GetEvent<GetPatsEvent>().Subscribe(GetPats);

            // End Cut Four

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetPatsCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            return true;
        }

        #endregion

        #endregion

        #region Test Category

        #region GetTestsPlan Command

        public DelegateCommand GetTestPlansCommand { get; set; }
        public string GetTestPlansContent { get; set; } = "Get Test Plans";
        public string GetTestPlansToolTip { get; set; } = "Get Test Plans ToolTip";

        // Can get fancy and use Resources
        //public string GetTestsPlanContent { get; set; } = "ViewName_GetTestsPlanContent";
        //public string GetTestsPlanToolTip { get; set; } = "ViewName_GetTestsPlanContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTestsPlanContent">GetTestsPlan</system:String>
        //    <system:String x:Key="ViewName_GetTestsPlanContentToolTip">GetTestsPlan ToolTip</system:String>

        public void GetTestPlans()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Test.Events.GetTestPlansEvent>().Publish(
                new Domain.Test.Events.GetTestPlansEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetTestPlansCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetTestsPlan Command

        #region GetTestSuites Command

        public DelegateCommand GetTestSuitesCommand { get; set; }
        public string GetTestSuitesContent { get; set; } = "GetTestSuites";
        public string GetTestSuitesToolTip { get; set; } = "GetTestSuites ToolTip";

        // Can get fancy and use Resources
        //public string GetTestSuitesContent { get; set; } = "ViewName_GetTestSuitesContent";
        //public string GetTestSuitesToolTip { get; set; } = "ViewName_GetTestSuitesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTestSuitesContent">GetTestSuites</system:String>
        //    <system:String x:Key="ViewName_GetTestSuitesContentToolTip">GetTestSuites ToolTip</system:String>

        public void GetTestSuites()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Test.Events.GetTestSuitesEvent>().Publish(
                new Domain.Test.Events.GetTestSuitesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    TestPlan = _contextMainViewModel.Context.SelectedTestPlan
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetTestSuitesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedTestPlan is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetTestSuites Command

        #region GetTestCases Command

        public DelegateCommand GetTestCasesCommand { get; set; }
        public string GetTestCasesContent { get; set; } = "GetTestCases";
        public string GetTestCasesToolTip { get; set; } = "GetTestCases ToolTip";

        // Can get fancy and use Resources
        //public string GetTestCasesContent { get; set; } = "ViewName_GetTestCasesContent";
        //public string GetTestCasesToolTip { get; set; } = "ViewName_GetTestCasesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTestCasesContent">GetTestCases</system:String>
        //    <system:String x:Key="ViewName_GetTestCasesContentToolTip">GetTestCases ToolTip</system:String>

        public void GetTestCases()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Test.Events.GetTestCasesEvent>().Publish(
                new Domain.Test.Events.GetTestCasesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    TestPlan = _contextMainViewModel.Context.SelectedTestPlan,
                    TestSuite = _contextMainViewModel.Context.SelectedTestSuite
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetTestCasesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedTestPlan is null
                || _contextMainViewModel.Context.SelectedTestSuite is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetTestCases Command

        #region GetTestPoints Command

        public DelegateCommand GetTestPointsCommand { get; set; }
        public string GetTestPointsContent { get; set; } = "GetTestPoints";
        public string GetTestPointsToolTip { get; set; } = "GetTestPoints ToolTip";

        // Can get fancy and use Resources
        //public string GetTestPointsContent { get; set; } = "ViewName_GetTestPointsContent";
        //public string GetTestPointsToolTip { get; set; } = "ViewName_GetTestPointsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTestPointsContent">GetTestPoints</system:String>
        //    <system:String x:Key="ViewName_GetTestPointsContentToolTip">GetTestPoints ToolTip</system:String>  

        public void GetTestPoints()
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Test.Events.GetTestPointsEvent>().Publish(
                new Domain.Test.Events.GetTestPointsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    TestPlan = _contextMainViewModel.Context.SelectedTestPlan.id,
                    TestSuite = _contextMainViewModel.Context.SelectedTestSuite.id,
                    //TestPlan = _contextMainViewModel.Context.SelectedTestPlan,
                    //TestSuite = _contextMainViewModel.Context.SelectedTestSuite
                });

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetTestPointsCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            return true;
        }

        #endregion

        #endregion Test Category

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

            EventAggregator.GetEvent<GetArtifactLinkTypesEvent>().Publish(
                new GetArtifactLinkTypesEventArgs()
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

            EventAggregator.GetEvent<GetClassificationNodesEvent>().Publish(
                new GetClassificationNodesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    IDs = _contextMainViewModel.Context.ClassificationNodeIds,
                    Depth = _contextMainViewModel.Context.ClassificationNodeDepth
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

            EventAggregator.GetEvent<GetFieldsWITEvent>().Publish(
                new GetFieldsWITEventArgs()
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

            EventAggregator.GetEvent<GetFieldsWITEvent>().Publish(
                new GetFieldsWITEventArgs()
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

            EventAggregator.GetEvent<GetQueriesEvent>().Publish(
                new GetQueriesEventArgs()
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

            EventAggregator.GetEvent<GetWorkItemTypeCategoriesEvent>().Publish(
                new GetWorkItemTypeCategoriesEventArgs()
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

            EventAggregator.GetEvent<GetStatesWITEvent>().Publish(
                new GetStatesWITEventArgs()
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

            EventAggregator.GetEvent<GetWorkItemTypesWITEvent>().Publish(
                new GetWorkItemTypesWITEventArgs()
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

            EventAggregator.GetEvent<GetWorkItemTypesFieldsEvent>().Publish(
                new GetWorkItemTypesFieldsEventArgs()
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

        #region GetWorkItem Command

        public DelegateCommand GetWorkItemCommand { get; set; }
        public string GetWorkItemContent { get; set; } = "GetWorkItem";
        public string GetWorkItemToolTip { get; set; } = "GetWorkItem ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkItemContent { get; set; } = "ViewName_GetWorkItemContent";
        //public string GetWorkItemToolTip { get; set; } = "ViewName_GetWorkItemContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkItemContent">GetWorkItem</system:String>
        //    <system:String x:Key="ViewName_GetWorkItemContentToolTip">GetWorkItem ToolTip</system:String>  

        public void GetWorkItem()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.WorkItemTracking.Events.GetWorkItemEvent>().Publish(
                new Domain.WorkItemTracking.Events.GetWorkItemEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Id = _contextMainViewModel.Context.Model.WorkItemId
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetWorkItemCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

    #endregion

    // End Cut One

    #endregion Work Item Tracking Category

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

        public void GetBehaviorsWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetBehaviorsEvent>().Publish(
                new GetBehaviorsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
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

            EventAggregator.GetEvent<GetFieldsWITPEvent>().Publish(
                new GetFieldsWITPEventArgs()
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

            EventAggregator.GetEvent<GetListsEvent>().Publish(
                new GetListsEventArgs()
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

            EventAggregator.GetEvent<GetProcessesWITPEvent>().Publish(
                new GetProcessesWITPEventArgs()
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

            EventAggregator.GetEvent<GetRulesEvent>().Publish(
                new GetRulesEventArgs()
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

            EventAggregator.GetEvent<GetStatesWITPEvent>().Publish(
                new GetStatesWITPEventArgs()
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

            EventAggregator.GetEvent<GetSystemControlsEvent>().Publish(
              new GetSystemControlsEventArgs()
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

            EventAggregator.GetEvent<GetWorkItemTypesWITPEvent>().Publish(
                new GetWorkItemTypesWITPEventArgs()
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

            EventAggregator.GetEvent<GetWorkItemTypesBehaviorsEvent>().Publish(
                new GetWorkItemTypesBehaviorsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWITP
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetWorkItemTypesBehaviors Command

        #endregion Work Item Tracking Process Category

        #endregion

        #endregion Public Methods

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