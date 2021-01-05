using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess
{
    public class GetWorkItemTypesWITPEventArgs
    {
        public Organization Organization;

        public Domain.Core.Process Process;
    }
}
