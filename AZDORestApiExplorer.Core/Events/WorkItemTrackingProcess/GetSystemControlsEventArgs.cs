namespace AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess
{
    public class GetSystemControlsEventArgs
    {
        public Domain.Organization Organization;

        public Domain.Core.Process Process;

        public Domain.WorkItemTrackingProcess.WorkItemType WorkItemType;
    }
}