using System;

using AZDORestApiExplorer.Domain.Build;
using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Git;
using AZDORestApiExplorer.Domain.Git.Events;
using AZDORestApiExplorer.Domain.Test;
using AZDORestApiExplorer.Presentation.ModelWrappers;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class ContextMainViewModel : EventViewModelBase, IInstanceCountVM //, IContextMainViewModel
    {

        #region Constructors, Initialization, and Load

        public ContextMainViewModel(
            IEventAggregator eventAggregator,
            DialogService dialogService) : base(eventAggregator, dialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void BuildWorkItemRefChanged(BuildWorkItemRef workItemRef)
        {
            int workItemId = 0;

            if (! (workItemRef is null)
                && int.TryParse(workItemRef.id, out workItemId))
            {
                Context.WorkItemId = int.Parse(workItemRef.id);
            }
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            InstanceCountVM++;

            EventAggregator.GetEvent<Domain.Core.Events.SelectedProcessChangedEvent>().Subscribe(ProcessChanged);
            EventAggregator.GetEvent<Domain.Core.Events.SelectedProjectChangedEvent>().Subscribe(ProjectChanged);
            EventAggregator.GetEvent<Domain.Core.Events.SelectedTeamChangedEvent>().Subscribe(TeamChanged);

            EventAggregator.GetEvent<Domain.Build.Events.SelectedBuildChangedEvent>().Subscribe(BuildChanged);

            EventAggregator.GetEvent<Domain.Build.Events.SelectedDefinitionChangedEvent>().Subscribe(DefinitionChanged);

            EventAggregator.GetEvent<Domain.Git.Events.SelectedRepositoryChangedEvent>().Subscribe(RepositoryChanged);
            EventAggregator.GetEvent<Domain.Git.Events.SelectedCommitChangedEvent>().Subscribe(CommitChanged);

            EventAggregator.GetEvent<Domain.Git.Events.SelectedPullRequestChangedEvent>().Subscribe(PullRequestChanged);
            EventAggregator.GetEvent<Domain.Git.Events.SelectedPullRequestCommitChangedEvent>().Subscribe(CommitChanged);
            EventAggregator.GetEvent<Domain.Git.Events.SelectedPullRequestIterationChangedEvent>().Subscribe(PullRequestIterationChanged);
            EventAggregator.GetEvent<Domain.Git.Events.SelectedPullRequestThreadChangedEvent>().Subscribe(PullRequestThreadChanged);

            EventAggregator.GetEvent<Domain.Test.Events.SelectedTestPlanChangedEvent>().Subscribe(TestPlanChanged);
            EventAggregator.GetEvent<Domain.Test.Events.SelectedTestSuiteChangedEvent>().Subscribe(TestSuiteChanged);
            EventAggregator.GetEvent<Domain.Test.Events.SelectedTestCaseChangedEvent>().Subscribe(TestCaseChanged);

            EventAggregator.GetEvent<Domain.Build.Events.SelectedBuildWorkItemRefChangedEvent>().Subscribe(BuildWorkItemRefChanged);
            EventAggregator.GetEvent<Domain.WorkItemTracking.Events.SelectedWorkItemTypeWITChangedEvent>().Subscribe(WorkItemTypeWITChanged);
            EventAggregator.GetEvent<Domain.WorkItemTrackingProcess.Events.SelectedWorkItemTypeWITPChangedEvent>().Subscribe(WorkItemTypeWITPChanged);

            Context.ClassificationNodeDepth = 2;

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums (none)


        #endregion

        #region Structures (none)


        #endregion

        #region Fields and Properties

        private ContextWrapper _context = new ContextWrapper(new Application.Context());
        
        public ContextWrapper Context
        {
            get => _context;
            set
            {
                if (_context == value)
                    return;
                _context = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Event Handlers (none)


        #endregion

        #region Public Methods (none)


        #endregion

        #region Protected Methods (none)


        #endregion

        #region Private Methods

        private void ProcessChanged(Process process)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            Context.SelectedProcess = process;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void ProjectChanged(Project project)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            Context.SelectedProject = project;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void TeamChanged(Team team)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            Context.SelectedTeam = team;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void BuildChanged(Domain.Build.Build build)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            Context.SelectedBuild = build;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void DefinitionChanged(Domain.Build.Definition definition)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            Context.SelectedDefinition = definition;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void RepositoryChanged(GitRepository repository)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            ClearRepsitoryDependentContext();

            EventAggregator.GetEvent<ClearCommitsEvent>().Publish();

            EventAggregator.GetEvent<ClearPullRequestsEvent>().Publish();
            //EventAggregator.GetEvent<ClearPullRequestAttachmentsEvent>().Publish();
            //EventAggregator.GetEvent<ClearPullRequestCommitsEvent>().Publish();
            //EventAggregator.GetEvent<ClearPullRequestIterationsEvent>().Publish();
            //EventAggregator.GetEvent<ClearPullRequestLabelsEvent>().Publish();
            //EventAggregator.GetEvent<ClearPullRequestPropertiesEvent>().Publish();
            //EventAggregator.GetEvent<ClearPullRequestReviewersEvent>().Publish();
            //EventAggregator.GetEvent<ClearPullRequestStatusesEvent>().Publish();
            //EventAggregator.GetEvent<ClearPullRequestThreadsEvent>().Publish();
            //EventAggregator.GetEvent<ClearPullRequestWorkItemsEvent>().Publish();

            Context.SelectedGitRepository = repository;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void ClearRepsitoryDependentContext()
        {
            ClearPullRequestDependentContext();
        }

        private void CommitChanged(Commit commit)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            Context.SelectedCommit = commit;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void CommitChanged(PullRequestCommit commit)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            if (!(commit is null))
            {
                var newCommit = new Domain.Git.Commit() { commitId = commit.commitId };
                Context.SelectedCommit = newCommit;
            }

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void PullRequestChanged(PullRequest pullRequest)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            //ClearPullRequestDependentContext();
            Context.SelectedPullRequest = pullRequest;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void ClearPullRequestDependentContext()
        {
            Context.SelectedCommit = null;
            Context.SelectedPullRequest = null;
            Context.SelectedPullRequestIteration = null;
            Context.SelectedPullRequestThread = null;
        }

        private void PullRequestIterationChanged(PullRequestIteration pullRequestIteration)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            //Context.SelectedPullRequestIteration = null;
            Context.SelectedPullRequestIteration = pullRequestIteration;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void PullRequestThreadChanged(PullRequestThread pullRequestThread)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            Context.SelectedPullRequestThread = pullRequestThread;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void TestPlanChanged(TestPlan testPlan)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            Context.SelectedTestPlan = testPlan;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void TestSuiteChanged(TestSuite testSuite)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            Context.SelectedTestSuite = testSuite;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void TestCaseChanged(TestCase testCase)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            Context.SelectedTestCase = testCase;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void WorkItemTypeWITChanged(Domain.WorkItemTracking.WorkItemType workItemType)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            Context.SelectedWorkItemTypeWIT = workItemType;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void WorkItemTypeWITPChanged(Domain.WorkItemTrackingProcess.WorkItemType workItemType)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            Context.SelectedWorkItemTypeWITP = workItemType;

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
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
