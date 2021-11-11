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

        #region GetArtifactLinkTypes Command

        public DelegateCommand GetArtifactLinkTypesCommand { get; set; }
        public string GetArtifactLinkTypesContent { get; set; } = "GetArtifactLinkTypes";
        public string GetArtifactLinkTypesToolTip { get; set; } = "GetArtifactLinkTypes ToolTip";

        // Can get fancy and use Resources
        //public string GetArtifactLinkTypesContent { get; set; } = "ViewName_GetArtifactLinkTypesContent";
        //public string GetArtifactLinkTypesToolTip { get; set; } = "ViewName_GetArtifactLinkTypesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetArtifactLinkTypesContent">GetArtifactLinkTypes</system:String>
        //    <system:String x:Key="ViewName_GetArtifactLinkTypesContentToolTip">GetArtifactLinkTypes ToolTip</system:String>



        public void GetArtifactLinkTypesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetArtifactLinkTypesEvent>().Publish(
                new GetArtifactLinkTypesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetArtifactLinkTypesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetArtifactLinkTypes Command

        #region GetClassificationNodes Command

        public DelegateCommand GetClassificationNodesCommand { get; set; }
        public string GetClassificationNodesContent { get; set; } = "Get Classification Nodes";
        public string GetClassificationNodesToolTip { get; set; } = "Get Classification Nodes ToolTip";

        // Can get fancy and use Resources
        //public string GetClassificationNodesContent { get; set; } = "ViewName_GetClassificationNodesContent";
        //public string GetClassificationNodesToolTip { get; set; } = "ViewName_GetClassificationNodesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetClassificationNodesContent">GetClassificationNodes</system:String>
        //    <system:String x:Key="ViewName_GetClassificationNodesContentToolTip">GetClassificationNodes ToolTip</system:String>



        public void GetClassificationNodesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetClassificationNodesEvent>().Publish(
                new GetClassificationNodesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    IDs = _contextMainViewModel.Context.ClassificationNodeIds,
                    Depth = _contextMainViewModel.Context.ClassificationNodeDepth
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetClassificationNodesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetClassificationNodes Command

        #region GetFields Command

        public DelegateCommand GetOrganizationFieldsWITCommand { get; set; }
        public string GetOrganizationFieldsWITContent { get; set; } = "GetFields (Organization)";
        public string GetOrganizationFieldsWITToolTip { get; set; } = "GetFields (Organization) ToolTip";

        // Can get fancy and use Resources
        //public string GetOrganizationFieldsContent { get; set; } = "ViewName_GetFieldsContent";
        //public string GetOrganizationFieldsToolTip { get; set; } = "ViewName_GetFieldsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetFieldsContent">GetFields</system:String>
        //    <system:String x:Key="ViewName_GetFieldsContentToolTip">GetFields ToolTip</system:String>

        public DelegateCommand GetProjectFieldsWITCommand { get; set; }

        public string GetProjectFieldsWITContent { get; set; } = "GetFields (Project)";

        public string GetProjectFieldsWITToolTip { get; set; } = "GetFields (Project) ToolTip";



        public void GetOrganizationFieldsWITExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetFieldsWITEvent>().Publish(
                new GetFieldsWITEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetOrganizationFieldsWITCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        // Can get fancy and use Resources
        //public string GetFieldsContent { get; set; } = "ViewName_GetFieldsContent";
        //public string GetFieldsToolTip { get; set; } = "ViewName_GetFieldsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetFieldsContent">GetFields</system:String>
        //    <system:String x:Key="ViewName_GetFieldsContentToolTip">GetFields ToolTip</system:String>



        public void GetProjectFieldsWITExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetFieldsWITEvent>().Publish(
                new GetFieldsWITEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetProjectFieldsWITCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetFields Command

        #region GetQueries Command

        public DelegateCommand GetQueriesCommand { get; set; }
        public string GetQueriesContent { get; set; } = "GetQueries";
        public string GetQueriesToolTip { get; set; } = "GetQueries ToolTip";

        // Can get fancy and use Resources
        //public string GetQueriesContent { get; set; } = "ViewName_GetQueriesContent";
        //public string GetQueriesToolTip { get; set; } = "ViewName_GetQueriesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetQueriesContent">GetQueries</system:String>
        //    <system:String x:Key="ViewName_GetQueriesContentToolTip">GetQueries ToolTip</system:String>



        public void GetQueriesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetQueriesEvent>().Publish(
                new GetQueriesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetQueriesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetQueries Command

        #region GetTags Command

        public DelegateCommand GetTagsCommand { get; set; }
        public string GetTagsContent { get; set; } = "GetTags";
        public string GetTagsToolTip { get; set; } = "GetTags ToolTip";

        // Can get fancy and use Resources
        //public string GetTagsContent { get; set; } = "ViewName_GetTagsContent";
        //public string GetTagsToolTip { get; set; } = "ViewName_GetTagsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTagsContent">GetTags</system:String>
        //    <system:String x:Key="ViewName_GetTagsContentToolTip">GetTags ToolTip</system:String>



        public void GetTagsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetTagsEvent>().Publish(
                new GetTagsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetTagsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetTags Command

        #region GetTemplates Command

        public DelegateCommand GetTemplatesCommand { get; set; }
        public string GetTemplatesContent { get; set; } = "GetTemplates";
        public string GetTemplatesToolTip { get; set; } = "GetTemplates ToolTip";

        // Can get fancy and use Resources
        //public string GetTemplatesContent { get; set; } = "ViewName_GetTemplatesContent";
        //public string GetTemplatesToolTip { get; set; } = "ViewName_GetTemplatesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetTemplatesContent">GetTemplates</system:String>
        //    <system:String x:Key="ViewName_GetTemplatesContentToolTip">GetTemplates ToolTip</system:String>



        public void GetTemplatesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetTemplatesEvent>().Publish(
                new GetTemplatesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    Team = _contextMainViewModel.Context.SelectedTeam
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetTemplatesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedTeam is null)
            {
                return false;
            }
            return true;
        }

        #endregion GetTemplates Command

        #region GetWorkItemIcons Command

        public DelegateCommand GetWorkItemIconsCommand { get; set; }
        public string GetWorkItemIconsContent { get; set; } = "GetWorkItemIcons";
        public string GetWorkItemIconsToolTip { get; set; } = "GetWorkItemIcons ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkItemIconsContent { get; set; } = "ViewName_GetWorkItemIconsContent";
        //public string GetWorkItemIconsToolTip { get; set; } = "ViewName_GetWorkItemIconsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkItemIconsContent">GetWorkItemIcons</system:String>
        //    <system:String x:Key="ViewName_GetWorkItemIconsContentToolTip">GetWorkItemIcons ToolTip</system:String>



        public void GetWorkItemIconsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetWorkItemIconsEvent>().Publish(
                new GetWorkItemIconsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetWorkItemIconsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }
        #endregion GetWorkItemIcons Command

        #region GetWorkRelationTypes Command

        public DelegateCommand GetWorkItemRelationTypesCommand { get; set; }
        public string GetWorkItemRelationTypesContent { get; set; } = "GetWorItemRelationTypes";
        public string GetWorkItemRelationTypesToolTip { get; set; } = "GetWorItemRelationTypes ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkRelationTypesContent { get; set; } = "ViewName_GetWorkRelationTypesContent";
        //public string GetWorkRelationTypesToolTip { get; set; } = "ViewName_GetWorkRelationTypesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkRelationTypesContent">GetWorkRelationTypes</system:String>
        //    <system:String x:Key="ViewName_GetWorkRelationTypesContentToolTip">GetWorkRelationTypes ToolTip</system:String>


        public void GetWorkItemRelationTypesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetWorkItemRelationTypesEvent>().Publish(
                new GetWorkItemRelationTypesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetWorkItemRelationTypesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }


        #endregion GetWorkRelationTypes Command

        #region GetWorkItemTypeCategories Command

        public DelegateCommand GetWorkItemTypeCategoriesCommand { get; set; }
        public string GetWorkItemTypeCategoriesContent { get; set; } = "GetWorkItemTypeCategories";
        public string GetWorkItemTypeCategoriesToolTip { get; set; } = "GetWorkItemTypeCategories ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkItemTypeCategoriesContent { get; set; } = "ViewName_GetWorkItemTypeCategoriesContent";
        //public string GetWorkItemTypeCategoriesToolTip { get; set; } = "ViewName_GetWorkItemTypeCategoriesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkItemTypeCategoriesContent">GetWorkItemTypeCategories</system:String>
        //    <system:String x:Key="ViewName_GetWorkItemTypeCategoriesContentToolTip">GetWorkItemTypeCategories ToolTip</system:String>



        public void GetWorkItemTypeCategoriesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetWorkItemTypeCategoriesEvent>().Publish(
                new GetWorkItemTypeCategoriesEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetWorkItemTypeCategoriesCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetWorkItemTypeCategories Command

        #region GetStates Command

        public DelegateCommand GetStatesWITCommand { get; set; }
        public string GetStatesWITContent { get; set; } = "GetStates";
        public string GetStatesWITToolTip { get; set; } = "GetStates ToolTip";

        // Can get fancy and use Resources
        //public string GetStatesWITPContent { get; set; } = "ViewName_GetStatesWITContent";
        //public string GetStatesWITPToolTip { get; set; } = "ViewName_GetStatesWITContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetStatesContent">GetStates</system:String>
        //    <system:String x:Key="ViewName_GetStatesContentToolTip">GetStates ToolTip</system:String>


        public void GetStatesWITExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetStatesWITEvent>().Publish(
                new GetStatesWITEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWIT
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetStatesWITCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWIT is null)
            {
                return false;
            }

            return true;
        }


        #endregion GetStates Command

        #region GetWorkItemTypesWIT Command

        public DelegateCommand GetWorkItemTypesWITCommand { get; set; }
        public string GetWorkItemTypesWITContent { get; set; } = "Get WorkItemTypes";
        public string GetWorkItemTypesWITToolTip { get; set; } = "Get WorkItemTypes ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkItemTypesWITContent { get; set; } = "ViewName_GetWorkItemTypesWITContent";
        //public string GetWorkItemTypesWITToolTip { get; set; } = "ViewName_GetWorkItemTypesWITContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkItemTypesWITContent">GetWorkItemTypesWIT</system:String>
        //    <system:String x:Key="ViewName_GetWorkItemTypesWITContentToolTip">GetWorkItemTypesWIT ToolTip</system:String>


        public void GetWorkItemTypesWITExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetWorkItemTypesWITEvent>().Publish(
                new GetWorkItemTypesWITEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetWorkItemTypesWITCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }


        #endregion GetWorkItemTypesWIT Command

        #region GetWorkItemTypesFields Command

        public DelegateCommand GetWorkItemTypesFieldsCommand { get; set; }
        public string GetWorkItemTypesFieldsContent { get; set; } = "Get WorkItemTypesFields";
        public string GetWorkItemTypesFieldsToolTip { get; set; } = "Get WorkItemTypesFields ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkItemTypesFieldsContent { get; set; } = "ViewName_GetWorkItemTypesFieldsContent";
        //public string GetWorkItemTypesFieldsToolTip { get; set; } = "ViewName_GetWorkItemTypesFieldsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkItemTypesFieldsContent">GetWorkItemTypesFields</system:String>
        //    <system:String x:Key="ViewName_GetWorkItemTypesFieldsContentToolTip">GetWorkItemTypesFields ToolTip</system:String>



        public void GetWorkItemTypesFieldsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetWorkItemTypesFieldsEvent>().Publish(
                new GetWorkItemTypesFieldsEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization
                    ,
                    Project = _contextMainViewModel.Context.SelectedProject
                    ,
                    WorkItemType = _contextMainViewModel.Context.SelectedWorkItemTypeWIT
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetWorkItemTypesFieldsCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null
                || _contextMainViewModel.Context.SelectedWorkItemTypeWIT is null)
            {
                return false;
            }

            return true;
        }

        #endregion GetWorkItemTypesFields Command

        #region GetWorkItem Command

        public DelegateCommand GetWorkItemCommand { get; set; }
        public string GetWorkItemContent { get; set; } = "GetWorkItem";
        public string GetWorkItemToolTip { get; set; } = "GetWorkItem ToolTip";

        // Can get fancy and use Resources
        //public string GetWorkItemContent { get; set; } = "ViewName_GetWorkItemContent";
        //public string GetWorkItemToolTip { get; set; } = "ViewName_GetWorkItemContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetWorkItemContent">GetWorkItem</system:String>
        //    <system:String x:Key="ViewName_GetWorkItemContentToolTip">GetWorkItem ToolTip</system:String>

        public void GetWorkItem()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<Domain.WorkItemTracking.Events.GetWorkItemEvent>().Publish(
                new Domain.WorkItemTracking.Events.GetWorkItemEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Id = _contextMainViewModel.Context.Model.WorkItemId
                });

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public bool GetWorkItemCanExecute()
        {
            if (_collectionMainViewModel.SelectedCollection is null
                || _contextMainViewModel.Context.SelectedProject is null)
            {
                return false;
            }

            return true;
        }

        private void CallGetWorkItem(WorkItem workItem)
        {
            EventAggregator.GetEvent<Domain.WorkItemTracking.Events.GetWorkItemEvent>().Publish(
                new Domain.WorkItemTracking.Events.GetWorkItemEventArgs()
                {
                    Organization = _collectionMainViewModel.SelectedCollection.Organization,
                    Project = _contextMainViewModel.Context.SelectedProject,
                    Id = _contextMainViewModel.Context.Model.WorkItemId
                });
        }

        private void CallGetWorkItem(BuildWorkItemRef workItemRef)
        {
            if (!(workItemRef is null))
            {
                int workItemId = 0;

                if (int.TryParse(workItemRef.id, out workItemId))
                {
                    EventAggregator.GetEvent<Domain.WorkItemTracking.Events.GetWorkItemEvent>().Publish(
                        new Domain.WorkItemTracking.Events.GetWorkItemEventArgs()
                        {
                            Organization = _collectionMainViewModel.SelectedCollection.Organization,
                            Project = _contextMainViewModel.Context.SelectedProject,
                            Id = workItemId
                        });
                }
            }
        }

        private void CallGetWorkItem(PullRequestWorkItem pullRequestWorkItem)
        {
            if (!(pullRequestWorkItem is null))
            {
                int workItemId = 0;

                if (int.TryParse(pullRequestWorkItem.id, out workItemId))
                {
                    EventAggregator.GetEvent<Domain.WorkItemTracking.Events.GetWorkItemEvent>().Publish(
                        new Domain.WorkItemTracking.Events.GetWorkItemEventArgs()
                        {
                            Organization = _collectionMainViewModel.SelectedCollection.Organization,
                            Project = _contextMainViewModel.Context.SelectedProject,
                            Id = workItemId
                        });
                }
            }
        }

        #endregion GetWorkItem Command

        #endregion Work Item Tracking Area
    }
}