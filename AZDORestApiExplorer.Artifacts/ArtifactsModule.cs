using System;

using AZDORestApiExplorer.Artifacts.Presentation.Views;
using AZDORestApiExplorer.Core;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using VNC;

namespace AZDORestApiExplorer.Artifacts
{
    public class ArtifactsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public ArtifactsModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            //containerRegistry.Register<IFeedMainViewModel, FeedMainViewModel>();
            containerRegistry.RegisterSingleton<IFeedMain, FeedMain>();

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            _regionManager.RegisterViewWithRegion(RegionNames.FeedMainRegion, typeof(IFeedMain));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}