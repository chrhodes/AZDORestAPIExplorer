namespace AZDORestApiExplorer.Application
{
    public class Context
    {
        public int ClassificationNodeDepth { get; set; }
        public string ClassificationNodeIds { get; set; }

        public Domain.Dashboard.Dashboard SelectedDashboard { get; set; }
        public Domain.Core.Process SelectedProcess { get; set; }

        public Domain.Core.Project SelectedProject { get; set; }

        // Build

        public Domain.Build.Build SelectedBuild { get; set; }
        public int BuildId { get; set; }
        public Domain.Build.Definition SelectedDefinition { get; set; }
        public int DefinitionId { get; set; }

        // Repository

        // Git

        public Domain.Git.Commit SelectedCommit { get; set; }
        public Domain.Git.PullRequest SelectedPullRequest { get; set; }
        public Domain.Git.GitRepository SelectedGitRepository { get; set; }
        public Domain.Git.PullRequestThread SelectedPullRequestThread { get; set; }

        public Domain.Core.Team SelectedTeam { get; set; }

        // Test

        public Domain.Test.TestCase SelectedTestCase { get; set; }
        public Domain.Test.TestPlan SelectedTestPlan { get; set; }
        public Domain.Test.TestSuite SelectedTestSuite { get; set; }


        public Domain.WorkItemTracking.WorkItem SelectedWorkItem { get; set; }

        public Domain.WorkItemTracking.WorkItemType SelectedWorkItemTypeWIT { get; set; }

        public Domain.WorkItemTrackingProcess.WorkItemType SelectedWorkItemTypeWITP { get; set; }
        public int WorkItemId { get; set; }
    }
}