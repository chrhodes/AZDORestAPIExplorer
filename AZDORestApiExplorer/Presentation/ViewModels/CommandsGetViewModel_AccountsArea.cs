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
    // This file contains the Fields, Properties, and Methods for the Accounts Area
    public partial class CommandsGetViewModel : EventViewModelBase, IInstanceCountVM
    {

        #region Fields and Properties

        #endregion Fields and Properties

        #region Private Methods

        #region GetAccounts Command

        public DelegateCommand GetAccountsCommand { get; set; }
        public string GetAccountsContent { get; set; } = "GetAccounts";
        public string GetAccountsToolTip { get; set; } = "GetAccounts ToolTip";

        // Can get fancy and use Resources
        //public string GetAccountsContent { get; set; } = "ViewName_GetAccountsContent";
        //public string GetAccountsToolTip { get; set; } = "ViewName_GetAccountsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetAccountsContent">GetAccounts</system:String>
        //    <system:String x:Key="ViewName_GetAccountsContentToolTip">GetAccounts ToolTip</system:String>



        public void GetAccountsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Accounts.Events.GetAccountsEvent>().Publish(
                new Domain.Accounts.Events.GetAccountsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    //, Project = _contextMainViewModel.Context.SelectedProject
                    //, Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetAccountsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion Accounts Area

        #endregion Public Methods

    }
}