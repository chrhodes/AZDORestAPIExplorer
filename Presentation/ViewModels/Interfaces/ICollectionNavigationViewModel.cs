using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public interface ICollectionNavigationViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
