using AZDORestApiExplorer.Domain.Git;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.ModelWrappers
{
    public class PushWrapper : ModelWrapper<Push>
    {
        public PushWrapper(Push model) : base(model)
        {
        }
    }
}
