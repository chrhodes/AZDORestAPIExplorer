using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events.Git
{
    public class GetCommitChangesEventArgs
    {
        public Organization Organization;

        // public Domain.Core.Process Process;

        public Domain.Core.Project Project;

        public Domain.Git.Repository Repository;

        public Domain.Git.Commit Commit;

        // public Domain.Core.Team Team;

        // public WorkItemType WorkItemType;
    }
}
