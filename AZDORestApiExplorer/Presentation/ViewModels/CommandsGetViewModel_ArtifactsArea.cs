using System;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain.Build;
using AZDORestApiExplorer.Domain.Build.Events;
using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Core.Events;
using AZDORestApiExplorer.Domain.Dashboard.Events;
using AZDORestApiExplorer.Domain.Git;
using AZDORestApiExplorer.Domain.Git.Events;
using AZDORestApiExplorer.Domain.Graph.Events;
using AZDORestApiExplorer.Domain.Test;
using AZDORestApiExplorer.Domain.Tokens.Events;
using AZDORestApiExplorer.Domain.WorkItemTracking;
using AZDORestApiExplorer.Domain.WorkItemTracking.Events;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess.Events;

using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public partial class CommandsGetViewModel : EventViewModelBase, IInstanceCountVM
    {
        #region Fields and Properties

        #endregion Fields and Properties

        #region Private Methods

        #region GetFeeds Command

        public DelegateCommand GetFeedsCommand { get; set; }
        public string GetFeedsContent { get; set; } = "GetFeeds";
        public string GetFeedsToolTip { get; set; } = "GetFeeds ToolTip";

        // Can get fancy and use Resources
        //public string GetFeedsContent { get; set; } = "ViewName_GetFeedsContent";
        //public string GetFeedsToolTip { get; set; } = "ViewName_GetFeedsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetFeedsContent">GetFeeds</system:String>
        //    <system:String x:Key="ViewName_GetFeedsContentToolTip">GetFeeds ToolTip</system:String>

        public void GetFeeds()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Artifacts.Events.GetFeedsEvent>().Publish(
                new Domain.Artifacts.Events.GetFeedsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetFeedsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetFeeds Command

        #endregion Public Methods

   }
}