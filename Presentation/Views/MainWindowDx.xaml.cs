using System.Windows;

using AZDORestApiExplorer.ViewModels;

namespace AZDORestApiExplorer.Presentation.Views
{
    public partial class MainWindowDx : Window
    {
        public MainWindowDxViewModel _viewModel;

        public MainWindowDx(MainWindowDxViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;

            Loaded += MainWindowDx_Loaded;
        }

        // Load ViewModel asynchronously

        private async void MainWindowDx_Loaded(object sender, RoutedEventArgs e)
        {
            //_viewModel.Load();
            //await _viewModel.LoadAsync();
        }
    }
}
