using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events.WorkItemTracking
{
    public class GetWorkItemTypesWITEventArgs
    {
        public Organization Organization;

        public Domain.Core.Project Project;
    }
}