using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ModelWrappers.Core
{
    public class ProjectWrapper : ModelWrapper<Domain.Core.Project>
    {
        public ProjectWrapper(Domain.Core.Project model) : base(model)
        {
        }
    }
}
