using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetSystemControlsEventArgs
    {
        public Organization Organization;

        public Process Process;

        public WorkItemType WorkItemType;
    }
}