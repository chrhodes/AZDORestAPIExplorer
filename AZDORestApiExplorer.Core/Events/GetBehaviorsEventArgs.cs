using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetBehaviorsEventArgs
    {
        public Organization Organization;

        public Process Process;

        // public WorkItemType WorkItemType;
    }
}