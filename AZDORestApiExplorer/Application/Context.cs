using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Application
{
    public class Context
    {
        public Project SelectedProject { get; set; }
        public Team SelectedTeam { get; set; }
        public Process SelectedProcess { get; set; }
        public WorkItemType SelectedWorkItemType { get; set; }
        public Dashboard SelectedDashboard { get; set; }
    }
}
