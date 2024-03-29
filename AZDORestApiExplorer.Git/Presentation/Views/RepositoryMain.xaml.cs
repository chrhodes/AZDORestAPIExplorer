using System;

using AZDORestApiExplorer.Domain.Git;
using AZDORestApiExplorer.Domain.Git.Events;
using AZDORestApiExplorer.Presentation.ViewModels;

using DevExpress.Xpf.Grid;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.Views
{
    public partial class RepositoryMain : ViewBase, IInstanceCountV
    {

        public RepositoryMain(DomainViewModel<GitRepository, GetRepositoriesEvent, GetRepositoriesEventArgs, SelectedRepositoryChangedEvent> viewModel)
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

        private void Repositories_LayoutGroup_ExpansionChanged(object sender, EventArgs e)
        {
            var lg = (DevExpress.Xpf.LayoutControl.LayoutGroup)sender;

            if (lg.IsCollapsed)
            {
                lg.Header = "Repository Selected - Expand to see Repositories Grid";
            }
            else
            {
                lg.Header = "";
            }

            var ea = e;
        }
    }
}
