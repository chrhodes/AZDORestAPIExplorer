using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public interface IProcessNavigationViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
