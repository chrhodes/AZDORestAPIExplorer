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

            containerRegistry.Register<ArtifactLinkTypeMainViewModel>();
            containerRegistry.RegisterSingleton<ArtifactLinkTypeMain>();

            containerRegistry.Register<ClassificationNodeMainViewModel>();
            containerRegistry.RegisterSingleton<ClassificationNodeMain>();

            containerRegistry.Register<FieldMainViewModel>();
            containerRegistry.RegisterSingleton<FieldMain>();

            containerRegistry.Register<QueryMainViewModel>();
            containerRegistry.RegisterSingleton<QueryMain>();

            containerRegistry.Register<TagMainViewModel>();
            containerRegistry.RegisterSingleton<TagMain>();

            containerRegistry.Register<TemplateMainViewModel>();
            containerRegistry.RegisterSingleton<TemplateMain>();

            containerRegistry.Register<WorkItemIconMainViewModel>();
            containerRegistry.RegisterSingleton<WorkItemIconMain>();

            containerRegistry.Register<WorkItemRelationTypeMainViewModel>();
            containerRegistry.RegisterSingleton<WorkItemRelationTypeMain>();

            containerRegistry.Register<WorkItemTypeMainViewModel>();
            containerRegistry.RegisterSingleton<WorkItemTypeMain>();

            containerRegistry.Register<WorkItemTypeCategoryMainViewModel>();
            containerRegistry.RegisterSingleton<WorkItemTypeCategoryMain>();

            containerRegistry.Register<StateMainViewModel>();
            containerRegistry.RegisterSingleton<StateMain>();

            containerRegistry.Register<WorkItemTypesFieldMainViewModel>();
            containerRegistry.RegisterSingleton<WorkItemTypesFieldMain>();

            containerRegistry.Register<CreateWorkItemMainViewModel>();
            containerRegistry.RegisterSingleton<CreateWorkItemMain>();

            containerRegistry.Register<WorkItemMainViewModel>();
            containerRegistry.RegisterSingleton<WorkItemMain>();

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
            _regionManager.RegisterViewWithRegion(RegionNames.ArtifactLinkTypeMainRegion, typeof(ArtifactLinkTypeMain));
            _regionManager.RegisterViewWithRegion(RegionNames.ClassificationNodeMainRegion, typeof(ClassificationNodeMain));
            _regionManager.RegisterViewWithRegion(RegionNames.FieldWITMainRegion, typeof(FieldMain));
            _regionManager.RegisterViewWithRegion(RegionNames.QueryMainRegion, typeof(QueryMain));
            _regionManager.RegisterViewWithRegion(RegionNames.TagMainRegion, typeof(TagMain));
            _regionManager.RegisterViewWithRegion(RegionNames.TemplateMainRegion, typeof(TemplateMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemIconMainRegion, typeof(WorkItemIconMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemRelationTypeMainRegion, typeof(WorkItemRelationTypeMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemTypeWITMainRegion, typeof(WorkItemTypeMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemTypeCategoryMainRegion, typeof(WorkItemTypeCategoryMain));
            _regionManager.RegisterViewWithRegion(RegionNames.StateWITMainRegion, typeof(StateMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemTypesFieldMainRegion, typeof(WorkItemTypesFieldMain));

            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemMainRegion, typeof(WorkItemMain));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}