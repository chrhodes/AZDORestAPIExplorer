using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetFieldsEventArgs
    {
        public CollectionDetails CollectionDetails;

        public Process Process;

        public WorkItemType WorkItemType;
    }
}