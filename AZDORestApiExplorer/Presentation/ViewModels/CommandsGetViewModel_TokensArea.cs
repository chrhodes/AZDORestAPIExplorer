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
    // NOTE(crhodes)
    // This file contains the Fields, Properties, and Methods for the Tokens Area
    public partial class CommandsGetViewModel : EventViewModelBase, IInstanceCountVM
    {
        #region Fields and Properties

        #endregion Fields and Properties

        #region Private Methods

        #region GetPats Command

        public DelegateCommand GetPatsCommand { get; set; }
        public string GetPatsContent { get; set; } = "GetPats";
        public string GetPatsToolTip { get; set; } = "GetPats ToolTip";

        // Can get fancy and use Resources
        //public string GetPatsContent { get; set; } = "ViewName_GetPatsContent";
        //public string GetPatsToolTip { get; set; } = "ViewName_GetPatsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetPatsContent">GetPats</system:String>
        //    <system:String x:Key="ViewName_GetPatsContentToolTip">GetPats ToolTip</system:String>

        public void GetPats()
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetPatsEvent>().Publish(
                new GetPatsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetPatsCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            return true;
        }

        #endregion GetPats Command

        #endregion Tokens Area

    }
}