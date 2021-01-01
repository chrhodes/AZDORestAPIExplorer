using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetWorkItemTypesEventArgs
    {
        public Organization Organization;

        public Process Process;
    }
}
