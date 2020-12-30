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
    public class HTTPExchangeModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public HTTPExchangeModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            containerRegistry.Register<IHTTPExchangeMainViewModel, HTTPExchangeMainViewModel>();
            containerRegistry.RegisterSingleton<IHTTPExchangeMain, HTTPExchangeMain>();

            // containerRegistry.RegisterSingleton<IHTTPExchangeLookupDataService, HTTPExchangeLookupDataService>();
            // containerRegistry.Register<IHTTPExchangeDataService, HTTPExchangeDataService>();

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            //this loads HTTPExchangeMain into the Shell loaded in CreateShell() in App.Xaml.cs
            _regionManager.RegisterViewWithRegion(RegionNames.HTTPExchangeMainRegion, typeof(IHTTPExchangeMain));


            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

        // TODO(crhodes)
        // Place these in Core\RegionNames.cs

        public static string HTTPExchangeMainRegion = "HTTPExchangeMainRegion";
        // public static string HTTPExchangeNavigationRegion = "HTTPExchangeNavigationRegion";
        // public static string HTTPExchangeDetailRegion = "HTTPExchangeDetailRegion";

        // TODO(crhodes)
        // Add this to App.xaml.cs - ConfigureModuleCatalog()

        moduleCatalog.AddModule(typeof(HTTPExchangeModule));
    }
}