using AZDORestApiExplorer.Domain.Git;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.ModelWrappers
{
    public class PullRequestWrapper : ModelWrapper<PullRequest>
    {
        public PullRequestWrapper(PullRequest model) : base(model)
        {
        }
    }
}
