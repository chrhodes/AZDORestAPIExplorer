using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.DomainServices;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

namespace AZDORestApiExplorer
{
    public class CatModule : IModule
    {
        private readonly IRegionManager _regionManager;
        //private readonly IUnityContainer _container;

        // 01

        public CatModule(IRegionManager regionManager)
        {
            //_container = container;
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<Presentation.ViewModels.ITYPEDetailViewModel, Presentation.ViewModels.CatDetailViewModel>();
            containerRegistry.Register<Presentation.Views.ITYPEDetail, Presentation.Views.TYPEDetail>();

            containerRegistry.Register<ICatDataService, CatDataService>();

            containerRegistry.Register<Presentation.ViewModels.ITYPEViewModel, Presentation.TYPE.ViewModels.TYPEViewModel>();
            containerRegistry.Register<Presentation.Views.ITYPE, Presentation.Views.TYPE>();

            containerRegistry.Register<ICatLookupDataService, LookupDataService>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.TYPERegion, typeof(Presentation.Views.TYPE));
            _regionManager.RegisterViewWithRegion(RegionNames.TYPEDetailRegion, typeof(Presentation.Views.TYPEDetail));
        }
    }
}