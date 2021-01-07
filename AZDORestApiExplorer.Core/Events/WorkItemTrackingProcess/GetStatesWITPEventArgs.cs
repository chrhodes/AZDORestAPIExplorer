namespace AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess
{
    public class GetStatesWITPEventArgs
    {
        public Domain.Organization Organization;

        public Domain.Core.Process Process;

        public Domain.WorkItemTrackingProcess.WorkItemType WorkItemType;
    }
}
