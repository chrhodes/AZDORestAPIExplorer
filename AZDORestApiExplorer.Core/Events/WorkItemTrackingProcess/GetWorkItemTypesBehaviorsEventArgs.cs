namespace AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess
{
    public class GetWorkItemTypesBehaviorsEventArgs
    {
        public Domain.Organization Organization;

        public Domain.Core.Process Process;

        public Domain.WorkItemTrackingProcess.WorkItemType WorkItemType;
    }
}
