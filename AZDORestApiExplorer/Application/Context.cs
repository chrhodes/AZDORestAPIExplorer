using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Core;

namespace AZDORestApiExplorer.Application
{
    public class Context
    {
        public Project SelectedProject { get; set; }
        public Team SelectedTeam { get; set; }
        public Process SelectedProcess { get; set; }
        public WorkItemType SelectedWorkItemType { get; set; }
        public Domain.Dashboard.Dashboard SelectedDashboard { get; set; }
    }
}
