using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events.WorkItemTracking
{
    public class GetFieldsWITEventArgs
    {
        public Organization Organization;

        public Domain.Core.Project Project;
        // public Process Process;

        // public WorkItemType WorkItemType;
    }
}