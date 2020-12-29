using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public interface IProjectNavigationViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
