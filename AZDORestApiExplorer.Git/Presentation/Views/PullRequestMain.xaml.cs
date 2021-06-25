using System;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.Views
{
    public partial class PullRequestMain : ViewBase, IPullRequestMain, IInstanceCountV
    {

        public PullRequestMain(ViewModels.IPullRequestMainViewModel viewModel)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            InstanceCountV++;
            InitializeComponent();

            ViewModel = viewModel;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        #region IInstanceCount

        private static int _instanceCountV;

        public int InstanceCountV
        {
            get => _instanceCountV;
            set => _instanceCountV = value;
        }

        #endregion

        private void ExportToExcel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            grdResults.View.ExportToXlsx(@"C:\temp\grdResults.xlsx");
        }

        private void ExportToWord_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            grdResults.View.ExportToDocx(@"C:\temp\grdResults.docx");
        }

        private void ExportToHtml_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            grdResults.View.ExportToHtml(@"C:\temp\grdResults.html");
        }

        private void ExportToPdf(object sender, System.Windows.RoutedEventArgs e)
        {
            grdResults.View.ExportToPdf(@"C:\temp\grdResults.pdf");
        }

        private void Export_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var s = sender;
            var rea = e;

            int i = 42;
        }

        private void defaultClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var s = sender;
            var rea = e;

            int i = 42;
        }
    }
}
