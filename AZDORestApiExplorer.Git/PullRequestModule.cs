using System;

using AZDORestApiExplorer.Core;

using AZDORestApiExplorer.Git.Presentation.ViewModels;
using AZDORestApiExplorer.Git.Presentation.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNC;

namespace AZDORestApiExplorer.Git
{
    public class PullRequestModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public PullRequestModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            containerRegistry.Register<IPullRequestMainViewModel, PullRequestMainViewModel>();
            containerRegistry.RegisterSingleton<IPullRequestMain, PullRequestMain>();

            // containerRegistry.RegisterSingleton<IPullRequestLookupDataService, PullRequestLookupDataService>();
            // containerRegistry.Register<IPullRequestDataService, PullRequestDataService>();

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            //this loads PullRequestMain into the Shell loaded in CreateShell() in App.Xaml.cs
            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestMainRegion, typeof(IPullRequestMain));


            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

        // TODO(crhodes)
        // Place these in Core\RegionNames.cs


        // public static string PullRequestNavigationRegion = "PullRequestNavigationRegion";
        // public static string PullRequestDetailRegion = "PullRequestDetailRegion";

        // TODO(crhodes)
        // Add this to App.xaml.cs - ConfigureModuleCatalog()


    }
}
