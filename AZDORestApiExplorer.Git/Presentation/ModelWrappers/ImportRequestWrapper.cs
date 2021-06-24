using AZDORestApiExplorer.Domain.Git;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.ModelWrappers
{
    public class ImportRequestWrapper : ModelWrapper<ImportRequest>
    {
        public ImportRequestWrapper(ImportRequest model) : base(model)
        {
        }
    }
}
