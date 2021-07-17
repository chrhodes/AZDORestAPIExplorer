using System;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.WorkItemTracking.Presentation.ViewModels;
using AZDORestApiExplorer.WorkItemTracking.Presentation.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using VNC;

namespace AZDORestApiExplorer.WorkItemTracking
{
    public class WorkItemTrackingModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public WorkItemTrackingModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            containerRegistry.Register<IArtifactLinkTypeMainViewModel, ArtifactLinkTypeMainViewModel>();
            containerRegistry.RegisterSingleton<IArtifactLinkTypeMain, ArtifactLinkTypeMain>();

            containerRegistry.Register<IClassificationNodeMainViewModel, ClassificationNodeMainViewModel>();
            containerRegistry.RegisterSingleton<IClassificationNodeMain, ClassificationNodeMain>();

            containerRegistry.Register<IFieldMainViewModel, FieldMainViewModel>();
            containerRegistry.RegisterSingleton<IFieldMain, FieldMain>();

            containerRegistry.Register<IQueryMainViewModel, QueryMainViewModel>();
            containerRegistry.RegisterSingleton<IQueryMain, QueryMain>();

            containerRegistry.Register<ITagMainViewModel, TagMainViewModel>();
            containerRegistry.RegisterSingleton<ITagMain, TagMain>();

            containerRegistry.Register<ITemplateMainViewModel, TemplateMainViewModel>();
            containerRegistry.RegisterSingleton<ITemplateMain, TemplateMain>();

            containerRegistry.Register<IWorkItemIconMainViewModel, WorkItemIconMainViewModel>();
            containerRegistry.RegisterSingleton<IWorkItemIconMain, WorkItemIconMain>();

            containerRegistry.Register<IWorkItemRelationTypeMainViewModel, WorkItemRelationTypeMainViewModel>();
            containerRegistry.RegisterSingleton<IWorkItemRelationTypeMain, WorkItemRelationTypeMain>();

            containerRegistry.Register<IWorkItemTypeMainViewModel, WorkItemTypeMainViewModel>();
            containerRegistry.RegisterSingleton<IWorkItemTypeMain, WorkItemTypeMain>();

            containerRegistry.Register<IWorkItemTypeCategoryMainViewModel, WorkItemTypeCategoryMainViewModel>();
            containerRegistry.RegisterSingleton<IWorkItemTypeCategoryMain, WorkItemTypeCategoryMain>();

            containerRegistry.Register<IStateMainViewModel, StateMainViewModel>();
            containerRegistry.RegisterSingleton<IStateMain, StateMain>();

            containerRegistry.Register<IWorkItemTypesFieldMainViewModel, WorkItemTypesFieldMainViewModel>();
            containerRegistry.RegisterSingleton<IWorkItemTypesFieldMain, WorkItemTypesFieldMain>();

            containerRegistry.Register<ICreateWorkItemMainViewModel, CreateWorkItemMainViewModel>();
            containerRegistry.RegisterSingleton<ICreateWorkItemMain, CreateWorkItemMain>();

            containerRegistry.Register<IWorkItemMainViewModel, WorkItemMainViewModel>();
            containerRegistry.RegisterSingleton<IWorkItemMain, WorkItemMain>();

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            
            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            //this loads AccountMain into the Shell loaded in CreateShell() in App.Xaml.cs
            _regionManager.RegisterViewWithRegion(RegionNames.ArtifactLinkTypeMainRegion, typeof(IArtifactLinkTypeMain));
            _regionManager.RegisterViewWithRegion(RegionNames.ClassificationNodeMainRegion, typeof(IClassificationNodeMain));
            _regionManager.RegisterViewWithRegion(RegionNames.FieldWITMainRegion, typeof(IFieldMain));
            _regionManager.RegisterViewWithRegion(RegionNames.QueryMainRegion, typeof(IQueryMain));
            _regionManager.RegisterViewWithRegion(RegionNames.TagMainRegion, typeof(ITagMain));
            _regionManager.RegisterViewWithRegion(RegionNames.TemplateMainRegion, typeof(ITemplateMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemIconMainRegion, typeof(IWorkItemIconMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemRelationTypeMainRegion, typeof(IWorkItemRelationTypeMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemTypeWITMainRegion, typeof(IWorkItemTypeMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemTypeCategoryMainRegion, typeof(IWorkItemTypeCategoryMain));
            _regionManager.RegisterViewWithRegion(RegionNames.StateWITMainRegion, typeof(IStateMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemTypesFieldMainRegion, typeof(IWorkItemTypesFieldMain));

            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemMainRegion, typeof(IWorkItemMain));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}