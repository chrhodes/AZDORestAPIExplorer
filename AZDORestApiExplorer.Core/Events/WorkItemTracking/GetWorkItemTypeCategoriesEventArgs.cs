using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.Core.Events.WorkItemTracking
{
    public class GetWorkItemTypeCategoriesEventArgs
    {
        public Organization Organization;

        public Domain.Core.Project Project;
    }
}