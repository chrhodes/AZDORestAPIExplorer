using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess;

namespace AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess
{
    public class GetWorkItemTypesEventArgs
    {
        public Organization Organization;

        public Process Process;
    }
}
