using AZDORestApiExplorer.Domain.Git;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.ModelWrappers
{
    public class BlobWrapper : ModelWrapper<Blob>
    {
        public BlobWrapper(Blob model) : base(model)
        {
        }
    }
}
