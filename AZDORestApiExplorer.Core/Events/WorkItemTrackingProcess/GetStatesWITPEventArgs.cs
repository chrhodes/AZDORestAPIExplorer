using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess;

namespace AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess
{
    public class GetStatesWITPEventArgs
    {
        public Organization Organization;

        public Domain.Core.Process Process;

        public WorkItemType WorkItemType;
    }
}
