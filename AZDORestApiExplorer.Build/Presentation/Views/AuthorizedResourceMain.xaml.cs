using System;

using AZDORestApiExplorer.Domain.Build;
using AZDORestApiExplorer.Domain.Build.Events;
using AZDORestApiExplorer.Presentation.ViewModels;

using DevExpress.Xpf.Grid;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Build.Presentation.Views
{
    public partial class AuthorizedResourceMain : ViewBase, IInstanceCountV
    {

        public AuthorizedResourceMain(DomainViewModel<AuthorizedResource, GetAuthorizedResourcesEvent, GetAuthorizedResourcesEventArgs, SelectedAuthorizedResourceChangedEvent> viewModel)
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
