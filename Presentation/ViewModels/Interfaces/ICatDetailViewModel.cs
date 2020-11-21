using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public interface ITYPEDetailViewModel : IViewModel
    {
        Task LoadAsync(int id);
    }
}
