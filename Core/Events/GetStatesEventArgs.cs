using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetStatesEventArgs
    {
        public CollectionDetails CollectionDetails;

        public Process Process;

        public WorkItemType WorkItemType;
    }
}
