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

            containerRegistry.Register<IProcessMainViewModel, ProcessMainViewModel>();
            containerRegistry.RegisterSingleton<IProcessMain, ProcessMain>();

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
            _regionManager.RegisterViewWithRegion(RegionNames.WITPProcessMainRegion, typeof(IProcessMain));

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }
    }
}