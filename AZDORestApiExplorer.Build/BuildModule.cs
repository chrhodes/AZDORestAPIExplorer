using System;

using AZDORestApiExplorer.Build.Presentation.Views;
using AZDORestApiExplorer.Core;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using VNC;

namespace AZDORestApiExplorer.Build
{
    public class BuildModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public BuildModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            //containerRegistry.Register<IAuthorizedResourceMainViewModel, AuthorizedResourceMainViewModel>();
            containerRegistry.RegisterSingleton<AuthorizedResourceMain>();
            //containerRegistry.Register<BuildMainViewModel>();
            containerRegistry.RegisterSingleton<BuildMain>();
            containerRegistry.RegisterSingleton<DefinitionMain>();

            //containerRegistry.Register<ITYPEMainViewModel, TYPEMainViewModel>();
            //containerRegistry.RegisterSingleton<ITYPE, TYPE>();

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            //this loads AccountMain into the Shell loaded in CreateShell() in App.Xaml.cs
            //_regionManager.RegisterViewWithRegion(RegionNames.TYPEMainRegion, typeof(ITYPEMain));

            _regionManager.RegisterViewWithRegion(RegionNames.AuthorizedResourceMainRegion, typeof(AuthorizedResourceMain));
            _regionManager.RegisterViewWithRegion(RegionNames.BuildMainRegion, typeof(BuildMain));
            _regionManager.RegisterViewWithRegion(RegionNames.DefinitionMainRegion, typeof(DefinitionMain));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}