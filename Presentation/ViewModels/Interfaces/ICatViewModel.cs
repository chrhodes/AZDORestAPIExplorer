using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public interface ITYPEViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
