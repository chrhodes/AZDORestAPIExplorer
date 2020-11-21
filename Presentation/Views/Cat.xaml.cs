using System.Windows;
using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.Views
{
    public partial class TYPE : UserControl, ITYPE
    {

        public TYPE(ViewModels.ITYPEViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            Loaded += TYPE_Loaded;
        }

        private async void TYPE_Loaded(object sender, RoutedEventArgs e)
        {
            await ((ViewModels.ITYPEViewModel)ViewModel).LoadAsync();
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
