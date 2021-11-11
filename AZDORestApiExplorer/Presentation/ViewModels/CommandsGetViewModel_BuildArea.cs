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
    // This file contains the Fields, Properties, and Methods for the Build
    public partial class CommandsGetViewModel : EventViewModelBase, IInstanceCountVM
    {

        #region Fields and Properties


        #endregion Fields and Properties

        #region Private Methods

        private void CallBuildDependentStuff(Domain.Build.Build build)
        {
            if (!(build is null))
            {
                // Info about Build

                EventAggregator.GetEvent<GetBuildInfoEvent>().Publish(
                    new GetBuildInfoEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Build = build
                    });

                // Info about Changes

                EventAggregator.GetEvent<GetBuildChangesEvent>().Publish(
                    new GetBuildChangesEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Build = build
                    });

                // Info about Logs

                EventAggregator.GetEvent<GetBuildLogsEvent>().Publish(
                    new GetBuildLogsEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Build = build
                    });

                // Info about WorkItemRefs

                EventAggregator.GetEvent<GetBuildWorkItemRefsEvent>().Publish(
                    new GetBuildWorkItemRefsEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Build = build
                    });
            }
        }

        #region GetAuthorizedResources Command

        public DelegateCommand GetAuthorizedResourcesCommand { get; set; }
        public string GetAuthorizedResourcesContent { get; set; } = "GetAuthorizedResources";
        public string GetAuthorizedResourcesToolTip { get; set; } = "GetAuthorizedResources ToolTip";

        // Can get fancy and use Resources
        //public string GetAuthorizedResourcesContent { get; set; } = "ViewName_GetAuthorizedResourcesContent";
        //public string GetAuthorizedResourcesToolTip { get; set; } = "ViewName_GetAuthorizedResourcesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetAuthorizedResourcesContent">GetAuthorizedResources</system:String>
        //    <system:String x:Key="ViewName_GetAuthorizedResourcesContentToolTip">GetAuthorizedResources ToolTip</system:String>

        public void GetAuthorizedResources()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetAuthorizedResourcesEvent>().Publish(
                new GetAuthorizedResourcesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetAuthorizedResourcesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetAuthorizedResources Command

        #region GetBuilds Command

        public DelegateCommand GetBuildsCommand { get; set; }
        public string GetBuildsContent { get; set; } = "GetBuilds";
        public string GetBuildsToolTip { get; set; } = "GetBuilds ToolTip";

        // Can get fancy and use Resources
        //public string GetBuildsContent { get; set; } = "ViewName_GetBuildsContent";
        //public string GetBuildsToolTip { get; set; } = "ViewName_GetBuildsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetBuildsContent">GetBuilds</system:String>
        //    <system:String x:Key="ViewName_GetBuildsContentToolTip">GetBuilds ToolTip</system:String>

        public void GetBuilds()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetBuildsEvent>().Publish(
                new GetBuildsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetBuildsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetBuilds Command

        #region GetBuildInfo Command

        public DelegateCommand GetBuildInfoCommand { get; set; }
        public string GetBuildInfoContent { get; set; } = "GetBuildInfo";
        public string GetBuildInfoToolTip { get; set; } = "GetBuildInfo ToolTip";

        // Can get fancy and use Resources
        //public string GetBuildInfoContent { get; set; } = "ViewName_GetBuildInfoContent";
        //public string GetBuildInfoToolTip { get; set; } = "ViewName_GetBuildInfoContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetBuildInfoContent">GetBuildInfo</system:String>
        //    <system:String x:Key="ViewName_GetBuildInfoContentToolTip">GetBuildInfo ToolTip</system:String>

        public void GetBuildInfo()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetBuildInfoEvent>().Publish(
                new GetBuildInfoEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Build = _contextMainViewModel.Context.SelectedBuild
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetBuildInfoCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedBuild is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetBuildInfo Command

        #region GetBuildChanges Command

        public DelegateCommand GetBuildChangesCommand { get; set; }
        public string GetBuildChangesContent { get; set; } = "GetBuildChanges";
        public string GetBuildChangesToolTip { get; set; } = "GetBuildChanges ToolTip";

        // Can get fancy and use Resources
        //public string GetBuildChangesContent { get; set; } = "ViewName_GetBuildChangesContent";
        //public string GetBuildChangesToolTip { get; set; } = "ViewName_GetBuildChangesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetBuildChangesContent">GetBuildChanges</system:String>
        //    <system:String x:Key="ViewName_GetBuildChangesContentToolTip">GetBuildChanges ToolTip</system:String>

        public void GetBuildChanges()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetBuildChangesEvent>().Publish(
                new GetBuildChangesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Build = _contextMainViewModel.Context.SelectedBuild
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetBuildChangesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedBuild is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetBuildChanges Command

        #region GetBuildLogs Command

        public DelegateCommand GetBuildLogsCommand { get; set; }
        public string GetBuildLogsContent { get; set; } = "GetBuildLogs";
        public string GetBuildLogsToolTip { get; set; } = "GetBuildLogs ToolTip";

        // Can get fancy and use Resources
        //public string GetBuildLogsContent { get; set; } = "ViewName_GetBuildLogsContent";
        //public string GetBuildLogsToolTip { get; set; } = "ViewName_GetBuildLogsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetBuildLogsContent">GetBuildLogs</system:String>
        //    <system:String x:Key="ViewName_GetBuildLogsContentToolTip">GetBuildLogs ToolTip</system:String>

        public void GetBuildLogs()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetBuildLogsEvent>().Publish(
                new GetBuildLogsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Build = _contextMainViewModel.Context.SelectedBuild
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetBuildLogsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedBuild is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetBuildLogs Command

        #region GetBuildWorkItemRefs Command

        public DelegateCommand GetBuildWorkItemRefsCommand { get; set; }
        public string GetBuildWorkItemRefsContent { get; set; } = "GetBuildWorkItemRefs";
        public string GetBuildWorkItemRefsToolTip { get; set; } = "GetBuildWorkItemRefs ToolTip";

        // Can get fancy and use Resources
        //public string GetBuildWorkItemRefsContent { get; set; } = "ViewName_GetBuildWorkItemRefsContent";
        //public string GetBuildWorkItemRefsToolTip { get; set; } = "ViewName_GetBuildWorkItemRefsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetBuildWorkItemRefsContent">GetBuildWorkItemRefs</system:String>
        //    <system:String x:Key="ViewName_GetBuildWorkItemRefsContentToolTip">GetBuildWorkItemRefs ToolTip</system:String>

        public void GetBuildWorkItemRefs()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetBuildWorkItemRefsEvent>().Publish(
                new GetBuildWorkItemRefsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Build = _contextMainViewModel.Context.SelectedBuild
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetBuildWorkItemRefsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedBuild is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetBuildWorkItemRefs Command

        #region GetControllers Command

        public DelegateCommand GetControllersCommand { get; set; }
        public string GetControllersContent { get; set; } = "GetControllers";
        public string GetControllersToolTip { get; set; } = "GetControllers ToolTip";

        // Can get fancy and use Resources
        //public string GetControllersContent { get; set; } = "ViewName_GetControllersContent";
        //public string GetControllersToolTip { get; set; } = "ViewName_GetControllersContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetControllersContent">GetControllers</system:String>
        //    <system:String x:Key="ViewName_GetControllersContentToolTip">GetControllers ToolTip</system:String>

        public void GetControllers()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetControllersEvent>().Publish(
                new GetControllersEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetControllersCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetControllers Command

        #region GetDefinitions Command

        public DelegateCommand GetDefinitionsCommand { get; set; }
        public string GetDefinitionsContent { get; set; } = "GetDefinitions";
        public string GetDefinitionsToolTip { get; set; } = "GetDefinitions ToolTip";

        // Can get fancy and use Resources
        //public string GetDefinitionsContent { get; set; } = "ViewName_GetDefinitionsContent";
        //public string GetDefinitionsToolTip { get; set; } = "ViewName_GetDefinitionsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetDefinitionsContent">GetDefinitions</system:String>
        //    <system:String x:Key="ViewName_GetDefinitionsContentToolTip">GetDefinitions ToolTip</system:String>

        public void GetDefinitions()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetDefinitionsEvent>().Publish(
                new GetDefinitionsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetDefinitionsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetDefinitions Command

        #region GetGeneralSettings Command

        public DelegateCommand GetGeneralSettingsCommand { get; set; }
        public string GetGeneralSettingsContent { get; set; } = "GetGeneralSettings";
        public string GetGeneralSettingsToolTip { get; set; } = "GetGeneralSettings ToolTip";

        // Can get fancy and use Resources
        //public string GetGeneralSettingsContent { get; set; } = "ViewName_GetGeneralSettingsContent";
        //public string GetGeneralSettingsToolTip { get; set; } = "ViewName_GetGeneralSettingsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetGeneralSettingsContent">GetGeneralSettings</system:String>
        //    <system:String x:Key="ViewName_GetGeneralSettingsContentToolTip">GetGeneralSettings ToolTip</system:String>

        public void GetGeneralSettings()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);
            // TODO(crhodes)
            // Do something amazing.
            // Message = "Cool, you called GetGeneralSettings";

            // Uncomment this if you are telling someone else to handle this

            // Common.EventAggregator.GetEvent<GetGeneralSettingsEvent>().Publish();

            // May want EventArgs

            EventAggregator.GetEvent<GetGeneralSettingsEvent>().Publish(
                new GetGeneralSettingsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetGeneralSettingsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetGeneralSettings Command

        #region GetOptions Command

        public DelegateCommand GetOptionsCommand { get; set; }
        public string GetOptionsContent { get; set; } = "GetOptions";
        public string GetOptionsToolTip { get; set; } = "GetOptions ToolTip";

        // Can get fancy and use Resources
        //public string GetOptionsContent { get; set; } = "ViewName_GetOptionsContent";
        //public string GetOptionsToolTip { get; set; } = "ViewName_GetOptionsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetOptionsContent">GetOptions</system:String>
        //    <system:String x:Key="ViewName_GetOptionsContentToolTip">GetOptions ToolTip</system:String>

        public void GetOptions()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetOptionsEvent>().Publish(
                new GetOptionsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetOptionsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetOptions Command

        #region GetResources Command

        public DelegateCommand GetResourcesCommand { get; set; }
        public string GetResourcesContent { get; set; } = "GetResources";
        public string GetResourcesToolTip { get; set; } = "GetResources ToolTip";

        // Can get fancy and use Resources
        //public string GetResourcesContent { get; set; } = "ViewName_GetResourcesContent";
        //public string GetResourcesToolTip { get; set; } = "ViewName_GetResourcesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetResourcesContent">GetResources</system:String>
        //    <system:String x:Key="ViewName_GetResourcesContentToolTip">GetResources ToolTip</system:String>

        public void GetResources()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetResourcesEvent>().Publish(
                new GetResourcesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Definition = _contextMainViewModel.Context.SelectedDefinition
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetResourcesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedDefinition is null)
            {
                return false;
            }

            return true;
        }

        private void CallGetResources(Domain.Build.Definition definition)
        {
            if (!(definition is null))
            {
                EventAggregator.GetEvent<GetResourcesEvent>().Publish(
                    new GetResourcesEventArgs()
                    {
                        Organization = _collectionMainViewModel.SelectedCollection.Organization,
                        Project = _contextMainViewModel.Context.SelectedProject,
                        Definition = definition
                    });
            }
        }

        #endregion GetResources Command

        #region GetSettings Command

        public DelegateCommand GetSettingsCommand { get; set; }
        public string GetSettingsContent { get; set; } = "GetSettings";
        public string GetSettingsToolTip { get; set; } = "GetSettings ToolTip";

        // Can get fancy and use Resources
        //public string GetSettingsContent { get; set; } = "ViewName_GetSettingsContent";
        //public string GetSettingsToolTip { get; set; } = "ViewName_GetSettingsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetSettingsContent">GetSettings</system:String>
        //    <system:String x:Key="ViewName_GetSettingsContentToolTip">GetSettings ToolTip</system:String>

        public void GetSettings()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetSettingsEvent>().Publish(
                new GetSettingsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetSettingsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetSettings Command

        #region GetBuildTags Command

        public DelegateCommand GetBuildTagsCommand { get; set; }
        public string GetBuildTagsContent { get; set; } = "GetTags";
        public string GetBuildTagsToolTip { get; set; } = "GetTags ToolTip";

        // Can get fancy and use Resources
        //public string GetTagsContent { get; set; } = "ViewName_GetTagsContent";
        //public string GetTagsToolTip { get; set; } = "ViewName_GetTagsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTagsContent">GetTags</system:String>
        //    <system:String x:Key="ViewName_GetTagsContentToolTip">GetTags ToolTip</system:String>

        public void GetBuildTags()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetBuildTagsEvent>().Publish(
                new GetBuildTagsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetBuildTagsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetBuildTags Command

        #endregion Build Area
    }
}