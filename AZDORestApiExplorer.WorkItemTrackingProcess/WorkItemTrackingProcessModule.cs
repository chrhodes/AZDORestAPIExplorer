using System;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.WorkItemTrackingProcess.Presentation.ViewModels;
using AZDORestApiExplorer.WorkItemTrackingProcess.Presentation.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using VNC;

namespace AZDORestApiExplorer.WorkItemTrackingProcess
{
    public class WorkItemTrackingProcessModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public WorkItemTrackingProcessModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            containerRegistry.Register<BehaviorMainViewModel>();
            containerRegistry.RegisterSingleton<BehaviorMain>();

            containerRegistry.Register<FieldMainViewModel>();
            containerRegistry.RegisterSingleton<FieldMain>();

            containerRegistry.Register<ListMainViewModel>();
            containerRegistry.RegisterSingleton<ListMain>();

            containerRegistry.Register<ProcessMainViewModel>();
            containerRegistry.RegisterSingleton<ProcessMain>();

            containerRegistry.Register<RuleMainViewModel>();
            containerRegistry.RegisterSingleton<RuleMain>();

            containerRegistry.Register<StateMainViewModel>();
            containerRegistry.RegisterSingleton<StateMain>();

            containerRegistry.Register<SystemControlMainViewModel>();
            containerRegistry.RegisterSingleton<SystemControlMain>();

            containerRegistry.Register<WorkItemTypeMainViewModel>();
            containerRegistry.RegisterSingleton<WorkItemTypeMain>();

            containerRegistry.Register<WorkItemTypesBehaviorMainViewModel>();
            containerRegistry.RegisterSingleton<WorkItemTypesBehaviorMain>();

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

            _regionManager.RegisterViewWithRegion(RegionNames.BehaviorMainRegion, typeof(BehaviorMain));
            _regionManager.RegisterViewWithRegion(RegionNames.FieldWITPMainRegion, typeof(FieldMain));
            _regionManager.RegisterViewWithRegion(RegionNames.ListMainRegion, typeof(ListMain));
            _regionManager.RegisterViewWithRegion(RegionNames.ProcessWITPMainRegion, typeof(ProcessMain));
            _regionManager.RegisterViewWithRegion(RegionNames.RuleMainRegion, typeof(RuleMain));
            _regionManager.RegisterViewWithRegion(RegionNames.StateWITPMainRegion, typeof(StateMain));
            _regionManager.RegisterViewWithRegion(RegionNames.SystemControlMainRegion, typeof(SystemControlMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemTypeWITPMainRegion, typeof(WorkItemTypeMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemTypesBehaviorMainRegion, typeof(WorkItemTypesBehaviorMain));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}