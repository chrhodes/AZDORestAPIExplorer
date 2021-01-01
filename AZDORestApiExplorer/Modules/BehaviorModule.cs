using System;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.DomainServices;
using AZDORestApiExplorer.Presentation.ViewModels;
using AZDORestApiExplorer.Presentation.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNC;

namespace AZDORestApiExplorer
{
    public class BehaviorModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public BehaviorModule(IRegionManager regionManager)
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

            // containerRegistry.RegisterSingleton<IBehaviorLookupDataService, BehaviorLookupDataService>();
            // containerRegistry.Register<IBehaviorDataService, BehaviorDataService>();

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            //this loads BehaviorMain into the Shell loaded in CreateShell() in App.Xaml.cs
            _regionManager.RegisterViewWithRegion(RegionNames.BehaviorMainRegion, typeof(IBehaviorMain));


            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

    }
}