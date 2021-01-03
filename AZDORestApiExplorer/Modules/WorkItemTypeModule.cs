using System;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.DomainServices;
using AZDORestApiExplorer.Presentation.ViewModels;
using AZDORestApiExplorer.Presentation.Views;
using AZDORestApiExplorer.WorkItemTrackingProcess.Presentation.ViewModels;
using AZDORestApiExplorer.WorkItemTrackingProcess.Presentation.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNC;

namespace AZDORestApiExplorer
{
    public class WorkItemTypeModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public WorkItemTypeModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            containerRegistry.Register<IWorkItemTypeMainViewModel, WorkItemTypeMainViewModel>();
            containerRegistry.RegisterSingleton<IWorkItemTypeMain, WorkItemTypeMain>();

            //containerRegistry.Register<IWorkItemTypeNavigationViewModel, WorkItemTypeNavigationViewModel>();
            //containerRegistry.RegisterSingleton<IWorkItemTypeNavigation, WorkItemTypeNavigation>();

            //containerRegistry.Register<IWorkItemTypeDetailViewModel, WorkItemTypeDetailViewModel>();
            //containerRegistry.RegisterSingleton<IWorkItemTypeDetail, WorkItemTypeDetail>();

            //containerRegistry.RegisterSingleton<IWorkItemTypeLookupDataService, WorkItemTypeLookupDataService>();
            //containerRegistry.Register<IWorkItemTypeDataService, WorkItemTypeDataService>();

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            //this loads WorkItemTypeMain into the Shell loaded in CreateShell() in App.Xaml.cs
            _regionManager.RegisterViewWithRegion(RegionNames.WorkItemTypeMainRegion, typeof(IWorkItemTypeMain));

            // These load into WorkItemTypeMain.xaml
            //_regionManager.RegisterViewWithRegion(RegionNames.WorkItemTypeNavigationRegion, typeof(IWorkItemTypeNavigation));
            //_regionManager.RegisterViewWithRegion(RegionNames.WorkItemTypeDetailRegion, typeof(IWorkItemTypeDetail));

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

    }
}