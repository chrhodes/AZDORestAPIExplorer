namespace AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess
{
    public class GetRulesEventArgs
    {
        public Domain.Organization Organization;

        public Domain.Core.Process Process;

        public Domain.WorkItemTrackingProcess.WorkItemType WorkItemType;
    }
}