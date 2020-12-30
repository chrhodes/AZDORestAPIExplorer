using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetWorkItemTypesEventArgs
    {
        public CollectionDetails CollectionDetails;

        public Process Process;
    }
}
