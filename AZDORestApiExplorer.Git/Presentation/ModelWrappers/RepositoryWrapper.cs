using AZDORestApiExplorer.Domain.Git;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.ModelWrappers
{
    public class RepositoryWrapper : ModelWrapper<Repository>
    {
        public RepositoryWrapper(Repository model) : base(model)
        {
        }
    }
}
