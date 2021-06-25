using AZDORestApiExplorer.Domain.Git;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.ModelWrappers
{
    public class CommitChangeWrapper : ModelWrapper<CommitChange>
    {
        public CommitChangeWrapper(CommitChange model) : base(model)
        {
        }
    }
}
