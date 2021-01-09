namespace AZDORestApiExplorer.Core.Events.WorkItemTracking
{
    public class GetStatesWITEventArgs
    {
        public Domain.Organization Organization;

        public Domain.Core.Project Project;

        public Domain.WorkItemTracking.WorkItemType WorkItemType;
    }
}