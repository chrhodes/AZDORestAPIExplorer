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
    public class ContextModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public ContextModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            containerRegistry.RegisterSingleton<IContextMainViewModel, ContextMainViewModel>();
            containerRegistry.RegisterSingleton<IContextMain, ContextMain>();

            //containerRegistry.Register<IContextNavigationViewModel, ContextNavigationViewModel>();
            //containerRegistry.RegisterSingleton<IContextNavigation, ContextNavigation>();

            //containerRegistry.Register<IContextDetailViewModel, ContextDetailViewModel>();
            //containerRegistry.RegisterSingleton<IContextDetail, ContextDetail>();

            //containerRegistry.RegisterSingleton<IContextLookupDataService, ContextLookupDataService>();
            //containerRegistry.Register<IContextDataService, ContextDataService>();

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            //this loads ContextMain into the Shell loaded in CreateShell() in App.Xaml.cs
            _regionManager.RegisterViewWithRegion(RegionNames.ContextMainRegion, typeof(IContextMain));

            // These load into ContextMain.xaml
            //_regionManager.RegisterViewWithRegion(RegionNames.ContextNavigationRegion, typeof(IContextNavigation));
            //_regionManager.RegisterViewWithRegion(RegionNames.ContextDetailRegion, typeof(IContextDetail));

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

    }
}