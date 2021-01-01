using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Core;

namespace AZDORestApiExplorer.Core.Events.Dashboard
{
    public class GetDashboardsEventArgs
    {
        public Organization Organization;

        public Project Project;

        public Team Team;
    }
}