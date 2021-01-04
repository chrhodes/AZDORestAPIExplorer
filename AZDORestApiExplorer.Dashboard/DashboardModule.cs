using System;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.Dashboard.Presentation.ViewModels;
using AZDORestApiExplorer.Dashboard.Presentation.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using VNC;

namespace AZDORestApiExplorer.Dashboard
{
    public class DashboardModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public DashboardModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            containerRegistry.Register<IDashboardMainViewModel, DashboardMainViewModel>();
            containerRegistry.RegisterSingleton<IDashboardMain, DashboardMain>();


            containerRegistry.Register<IWidgetMainViewModel, WidgetMainViewModel>();
            containerRegistry.RegisterSingleton<IWidgetMain, WidgetMain>();

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
            _regionManager.RegisterViewWithRegion(RegionNames.DashboardMainRegion, typeof(IDashboardMain));
            _regionManager.RegisterViewWithRegion(RegionNames.WidgetMainRegion, typeof(IWidgetMain));

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }
    }
}