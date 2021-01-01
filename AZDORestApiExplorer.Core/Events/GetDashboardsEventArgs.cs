using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetDashboardsEventArgs
    {
        public Organization Organization;

        public Project Project;

        public Team Team;
    }
}