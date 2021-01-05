using AZDORestApiExplorer.Domain;

namespace AZDORestApiExplorer.WorkItemTracking.Core.Events
{
    public class GetTagsEventArgs
    {
        public Organization Organization;

        public Domain.Core.Project Project;
    }
}