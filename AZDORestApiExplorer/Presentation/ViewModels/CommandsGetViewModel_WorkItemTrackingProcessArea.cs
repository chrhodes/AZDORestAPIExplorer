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
    // This file contains the Fields, Properties, and Methods for the Test Area
    public partial class CommandsGetViewModel : EventViewModelBase, IInstanceCountVM
    {
        #region Fields and Properties

        #endregion Fields and Properties

        #region Private Methods

        #region GetBehaviors Command

        public DelegateCommand GetBehaviorsWITPCommand { get; set; }
        public string GetBehaviorsWITPContent { get; set; } = "GetBehaviors";
        public string GetBehaviorsWITPToolTip { get; set; } = "GetBehaviors ToolTip";

        // Can get fancy and use Resources
        //public string BehaviorContent { get; set; } = "ViewName_BehaviorContent";
        //public string BehaviorToolTip { get; set; } = "ViewName_BehaviorContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_BehaviorContent">Behavior</system:String>
        //    <system:String x:Key="ViewName_BehaviorContentToolTip">Behavior ToolTip</system:String>

        public bool GetBehaviorsWITPCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null)
            {
                return false;
            }

            return true;
        }

        public void GetBehaviorsWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetBehaviorsEvent>().Publish(
                new GetBehaviorsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetBehaviors Command

        #region GetFields Command

        public DelegateCommand GetFieldsWITPCommand { get; set; }
        public string GetFieldsWITPContent { get; set; } = "Get Fields";
        public string GetFieldsWITPToolTip { get; set; } = "Get Fields ToolTip";

        // Can get fancy and use Resources
        //public string GetFieldsContent { get; set; } = "ViewName_GetFieldsContent";
        //public string GetFieldsToolTip { get; set; } = "ViewName_GetFieldsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetFieldsContent">GetFields</system:String>
        //    <system:String x:Key="ViewName_GetFieldsContentToolTip">GetFields ToolTip</system:String>

        public bool GetFieldsWITPCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWITP is null)
            {
                return false;
            }

            return true;
        }

        public void GetFieldsWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetFieldsWITPEvent>().Publish(
                new GetFieldsWITPEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWITP
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetFields Command

        #region GetLists Command

        public DelegateCommand GetListsCommand { get; set; }
        public string GetListsContent { get; set; } = "GetLists";
        public string GetListsToolTip { get; set; } = "GetLists ToolTip";

        // Can get fancy and use Resources
        //public string GetListsContent { get; set; } = "ViewName_GetListsContent";
        //public string GetListsToolTip { get; set; } = "ViewName_GetListsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetListsContent">GetLists</system:String>
        //    <system:String x:Key="ViewName_GetListsContentToolTip">GetLists ToolTip</system:String>

        public bool GetListsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetListsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetListsEvent>().Publish(
                new GetListsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetLists Command

        #region GetProcesses Command

        public DelegateCommand GetProcessesWITPCommand { get; set; }
        public string GetProcessesWITPContent { get; set; } = "Get Processes";
        public string GetProcessesWITPToolTip { get; set; } = "Get Processes ToolTip";

        // Can get fancy and use Resources
        //public string GetProcessesContent { get; set; } = "ViewName_GetProcessesContent";
        //public string GetProcessesToolTip { get; set; } = "ViewName_GetProcessesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProcessesContent">GetProcesses</system:String>
        //    <system:String x:Key="ViewName_GetProcessesContentToolTip">GetProcesses ToolTip</system:String>

        public bool GetProcessesWITPCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        public void GetProcessesWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetProcessesWITPEvent>().Publish(
                new GetProcessesWITPEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetProcesses Command

        #region GetRules Command

        public DelegateCommand GetRulesCommand { get; set; }
        public string GetRulesContent { get; set; } = "GetRules";
        public string GetRulesToolTip { get; set; } = "GetRules ToolTip";

        // Can get fancy and use Resources
        //public string GetRulesContent { get; set; } = "ViewName_GetRulesContent";
        //public string GetRulesToolTip { get; set; } = "ViewName_GetRulesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetRulesContent">GetRules</system:String>
        //    <system:String x:Key="ViewName_GetRulesContentToolTip">GetRules ToolTip</system:String>

        public bool GetRulesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWITP is null)
            {
                return false;
            }

            return true;
        }

        public void GetRulesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetRulesEvent>().Publish(
                new GetRulesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWITP
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetRules Command

        #region GetStates Command

        public DelegateCommand GetStatesWITPCommand { get; set; }
        public string GetStatesWITPContent { get; set; } = "GetStates";
        public string GetStatesWITPToolTip { get; set; } = "GetStates ToolTip";

        // Can get fancy and use Resources
        //public string GetStatesWITPContent { get; set; } = "ViewName_GetStatesWITPContent";
        //public string GetStatesWITPToolTip { get; set; } = "ViewName_GetStatesWITPContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetStatesContent">GetStates</system:String>
        //    <system:String x:Key="ViewName_GetStatesContentToolTip">GetStates ToolTip</system:String>

        public bool GetStatesWITPCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWITP is null)
            {
                return false;
            }

            return true;
        }

        public void GetStatesWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetStatesWITPEvent>().Publish(
                new GetStatesWITPEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWITP
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetStates Command

        #region GetSystemControls Command

        public DelegateCommand GetSystemControlsCommand { get; set; }
        public string GetSystemControlsContent { get; set; } = "GetSystemControls";
        public string GetSystemControlsToolTip { get; set; } = "GetSystemControls ToolTip";

        // Can get fancy and use Resources
        //public string GetSystemControlsContent { get; set; } = "ViewName_GetSystemControlsContent";
        //public string GetSystemControlsToolTip { get; set; } = "ViewName_GetSystemControlsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetSystemControlsContent">GetSystemControls</system:String>
        //    <system:String x:Key="ViewName_GetSystemControlsContentToolTip">GetSystemControls ToolTip</system:String>

        public bool GetSystemControlsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWITP is null)
            {
                return false;
            }

            return true;
        }

        public void GetSystemControlsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetSystemControlsEvent>().Publish(
              new GetSystemControlsEventArgs()
              {
                  Organization = _collectionMainViewModel.SelectedCollection.Organization
                  ,
                  Process = _contextMainViewModel.Context.SelectedProcess
                  ,
                  WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWITP
              });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetSystemControls Command

        #region GetWorkItemTypes Command

        public DelegateCommand GetWorkItemTypesWITPCommand { get; set; }
        public string GetWorkItemTypesWITPContent { get; set; } = "GetWorkItemTypes";
        public string GetWorkItemTypesWITPToolTip { get; set; } = "GetWorkItemTypes ToolTip";

        // Can get fancy and use Resources
        //public string GetProcessesContent { get; set; } = "ViewName_GetProcessesContent";
        //public string GetProcessesToolTip { get; set; } = "ViewName_GetProcessesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProcessesContent">GetProcesses</system:String>
        //    <system:String x:Key="ViewName_GetProcessesContentToolTip">GetProcesses ToolTip</system:String>

        public bool GetWorkItemTypesWITPCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null)
            {
                return false;
            }

            return true;
        }

        public void GetWorkItemTypesWITPExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetWorkItemTypesWITPEvent>().Publish(
                new GetWorkItemTypesWITPEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetWorkItemTypes Command

        #region GetWorkItemTypesBehaviors Command

        public DelegateCommand GetWorkItemTypesBehaviorsCommand { get; set; }
        public string GetWorkItemTypesBehaviorsContent { get; set; } = "GetWorkItemTypesBehaviors";
        public string GetWorkItemTypesBehaviorsToolTip { get; set; } = "GetWorkItemTypesBehaviors ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkItemTypesBehaviorsContent { get; set; } = "ViewName_GetWorkItemTypesBehaviorsContent";
        //public string GetWorkItemTypesBehaviorsToolTip { get; set; } = "ViewName_GetWorkItemTypesBehaviorsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkItemTypesBehaviorsContent">GetWorkItemTypesBehaviors</system:String>
        //    <system:String x:Key="ViewName_GetWorkItemTypesBehaviorsContentToolTip">GetWorkItemTypesBehaviors ToolTip</system:String>

        public bool GetWorkItemTypesBehaviorsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProcess is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWITP is null)
            {
                return false;
            }

            return true;
        }

        public void GetWorkItemTypesBehaviorsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetWorkItemTypesBehaviorsEvent>().Publish(
                new GetWorkItemTypesBehaviorsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Process = _contextMainViewModel.Context.SelectedProcess,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWITP
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion GetWorkItemTypesBehaviors Command

        #endregion Commands
    }
}