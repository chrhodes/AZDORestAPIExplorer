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
    public class ProjectModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public ProjectModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            //containerRegistry.Register<IProjectMainViewModel, ProjectMainViewModel>();
            containerRegistry.RegisterSingleton<IProjectMain, ProjectMain>();

            //containerRegistry.Register<IProjectNavigationViewModel, ProjectNavigationViewModel>();
            //containerRegistry.RegisterSingleton<IProjectNavigation, ProjectNavigation>();

            //containerRegistry.Register<IProjectDetailViewModel, ProjectDetailViewModel>();
            //containerRegistry.RegisterSingleton<IProjectDetail, ProjectDetail>();

            //containerRegistry.RegisterSingleton<IProjectLookupDataService, ProjectLookupDataService>();
            //containerRegistry.Register<IProjectDataService, ProjectDataService>();

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            ////this loads ProjectMain into the Shell loaded in CreateShell() in App.Xaml.cs
            _regionManager.RegisterViewWithRegion(RegionNames.ProjectMainRegion, typeof(IProjectMain));

            //// These load into ProjectMain.xaml
            //_regionManager.RegisterViewWithRegion(RegionNames.ProjectNavigationRegion, typeof(IProjectNavigation));
            //_regionManager.RegisterViewWithRegion(RegionNames.ProjectDetailRegion, typeof(IProjectDetail));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}