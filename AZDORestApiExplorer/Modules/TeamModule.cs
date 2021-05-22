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
    public class TeamModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public TeamModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            containerRegistry.Register<ITeamMainViewModel, TeamMainViewModel>();
            containerRegistry.RegisterSingleton<ITeamMain, TeamMain>();

            //containerRegistry.Register<ITeamNavigationViewModel, TeamNavigationViewModel>();
            //containerRegistry.RegisterSingleton<ITeamNavigation, TeamNavigation>();

            //containerRegistry.Register<ITeamDetailViewModel, TeamDetailViewModel>();
            //containerRegistry.RegisterSingleton<ITeamDetail, TeamDetail>();

            //containerRegistry.RegisterSingleton<ITeamLookupDataService, TeamLookupDataService>();
            //containerRegistry.Register<ITeamDataService, TeamDataService>();

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            //this loads TeamMain into the Shell loaded in CreateShell() in App.Xaml.cs
            _regionManager.RegisterViewWithRegion(RegionNames.TeamMainRegion, typeof(ITeamMain));

            // These load into TeamMain.xaml
            //_regionManager.RegisterViewWithRegion(RegionNames.TeamNavigationRegion, typeof(ITeamNavigation));
            //_regionManager.RegisterViewWithRegion(RegionNames.TeamDetailRegion, typeof(ITeamDetail));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}