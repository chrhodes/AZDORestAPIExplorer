namespace AZDORestApiExplorer.Core.Events.WorkItemTracking
{
    public class GetWorkItemTypesFieldsWITEventArgs
    {
        public Domain.Organization Organization;

        public Domain.Core.Project Project;

        public Domain.WorkItemTrackingProcess.WorkItemType WorkItemType;
    }
}