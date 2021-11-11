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

        #region GetTestsPlan Command

        public DelegateCommand GetTestPlansCommand { get; set; }
        public string GetTestPlansContent { get; set; } = "Get Test Plans";
        public string GetTestPlansToolTip { get; set; } = "Get Test Plans ToolTip";

        // Can get fancy and use Resources
        //public string GetTestsPlanContent { get; set; } = "ViewName_GetTestsPlanContent";
        //public string GetTestsPlanToolTip { get; set; } = "ViewName_GetTestsPlanContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTestsPlanContent">GetTestsPlan</system:String>
        //    <system:String x:Key="ViewName_GetTestsPlanContentToolTip">GetTestsPlan ToolTip</system:String>

        public void GetTestPlans()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Test.Events.GetTestPlansEvent>().Publish(
                new Domain.Test.Events.GetTestPlansEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetTestPlansCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetTestsPlan Command

        #region GetTestSuites Command

        public DelegateCommand GetTestSuitesCommand { get; set; }
        public string GetTestSuitesContent { get; set; } = "GetTestSuites";
        public string GetTestSuitesToolTip { get; set; } = "GetTestSuites ToolTip";

        // Can get fancy and use Resources
        //public string GetTestSuitesContent { get; set; } = "ViewName_GetTestSuitesContent";
        //public string GetTestSuitesToolTip { get; set; } = "ViewName_GetTestSuitesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTestSuitesContent">GetTestSuites</system:String>
        //    <system:String x:Key="ViewName_GetTestSuitesContentToolTip">GetTestSuites ToolTip</system:String>

        public void GetTestSuites()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Test.Events.GetTestSuitesEvent>().Publish(
                new Domain.Test.Events.GetTestSuitesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    TestPlan = _contextMainViewModel.Context.SelectedTestPlan
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetTestSuitesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedTestPlan is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetTestSuites Command

        #region GetTestCases Command

        public DelegateCommand GetTestCasesCommand { get; set; }
        public string GetTestCasesContent { get; set; } = "GetTestCases";
        public string GetTestCasesToolTip { get; set; } = "GetTestCases ToolTip";

        // Can get fancy and use Resources
        //public string GetTestCasesContent { get; set; } = "ViewName_GetTestCasesContent";
        //public string GetTestCasesToolTip { get; set; } = "ViewName_GetTestCasesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTestCasesContent">GetTestCases</system:String>
        //    <system:String x:Key="ViewName_GetTestCasesContentToolTip">GetTestCases ToolTip</system:String>

        public void GetTestCases()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Test.Events.GetTestCasesEvent>().Publish(
                new Domain.Test.Events.GetTestCasesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    TestPlan = _contextMainViewModel.Context.SelectedTestPlan,
                    TestSuite = _contextMainViewModel.Context.SelectedTestSuite
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetTestCasesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedTestPlan is null
                || _contextMainViewModel.Context.SelectedTestSuite is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetTestCases Command

        #region GetTestPoints Command

        public DelegateCommand GetTestPointsCommand { get; set; }
        public string GetTestPointsContent { get; set; } = "GetTestPoints";
        public string GetTestPointsToolTip { get; set; } = "GetTestPoints ToolTip";

        // Can get fancy and use Resources
        //public string GetTestPointsContent { get; set; } = "ViewName_GetTestPointsContent";
        //public string GetTestPointsToolTip { get; set; } = "ViewName_GetTestPointsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTestPointsContent">GetTestPoints</system:String>
        //    <system:String x:Key="ViewName_GetTestPointsContentToolTip">GetTestPoints ToolTip</system:String>

        public void GetTestPoints()
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.Test.Events.GetTestPointsEvent>().Publish(
                new Domain.Test.Events.GetTestPointsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    TestPlan = _contextMainViewModel.Context.SelectedTestPlan.id,
                    TestSuite = _contextMainViewModel.Context.SelectedTestSuite.id,
                    //TestPlan = _contextMainViewModel.Context.SelectedTestPlan,
                    //TestSuite = _contextMainViewModel.Context.SelectedTestSuite
                });

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetTestPointsCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            return true;
        }

        #endregion GetTestPoints Command

        #endregion Commands
    }
}