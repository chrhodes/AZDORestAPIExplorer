using System;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Dashboard.Presentation.Views
{
    public partial class WidgetMain : ViewBase, IWidgetMain, IInstanceCountV
    {

        public WidgetMain(ViewModels.IWidgetMainViewModel viewModel)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            InstanceCountV++;
            InitializeComponent();

            ViewModel = viewModel;

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

    }
}
