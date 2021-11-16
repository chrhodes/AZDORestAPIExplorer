using System;
using System.Windows;

using AZDORestApiExplorer.ViewModels;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.Views
{
    public partial class MainWindow : Window, IInstanceCountV
    {
        public MainWindowViewModel _viewModel;

        public MainWindow(MainWindowViewModel viewModel)
        {
            Int64 startTicks = Log.CONSTRUCTOR($"Enter ({viewModel.GetType()})", Common.LOG_CATEGORY);

            InstanceCountV++;
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #region IInstanceCount

        private static int _instanceCountV;

        public int InstanceCountV
        {
            get => _instanceCountV;
            set => _instanceCountV = value;
        }

        #endregion

        private void SaveLayout_Click(object sender, RoutedEventArgs e)
        {
            lg_Body_dlm.SaveLayoutToXml(@"C:\temp\AZDORestApiExplorerLayout.xml");
        }

        private void RestoreLayout_Click(object sender, RoutedEventArgs e)
        {
            lg_Body_dlm.RestoreLayoutFromXml(@"C:\temp\AZDORestApiExplorerLayout.xml");
        }
    }
}
