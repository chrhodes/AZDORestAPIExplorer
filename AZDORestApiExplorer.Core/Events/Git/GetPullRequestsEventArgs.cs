using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events.Git
{
    public class GetPullRequestsEventArgs
    {
        public Organization Organization;

        // public Domain.Core.Process Process;

        public Domain.Core.Project Project;

        public Domain.Git.Repository Repository;

        // public Domain.Core.Team Team;

        // public WorkItemType WorkItemType;
    }
}
