using System.Threading.Tasks;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public interface ITeamMainViewModel : IViewModel
    {
        Task LoadAsync();
    }
}
