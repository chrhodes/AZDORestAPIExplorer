using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetFieldsEventArgs
    {
        public Organization Organization;

        public Process Process;

        public WorkItemType WorkItemType;
    }
}