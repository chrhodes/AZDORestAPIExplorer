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
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            containerRegistry.Register<IBehaviorMainViewModel, BehaviorMainViewModel>();
            containerRegistry.RegisterSingleton<IBehaviorMain, BehaviorMain>();

            containerRegistry.Register<IFieldMainViewModel, FieldMainViewModel>();
            containerRegistry.RegisterSingleton<IFieldMain, FieldMain>();

            containerRegistry.Register<IListMainViewModel, ListMainViewModel>();
            containerRegistry.RegisterSingleton<IListMain, ListMain>();

            containerRegistry.Register<IProcessMainViewModel, ProcessMainViewModel>();
            containerRegistry.RegisterSingleton<IProcessMain, ProcessMain>();

            containerRegistry.Register<IRuleMainViewModel, RuleMainViewModel>();
            containerRegistry.RegisterSingleton<IRuleMain, RuleMain>();

            containerRegistry.Register<IStateMainViewModel, StateMainViewModel>();
            containerRegistry.RegisterSingleton<IStateMain, StateMain>();

            containerRegistry.Register<ISystemControlMainViewModel, SystemControlMainViewModel>();
            containerRegistry.RegisterSingleton<ISystemControlMain, SystemControlMain>();

            containerRegistry.Register<IWorkItemTypeMainViewModel, WorkItemTypeMainViewModel>();
            containerRegistry.RegisterSingleton<IWorkItemTypeMain, WorkItemTypeMain>();

            containerRegistry.Register<IWorkItemTypesBehaviorMainViewModel, WorkItemTypesBehaviorMainViewModel>();
            containerRegistry.RegisterSingleton<IWorkItemTypesBehaviorMain, WorkItemTypesBehaviorMain>();

            // containerRegistry.RegisterSingleton<IFieldLookupDataService, FieldLookupDataService>();
            // containerRegistry.Register<IFieldDataService, FieldDataService>();

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            //this loads AccountMain into the Shell loaded in CreateShell() in App.Xaml.cs

            _regionManager.RegisterViewWithRegion(RegionNames.BehaviorMainRegion, typeof(IBehaviorMain));
            _regionManager.RegisterViewWithRegion(RegionNames.FieldWITPMainRegion, typeof(IFieldMain));
            _regionManager.RegisterViewWithRegion(RegionNames.ListMainRegion, typeof(IListMain));
            _regionManager.RegisterViewWithRegion(RegionNames.ProcessWITPMainRegion, typeof(IProcessMain));
            _regionManager.RegisterViewWithRegion(RegionNames.RuleMainRegion, typeof(IRuleMain));
            _regionManager.RegisterViewWithRegion(RegionNames.StateWITPMainRegion, typeof(IStateMain));
            _regionManager.RegisterViewWithRegion(RegionNames.SystemControlMainRegion, typeof(ISystemControlMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemTypeWITPMainRegion, typeof(IWorkItemTypeMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemTypesBehaviorMainRegion, typeof(IWorkItemTypesBehaviorMain));

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }
    }
}