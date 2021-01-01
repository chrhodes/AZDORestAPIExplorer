using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetStatesEventArgs
    {
        public Organization Organization;

        public Process Process;

        public WorkItemType WorkItemType;
    }
}
