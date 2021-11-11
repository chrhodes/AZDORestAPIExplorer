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
    // This file contains the Fields, Properties, and Methods for the Graph Area
    public partial class CommandsGetViewModel : EventViewModelBase, IInstanceCountVM
    {
        #region Fields and Properties

        #endregion Fields and Properties

        #region Private Methods

        #region GetGroups Command

        public DelegateCommand GetGroupsCommand { get; set; }
        public string GetGroupsContent { get; set; } = "GetGroups";
        public string GetGroupsToolTip { get; set; } = "GetGroups ToolTip";

        // Can get fancy and use Resources
        //public string GetGroupsContent { get; set; } = "ViewName_GetGroupsContent";
        //public string GetGroupsToolTip { get; set; } = "ViewName_GetGroupsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetGroupsContent">GetGroups</system:String>
        //    <system:String x:Key="ViewName_GetGroupsContentToolTip">GetGroups ToolTip</system:String>

        public void GetGroups()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetGroupsEvent>().Publish(
                new GetGroupsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetGroupsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetGroups Command

        #region GetUsers Command

        public DelegateCommand GetUsersCommand { get; set; }
        public string GetUsersContent { get; set; } = "GetUsers";
        public string GetUsersToolTip { get; set; } = "GetUsers ToolTip";

        // Can get fancy and use Resources
        //public string GetUsersContent { get; set; } = "ViewName_GetUsersContent";
        //public string GetUsersToolTip { get; set; } = "ViewName_GetUsersContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetUsersContent">GetUsers</system:String>
        //    <system:String x:Key="ViewName_GetUsersContentToolTip">GetUsers ToolTip</system:String>

        public void GetUsers()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetUsersEvent>().Publish(
                new GetUsersEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetUsersCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetUsers

        #endregion Commands
    }
}