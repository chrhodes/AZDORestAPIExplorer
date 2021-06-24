using AZDORestApiExplorer.Domain.Git;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.ModelWrappers
{
    public class ItemWrapper : ModelWrapper<Item>
    {
        public ItemWrapper(Item model) : base(model)
        {
        }
    }
}
