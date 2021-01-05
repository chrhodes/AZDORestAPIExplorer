using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess
{
    public class GetBehaviorsEventArgs
    {
        public Organization Organization;

        public Domain.Core.Process Process;

        // public WorkItemType WorkItemType;
    }
}