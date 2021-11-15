using System;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain.Build;
using AZDORestApiExplorer.Domain.Build.Events;
using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Core.Events;
using AZDORestApiExplorer.Domain.Dashboard.Events;
using AZDORestApiExplorer.Domain.Git;
using AZDORestApiExplorer.Domain.Git.Events;
using AZDORestApiExplorer.Domain.Graph.Events;
using AZDORestApiExplorer.Domain.Test;
using AZDORestApiExplorer.Domain.Tokens.Events;
using AZDORestApiExplorer.Domain.WorkItemTracking;
using AZDORestApiExplorer.Domain.WorkItemTracking.Events;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess.Events;

using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public partial class CommandsGetViewModel : EventViewModelBase, IInstanceCountVM
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

            #endregion Core Area

            #region Accounts Area

            GetAccountsCommand = new DelegateCommand(GetAccountsExecute, GetAccountsCanExecute);

            #endregion Accounts Area

            #region Artifacts Area

            GetFeedsCommand = new DelegateCommand(GetFeeds, GetFeedsCanExecute);

            #endregion Artifacts Area

            #region Build Area

            // Trigger calls to other Build Related stuff anytime a new Build is selected
            EventAggregator.GetEvent<SelectedBuildChangedEvent>().Subscribe(CallBuildDependentStuff);

            GetAuthorizedResourcesCommand = new DelegateCommand(GetAuthorizedResources, GetAuthorizedResourcesCanExecute);

            GetBuildsCommand = new DelegateCommand(GetBuilds, GetBuildsCanExecute);

            GetBuildInfoCommand = new DelegateCommand(GetBuildInfo, GetBuildInfoCanExecute);

            GetBuildChangesCommand = new DelegateCommand(GetBuildChanges, GetBuildChangesCanExecute);
            GetBuildTagsCommand = new DelegateCommand(GetBuildTags, GetBuildTagsCanExecute);
            GetBuildLogsCommand = new DelegateCommand(GetBuildLogs, GetBuildLogsCanExecute);
            GetBuildWorkItemRefsCommand = new DelegateCommand(GetBuildWorkItemRefs, GetBuildWorkItemRefsCanExecute);

            GetControllersCommand = new DelegateCommand(GetControllers, GetControllersCanExecute);

            GetDefinitionsCommand = new DelegateCommand(GetDefinitions, GetDefinitionsCanExecute);
            // Trigger a GetResources call anytime a new Definition is selected
            EventAggregator.GetEvent<SelectedDefinitionChangedEvent>().Subscribe(CallGetResources);

            GetGeneralSettingsCommand = new DelegateCommand(GetGeneralSettings, GetGeneralSettingsCanExecute);
            GetOptionsCommand = new DelegateCommand(GetOptions, GetOptionsCanExecute);
            GetResourcesCommand = new DelegateCommand(GetResources, GetResourcesCanExecute);
            GetSettingsCommand = new DelegateCommand(GetSettings, GetSettingsCanExecute);

            #endregion Build Area

            #region Dashboard Area

            GetDashboardsCommand = new DelegateCommand(GetDashboardsExecute, GetDashboardsCanExecute);

            GetWidgetsCommand = new DelegateCommand(GetWidgetsExecute, GetWidgetsCanExecute);

            #endregion Dashboard Area

            #region Git Area

            EventAggregator.GetEvent<SelectedPullRequestChangedEvent>().Subscribe(CallPullRequestDependentStuff);

            GetRepositoriesCommand = new DelegateCommand(GetRepositoriesExecute, GetRepositoriesCanExecute);
            GetProjectRepositoriesCommand = new DelegateCommand(GetProjectRepositoriesExecute, GetProjectRepositoriesCanExecute);

            GetBlobsCommand = new DelegateCommand(GetBlobs, GetBlobsCanExecute);
            GetCommitsCommand = new DelegateCommand(GetCommits, GetCommitsCanExecute);
            GetCommitChangesCommand = new DelegateCommand(GetCommitChanges, GetCommitChangesCanExecute);

            // Trigger a GetCommitChanges call anytime a new Commit is selected
            EventAggregator.GetEvent<SelectedCommitChangedEvent>().Subscribe(CallGetCommitChange);
            EventAggregator.GetEvent<SelectedPullRequestCommitChangedEvent>().Subscribe(CallGetCommitChange);

            GetImportRequestsCommand = new DelegateCommand(GetImportRequests, GetImportRequestsCanExecute);
            GetItemsCommand = new DelegateCommand(GetItems, GetItemsCanExecute);
            GetMergesCommand = new DelegateCommand(GetMerges, GetMergesCanExecute);

            GetPullRequestsCommand = new DelegateCommand(GetPullRequests, GetPullRequestsCanExecute);

            GetPullRequestAttachmentsCommand = new DelegateCommand(GetPullRequestAttachments, GetPullRequestInfoCanExecute);
            GetPullRequestCommitsCommand = new DelegateCommand(GetPullRequestCommits, GetPullRequestInfoCanExecute);
            GetPullRequestCommitChangesCommand = new DelegateCommand(GetPullRequestCommitChanges, GetPullRequestCommitChangesCanExecute);
            GetPullRequestIterationsCommand = new DelegateCommand(GetPullRequestIterations, GetPullRequestInfoCanExecute);
            GetPullRequestIterationChangesCommand = new DelegateCommand(GetPullRequestIterationChanges, GetPullRequestIterationChangesCanExecute);
            GetPullRequestLabelsCommand = new DelegateCommand(GetPullRequestLabels, GetPullRequestInfoCanExecute);
            GetPullRequestPropertiesCommand = new DelegateCommand(GetPullRequestProperties, GetPullRequestInfoCanExecute);
            GetPullRequestReviewersCommand = new DelegateCommand(GetPullRequestReviewers, GetPullRequestInfoCanExecute);
            GetPullRequestStatusesCommand = new DelegateCommand(GetPullRequestStatuses, GetPullRequestInfoCanExecute);

            GetPullRequestThreadsCommand = new DelegateCommand(GetPullRequestThreads, GetPullRequestInfoCanExecute);

            // Trigger a GetPullRequestThreadComments call anytime a new thead is selected
            // NOTE(crhodes)
            // These seem to come reliably from Thread.  Comment this out for now.  

            //EventAggregator.GetEvent<SelectedPullRequestThreadChangedEvent>().Subscribe(CallGetThreadChange);

            // I have left this call in case there is more info that is helpful.

            GetPullRequestThreadCommentsCommand = new DelegateCommand(GetPullRequestThreadComments, GetPullRequestThreadCommentsCanExecute);
            GetPullRequestWorkItemsCommand = new DelegateCommand(GetPullRequestWorkItems, GetPullRequestInfoCanExecute);

            GetPushesCommand = new DelegateCommand(GetPushes, GetPushesCanExecute);
            GetStatsCommand = new DelegateCommand(GetStats, GetStatsCanExecute);
            GetRefsCommand = new DelegateCommand(GetRefs, GetRefsCanExecute);

            #endregion Git Area

            #region Graph Area

            GetGroupsCommand = new DelegateCommand(GetGroups, GetGroupsCanExecute);
            GetUsersCommand = new DelegateCommand(GetUsers, GetUsersCanExecute);

            #endregion Graph Area

            #region Test Category

            GetTestPlansCommand = new DelegateCommand(GetTestPlans, GetTestPlansCanExecute);
            GetTestSuitesCommand = new DelegateCommand(GetTestSuites, GetTestSuitesCanExecute);
            GetTestCasesCommand = new DelegateCommand(GetTestCases, GetTestCasesCanExecute);

            GetTestPointsCommand = new DelegateCommand(GetTestPoints, GetTestPointsCanExecute);

            #endregion Test Category

            #region Tokens Area

            GetPatsCommand = new DelegateCommand(GetPats, GetPatsCanExecute);

            #endregion Tokens Area

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
            // Trigger a GetWorkItem call anytime a new WorkItem is selected or entered
            EventAggregator.GetEvent<SelectedBuildWorkItemRefChangedEvent>().Subscribe(CallGetWorkItem);
            EventAggregator.GetEvent<SelectedPullRequestWorkItemChangedEvent>().Subscribe(CallGetWorkItem);

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
            EventAggregator.GetEvent<Domain.Git.Events.SelectedPullRequestCommitChangedEvent>().Subscribe(RaiseCommitChanged);
            EventAggregator.GetEvent<Domain.Git.Events.SelectedPullRequestChangedEvent>().Subscribe(RaisePullRequestChanged);
            EventAggregator.GetEvent<Domain.Git.Events.SelectedPullRequestIterationChangedEvent>().Subscribe(RaisePullRequestIterationChanged);
            EventAggregator.GetEvent<Domain.Git.Events.SelectedPullRequestThreadChangedEvent>().Subscribe(RaisePullRequestThreadChanged);

            EventAggregator.GetEvent<Domain.Test.Events.SelectedTestPlanChangedEvent>().Subscribe(RaiseTestPlanChanged);
            EventAggregator.GetEvent<Domain.Test.Events.SelectedTestSuiteChangedEvent>().Subscribe(RaiseTestSuiteChanged);
            EventAggregator.GetEvent<Domain.Test.Events.SelectedTestCaseChangedEvent>().Subscribe(RaiseTestCaseChanged);

            EventAggregator.GetEvent<SelectedWorkItemTypeWITChangedEvent>().Subscribe(RaiseWorkItemTypeWITChanged);
            EventAggregator.GetEvent<SelectedWorkItemTypeWITPChangedEvent>().Subscribe(RaiseWorkItemTypeWITPChanged);

            EventAggregator.GetEvent<SelectedDashboardChangedEvent>().Subscribe(RaiseDashboardChanged);

            #endregion Context Enablement

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // NOTE(crhodes)
        // RaiseCanExecuteChanged for any Command that is dependent on Context.
        // N.B. Need to add to each Context Item for button to be enabled.

        //++ Add Commands that only depend on Organization Context here

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

            // Graph

            GetGroupsCommand.RaiseCanExecuteChanged();
            GetUsersCommand.RaiseCanExecuteChanged();

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

        // Other commands that depend on more do not need to be added
        // as the check is in all CanExecute methods

        private void RaiseBuildChanged(Domain.Build.Build build)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetBuildInfoCommand.RaiseCanExecuteChanged();
            GetBuildChangesCommand.RaiseCanExecuteChanged();
            GetBuildLogsCommand.RaiseCanExecuteChanged();
            GetBuildWorkItemRefsCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseCommitChanged(Commit commit)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetCommitChangesCommand.RaiseCanExecuteChanged();
            //GetPullRequestCommitChangesCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseCommitChanged(PullRequestCommit pullRequestCommit)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetCommitChangesCommand.RaiseCanExecuteChanged();
            //GetPullRequestCommitChangesCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseDashboardChanged(Domain.Dashboard.Dashboard dashboard)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetWidgetsCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseDefinitionChanged(Domain.Build.Definition definition)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetResourcesCommand.RaiseCanExecuteChanged();

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
            GetBuildTagsCommand.RaiseCanExecuteChanged();

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

        private void RaisePullRequestIterationChanged(PullRequestIteration obj)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetPullRequestIterationChangesCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaisePullRequestThreadChanged(PullRequestThread obj)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            GetPullRequestThreadCommentsCommand.RaiseCanExecuteChanged();

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RaiseRepositoryChanged(GitRepository repository)
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

        private void RaiseTestCaseChanged(TestCase testCase)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            // TODO(crhodes)
            // Add as things that depend on TestCase get added

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

        #endregion Fields and Properties

        #region IInstanceCount

        private static int _instanceCountVM;

        public int InstanceCountVM
        {
            get => _instanceCountVM;
            set => _instanceCountVM = value;
        }

        #endregion IInstanceCount
    }
}