using System;

using DevExpress.XtraPrinting;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.WorkItemTracking.Presentation.Views
{
    public partial class FieldMain : ViewBase, IFieldMain, IInstanceCountV
    {

        public FieldMain(ViewModels.IFieldMainViewModel viewModel) : base()
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            InstanceCountV++;
            InitializeComponent();

            ViewModel = viewModel;

            // TODO(crhodes)
            // Determine if need to do this or if the plumbing is broken.

            // Doing this so can have the export to excel in view model.  Maybe we just let this happen in the code behind.
            // See ExportToExcel_Click below.

            ViewModel.View = this;

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
            // TODO(crhodes)
            // Maybe launch file picker to get name.

            XlsxExportOptions options = new XlsxExportOptions();
            options.SheetName = "WITFields";
            gcMainTable.View.ExportToXlsx(@"C:\temp\FieldData.xlsx",options);
        }
    }
}
