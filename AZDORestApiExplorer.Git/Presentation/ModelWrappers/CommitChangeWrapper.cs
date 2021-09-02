using AZDORestApiExplorer.Domain.Git;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.ModelWrappers
{
    public class CommitChangeWrapper : ModelWrapper<Change>
    {
        public CommitChangeWrapper(Change model) : base(model)
        {
        }
    }
}
