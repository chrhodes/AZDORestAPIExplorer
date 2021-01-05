using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events.WorkItemTracking
{
    public class GetWorkItemTypesFieldsWITEventArgs
    {
        public Organization Organization;

        public Domain.Core.Project Project;
    }
}