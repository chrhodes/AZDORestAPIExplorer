using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events.Test
{
    public class GetTestSuitesEventArgs
    {
        public Organization Organization;

        // public Domain.Core.Process Process;

        public Domain.Core.Project Project;

        public Domain.Test.TestPlan TestPlan;

        // public Domain.Core.Team Team;

        // public WorkItemType WorkItemType;
    }
}
