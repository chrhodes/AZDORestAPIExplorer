using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetDashboardsEventArgs
    {
        public CollectionDetails CollectionDetails;

        public Project Project;

        public Team Team;
    }
}