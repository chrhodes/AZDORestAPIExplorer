using AZDORestApiExplorer.Domain.Build;
using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Git;
using AZDORestApiExplorer.Domain.Test;
using AZDORestApiExplorer.Domain.WorkItemTracking;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ModelWrappers
{
    public class ContextWrapper : ModelWrapper<Application.Context>
    {
        public ContextWrapper(Application.Context model) : base(model)
        {
        }

        public Domain.Core.Process SelectedProcess
        {
            get { return GetValue<Domain.Core.Process>(); }
            set { SetValue(value); }
        }

        public Project SelectedProject
        {
            get { return GetValue<Project>(); }
            set { SetValue(value); }
        }

        public Team SelectedTeam
        {
            get { return GetValue<Team>(); }
            set { SetValue(value); }
        }

        public string ClassificationNodeIds
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int ClassificationNodeDepth
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public Domain.Build.Build SelectedBuild
        {
            get { return GetValue<Domain.Build.Build>(); }
            set { SetValue(value); }
        }

        public int BuildId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public Definition SelectedDefinition
        {
            get { return GetValue<Definition>(); }
            set { SetValue(value); }
        }

        public int DefinitionId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public GitRepository SelectedGitRepository
        {
            get { return GetValue<GitRepository>(); }
            set { SetValue(value); }
        }

        public Commit SelectedCommit
        {
            get { return GetValue<Commit>(); }
            set { SetValue(value); }
        }

        public PullRequest SelectedPullRequest
        {
            get { return GetValue<PullRequest>(); }
            set { SetValue(value); }
        }

        public PullRequestThread SelectedPullRequestThread
        {
            get { return GetValue<PullRequestThread>(); }
            set { SetValue(value); }
        }

        public Domain.Dashboard.Dashboard SelectedDashboard
        {
            get { return GetValue<Domain.Dashboard.Dashboard>(); }
            set { SetValue(value); }
        }

        public TestPlan SelectedTestPlan
        {
            get { return GetValue<TestPlan>(); }
            set { SetValue(value); }
        }

        public TestSuite SelectedTestSuite
        {
            get { return GetValue<TestSuite>(); }
            set { SetValue(value); }
        }

        public TestCase SelectedTestCase
        {
            get { return GetValue<TestCase>(); }
            set { SetValue(value); }
        }

        public WorkItem SelectedWorkItem
        {
            get { return GetValue<WorkItem>(); }
            set { SetValue(value); }
        }

        public Domain.WorkItemTracking.WorkItemType SelectedWorkItemTypeWIT
        {
            get { return GetValue<Domain.WorkItemTracking.WorkItemType>(); }
            set { SetValue(value); }
        }

        public int WorkItemId
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public Domain.WorkItemTrackingProcess.WorkItemType SelectedWorkItemTypeWITP
        {
            get { return GetValue<Domain.WorkItemTrackingProcess.WorkItemType>(); }
            set { SetValue(value); }
        }

    }
}
