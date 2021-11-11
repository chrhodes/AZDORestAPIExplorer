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
    // This file contains the Fields, Properties, and Methods for the Core Area
    public partial class CommandsGetViewModel : EventViewModelBase, IInstanceCountVM
    {
        #region Fields and Properties

        #endregion Fields and Properties

        #region Private Methods

        #region GetCoreProcesses Command

        public DelegateCommand GetCoreProcessesCommand { get; set; }
        public string GetCoreProcessesContent { get; set; } = "Get Processes";
        public string GetCoreProcessesToolTip { get; set; } = "Get Processes ToolTip";

        // Can get fancy and use Resources
        //public string GetProcessesContent { get; set; } = "ViewName_GetProcessesContent";
        //public string GetProcessesToolTip { get; set; } = "ViewName_GetProcessesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProcessesContent">GetProcesses</system:String>
        //    <system:String x:Key="ViewName_GetProcessesContentToolTip">GetProcesses ToolTip</system:String>

        public void GetCoreProcessesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Core.Events.GetProcessesEvent>().Publish(
                new Domain.Core.Events.GetProcessesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetCoreProcessesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetCoreProcesses Command

        #region GetCoreProjects Command

        public DelegateCommand GetProjectsCommand { get; set; }
        public string GetProjectsContent { get; set; } = "Get Projects";
        public string GetProjectsToolTip { get; set; } = "Get Projects ToolTip";

        // Can get fancy and use Resources
        //public string Get_CoreProjectsContent { get; set; } = "ViewName_Get_Core_ProjectsContent";
        //public string Get_CoreProjectsToolTip { get; set; } = "ViewName_Get_Core_ProjectsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_Get_Core_ProjectsContent">Get_Core_Projects</system:String>
        //    <system:String x:Key="ViewName_Get_Core_ProjectsContentToolTip">Get_Core_Projects ToolTip</system:String>

        public void GetProjectsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            //EventAggregator.GetEvent<GetProjectsEvent>().Publish(
            //    new GetProjectsEventArgs()
            //    {
            //        Organization = _collectionMainViewModel.SelectedCollection.Organization
            //    });

            EventAggregator.GetEvent<Domain.Core.Events.GetProjectsEvent>().Publish(
                new Domain.Core.Events.GetProjectsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetProjectsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetCoreProjects Command

        #region GetTeams Command

        public DelegateCommand GetTeamsCommand { get; set; }
        public string GetTeamsContent { get; set; } = "GetTeams";
        public string GetTeamsToolTip { get; set; } = "GetTeams ToolTip";

        // Can get fancy and use Resources
        //public string GetTeamsContent { get; set; } = "ViewName_GetTeamsContent";
        //public string GetTeamsToolTip { get; set; } = "ViewName_GetTeamsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTeamsContent">GetTeams</system:String>
        //    <system:String x:Key="ViewName_GetTeamsContentToolTip">GetTeams ToolTip</system:String>



        public void GetTeamsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Core.Events.GetTeamsEvent>().Publish(
                new Domain.Core.Events.GetTeamsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetTeamsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetTeams Command

        #endregion Commands
    }
}