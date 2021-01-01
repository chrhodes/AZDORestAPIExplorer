using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetRulesEventArgs
    {
        public Organization Organization;

        public Process Process;

        public WorkItemType WorkItemType;
    }
}