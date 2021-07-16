using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events.Test
{
    public class xGetTestCasesEventArgs
    {
        public Organization Organization;

        // public Domain.Core.Process Process;

        public Domain.Core.Project Project;

        public Domain.Test.TestPlan TestPlan;

        public Domain.Test.TestSuite TestSuite;

        // public Domain.Core.Team Team;

        // public WorkItemType WorkItemType;
    }
}
