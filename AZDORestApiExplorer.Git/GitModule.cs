using System;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.Git.Presentation.ViewModels;
using AZDORestApiExplorer.Git.Presentation.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using VNC;

namespace AZDORestApiExplorer.Git
{
    public class GitModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public GitModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            containerRegistry.Register<IRepositoryMainViewModel, RepositoryMainViewModel>();
            containerRegistry.RegisterSingleton<IRepositoryMain, RepositoryMain>();

            // containerRegistry.RegisterSingleton<IRepositoryLookupDataService, RepositoryLookupDataService>();
            // containerRegistry.Register<IRepositoryDataService, RepositoryDataService>();

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            //this loads RepositoryMain into the Shell loaded in CreateShell() in App.Xaml.cs
            _regionManager.RegisterViewWithRegion(RegionNames.RepositoryMainRegion, typeof(IRepositoryMain));

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }
    }
}