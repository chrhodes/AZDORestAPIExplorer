using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.WorkItemTracking;

namespace AZDORestApiExplorer.Core.Events.WorkItemTracking
{
    public class GetStatesWITEventArgs
    {
        public Organization Organization;

        public Project Project;

        public WorkItemType WorkItemType;
    }
}