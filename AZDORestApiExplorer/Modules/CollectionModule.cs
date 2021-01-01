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
    public class CollectionModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public CollectionModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            //containerRegistry.RegisterSingleton<ICollectionMainViewModel, CollectionMainViewModel>();
            //containerRegistry.RegisterSingleton<ICollectionMain, CollectionMain>();

            //containerRegistry.Register<ICollectionNavigationViewModel, CollectionNavigationViewModel>();
            //containerRegistry.RegisterSingleton<ICollectionNavigation, CollectionNavigation>();

            //containerRegistry.Register<ICollectionDetailViewModel, CollectionDetailViewModel>();
            //containerRegistry.RegisterSingleton<ICollectionDetail, CollectionDetail>();

            //containerRegistry.RegisterSingleton<ICollectionLookupDataService, CollectionLookupDataService>();
            //containerRegistry.Register<ICollectionDataService, CollectionDataService>();

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            //this loads CollectionMain into the Shell loaded in CreateShell() in App.Xaml.cs
            _regionManager.RegisterViewWithRegion(RegionNames.CollectionMainRegion, typeof(ICollectionMain));

            // These load into CollectionMain.xaml
            //_regionManager.RegisterViewWithRegion(RegionNames.CollectionNavigationRegion, typeof(ICollectionNavigation));
            //_regionManager.RegisterViewWithRegion(RegionNames.CollectionDetailRegion, typeof(ICollectionDetail));

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

    }
}