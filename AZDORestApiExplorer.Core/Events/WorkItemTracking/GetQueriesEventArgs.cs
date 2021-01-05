using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events.WorkItemTracking
{
    public class GetQueriesEventArgs
    {
        public Organization Organization;

        public Domain.Core.Project Project;
    }
}