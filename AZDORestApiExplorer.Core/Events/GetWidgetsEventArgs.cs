using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetWidgetsEventArgs
    {
        public Organization Organization;

        public Project Project;

        public Team Team;

        public Dashboard Dashboard;
    }
}