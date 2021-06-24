using AZDORestApiExplorer.Domain.Git;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.ModelWrappers
{
    public class CommitWrapper : ModelWrapper<Commit>
    {
        public CommitWrapper(Commit model) : base(model)
        {
        }
    }
}
