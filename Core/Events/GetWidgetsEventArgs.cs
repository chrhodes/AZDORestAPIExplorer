using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetWidgetsEventArgs
    {
        public CollectionDetails CollectionDetails;

        public Project Project;

        public Team Team;

        public Dashboard Dashboard;
    }
}