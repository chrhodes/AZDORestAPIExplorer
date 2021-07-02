namespace AZDORestApiExplorer.Application
{
    public class Context
    {
        public Domain.Core.Process SelectedProcess { get; set; }
        public Domain.Core.Project SelectedProject { get; set; }
        public Domain.Core.Team SelectedTeam { get; set; }

        public Domain.Dashboard.Dashboard SelectedDashboard { get; set; }

        public Domain.Git.Repository SelectedRepository { get; set; }
        public Domain.Git.Commit SelectedCommit { get; set; }

        public Domain.Test.TestPlan SelectedTestPlan { get; set; }
        public Domain.Test.TestSuite SelectedTestSuite { get; set; }
        public Domain.Test.TestCase SelectedTestCase { get; set; }

        public Domain.WorkItemTracking.WorkItemType SelectedWorkItemTypeWIT { get; set; }

        public Domain.WorkItemTrackingProcess.WorkItemType SelectedWorkItemTypeWITP { get; set; }

    }
}
