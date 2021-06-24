using AZDORestApiExplorer.Domain.Git;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.ModelWrappers
{
    public class RefWrapper : ModelWrapper<Ref>
    {
        public RefWrapper(Ref model) : base(model)
        {
        }
    }
}
