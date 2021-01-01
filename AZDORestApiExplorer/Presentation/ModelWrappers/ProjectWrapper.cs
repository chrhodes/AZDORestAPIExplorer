using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ModelWrappers
{
    public class ProjectWrapper : ModelWrapper<Domain.Project>
    {
        public ProjectWrapper(Domain.Project model) : base(model)
        {
        }
    }
}
