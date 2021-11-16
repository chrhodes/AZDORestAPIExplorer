using System;

using AZDORestApiExplorer.Domain.Git;
using AZDORestApiExplorer.Domain.Git.Events;
using AZDORestApiExplorer.Presentation.ViewModels;

using DevExpress.Xpf.Grid;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Git.Presentation.Views
{
    public partial class PullRequestMain : ViewBase, IInstanceCountV
    {

        // NOTE(crhodes)
        // Go back to using DomainViewModel!

        //// NOTE(crhodes)
        //// Go back to using specific ViewModel as PullRequestMain has lots of complexity.
        //// No longer single Grid.

        //public PullRequestMain(ViewModels.PullRequestMainViewModel viewModel)
        //{
        //    Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

        //    InstanceCountV++;
        //    InitializeComponent();

        //    ViewModel = viewModel;
        //    TargetGrid = grdResults;

        //    Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        //}

        public PullRequestMain(
            DomainViewModel2<PullRequest, GetPullRequestsEvent, GetPullRequestsEventArgs, SelectedPullRequestChangedEvent, ClearPullRequestsEvent> viewModel)
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

        private void LayoutGroup_ExpansionChanged(object sender, EventArgs e)
        {
            var lg = (DevExpress.Xpf.LayoutControl.LayoutGroup)sender;

            if (lg.IsCollapsed)
            {
                lg.Header = "Pull Request Selected - Expand to see Pull Requests Grid";
            }
            else
            {
                lg.Header = "";
            }

            var ea = e;
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
