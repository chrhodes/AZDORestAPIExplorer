using AZDORestApiExplorer.Domain.WorkItemTracking;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.WorkItemTracking.Presentation.ModelWrappers
{
    public class WorkItemWrapper : ModelWrapper<WorkItem>
    {
        public WorkItemWrapper(WorkItem model) : base(model)
        {
        }
    }
}
