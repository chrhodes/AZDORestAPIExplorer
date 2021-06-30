using System;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.Views
{
    public partial class StatMain : ViewBase, IStatMain, IInstanceCountV
    {

        public StatMain(ViewModels.IStatMainViewModel viewModel)
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