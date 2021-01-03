using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess;

namespace AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess
{
    public class GetBehaviorsEventArgs
    {
        public Organization Organization;

        public Process Process;

        // public WorkItemType WorkItemType;
    }
}