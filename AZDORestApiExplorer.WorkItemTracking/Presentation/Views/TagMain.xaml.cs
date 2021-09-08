using System;

using DevExpress.Xpf.Grid;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.WorkItemTracking.Presentation.Views
{
    public partial class TagMain : ViewBase, IInstanceCountV
    {

        public TagMain(ViewModels.TagMainViewModel viewModel)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            InstanceCountV++;
            InitializeComponent();

            ViewModel = viewModel;
            TargetGrid = grdResults;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private GridControl _targetGrid;

        public GridControl TargetGrid
        {
            get => _targetGrid;
            set => _targetGrid = value;
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
