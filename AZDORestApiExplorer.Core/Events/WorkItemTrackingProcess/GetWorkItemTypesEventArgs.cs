using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess
{
    public class GetWorkItemTypesEventArgs
    {
        public Organization Organization;

        public Domain.Core.Process Process;
    }
}
