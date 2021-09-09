using System;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Git;
using AZDORestApiExplorer.Domain.Test;
using AZDORestApiExplorer.Presentation.ModelWrappers;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Services;

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

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            InstanceCountVM++;

            EventAggregator.GetEvent<Domain.Core.Events.SelectedProcessChangedEvent>().Subscribe(ProcessChanged);
            EventAggregator.GetEvent<Domain.Core.Events.SelectedProjectChangedEvent>().Subscribe(ProjectChanged);
            EventAggregator.GetEvent<Domain.Core.Events.SelectedTeamChangedEvent>().Subscribe(TeamChanged);

            EventAggregator.GetEvent<Domain.Git.Events.SelectedRepositoryChangedEvent>().Subscribe(RepositoryChanged);
            EventAggregator.GetEvent<Domain.Git.Events.SelectedCommitChangedEvent>().Subscribe(CommitChanged);
            EventAggregator.GetEvent<Domain.Git.Events.SelectedPullRequestChangedEvent>().Subscribe(PullRequestChanged);

            EventAggregator.GetEvent<Domain.Test.Events.SelectedTestPlanChangedEvent>().Subscribe(TestPlanChanged);
            EventAggregator.GetEvent<Domain.Test.Events.SelectedTestSuiteChangedEvent>().Subscribe(TestSuiteChanged);
            EventAggregator.GetEvent<Domain.Test.Events.SelectedTestCaseChangedEvent>().Subscribe(TestCaseChanged);

            EventAggregator.GetEvent<Domain.WorkItemTracking.Events.SelectedWorkItemTypeWITChangedEvent>().Subscribe(WorkItemTypeWITChanged);
            EventAggregator.GetEvent<Domain.WorkItemTrackingProcess.Events.SelectedWorkItemTypeWITPChangedEvent>().Subscribe(WorkItemTypeWITPChanged);

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


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

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private void ProcessChanged(Process process)
        {
            Context.SelectedProcess = process;
        }

        private void ProjectChanged(Project project)
        {
            Context.SelectedProject = project;
        }

        private void TeamChanged(Team team)
        {
            Context.SelectedTeam = team;
        }

        private void RepositoryChanged(Repository repository)
        {
            Context.SelectedRepository = repository;
        }

        private void CommitChanged(Commit commit)
        {
            Context.SelectedCommit = commit;
        }

        private void PullRequestChanged(PullRequest pullRequest)
        {
            Context.SelectedPullRequest = pullRequest;
        }

        private void TestPlanChanged(TestPlan testPlan)
        {
            Context.SelectedTestPlan = testPlan;
        }

        private void TestSuiteChanged(TestSuite testSuite)
        {
            Context.SelectedTestSuite = testSuite;
        }

        private void TestCaseChanged(TestCase testCase)
        {
            Context.SelectedTestCase = testCase;
        }

        private void WorkItemTypeWITChanged(Domain.WorkItemTracking.WorkItemType workItemType)
        {
            Context.SelectedWorkItemTypeWIT = workItemType;
        }

        private void WorkItemTypeWITPChanged(Domain.WorkItemTrackingProcess.WorkItemType workItemType)
        {
            Context.SelectedWorkItemTypeWITP = workItemType;
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
