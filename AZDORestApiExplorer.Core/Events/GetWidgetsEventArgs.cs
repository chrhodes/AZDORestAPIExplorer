using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Core;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetWidgetsEventArgs
    {
        public Organization Organization;

        public Project Project;

        public Team Team;

        public Domain.Dashboard.Dashboard Dashboard;
    }
}