using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess;

namespace AZDORestApiExplorer.Application
{
    public class Context
    {
        public Domain.Core.Process SelectedProcess { get; set; }
        public Project SelectedProject { get; set; }
        public Team SelectedTeam { get; set; }

        public WorkItemType SelectedWorkItemType { get; set; }
        public Domain.Dashboard.Dashboard SelectedDashboard { get; set; }
    }
}
