using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.Views
{
    public partial class TYPEDetail : UserControl, ITYPEDetail
    {
        public TYPEDetail(ViewModels.ITYPEDetailViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
