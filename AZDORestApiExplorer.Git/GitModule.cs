using System;

using AZDORestApiExplorer.Git.Presentation.Views;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.Git.Presentation.ViewModels;
using AZDORestApiExplorer.Git.Presentation.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using VNC;
using VNC.Core.Presentation.Views;
using VNC.Core.Presentation.ViewModels;

namespace AZDORestApiExplorer.Git
{
    public class GitModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public GitModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            //containerRegistry.Register<IRepositoryMainViewModel, RepositoryMainViewModel>();
            containerRegistry.RegisterSingleton<RepositoryMain>();

            containerRegistry.Register<BlobMainViewModel>();
            containerRegistry.RegisterSingleton<BlobMain>();

            //containerRegistry.Register<ICommitMainViewModel, CommitMainViewModel>();
            containerRegistry.RegisterSingleton<CommitMain>();

            containerRegistry.Register<CommitChangeMainViewModel>();
            containerRegistry.RegisterSingleton<CommitChangeMain>();

            containerRegistry.Register<ImportRequestMainViewModel>();
            containerRegistry.RegisterSingleton<ImportRequestMain>();

            containerRegistry.Register<ItemMainViewModel>();
            containerRegistry.RegisterSingleton<ItemMain>();

            containerRegistry.Register<MergeMainViewModel>();
            containerRegistry.RegisterSingleton<MergeMain>();

            containerRegistry.Register<PullRequestMainViewModel>();
            containerRegistry.RegisterSingleton<PullRequestMain>();

            containerRegistry.Register<PushMainViewModel>();
            containerRegistry.RegisterSingleton<PushMain>();

            containerRegistry.Register<StatMainViewModel>();
            containerRegistry.RegisterSingleton<StatMain>();

            containerRegistry.Register<RefMainViewModel>();
            containerRegistry.RegisterSingleton<RefMain>();

            // containerRegistry.RegisterSingleton<IRepositoryLookupDataService, RepositoryLookupDataService>();
            // containerRegistry.Register<IRepositoryDataService, RepositoryDataService>();

            containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>("NotificationDialog");

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            //this loads RepositoryMain into the Shell loaded in CreateShell() in App.Xaml.cs
            _regionManager.RegisterViewWithRegion(RegionNames.RepositoryMainRegion, typeof(RepositoryMain));

            _regionManager.RegisterViewWithRegion(RegionNames.BlobMainRegion, typeof(BlobMain));

            _regionManager.RegisterViewWithRegion(RegionNames.CommitMainRegion, typeof(CommitMain));

            _regionManager.RegisterViewWithRegion(RegionNames.CommitChangeMainRegion, typeof(CommitChangeMain));

            _regionManager.RegisterViewWithRegion(RegionNames.ImportRequestMainRegion, typeof(ImportRequestMain));

            _regionManager.RegisterViewWithRegion(RegionNames.ItemMainRegion, typeof(ItemMain));

            _regionManager.RegisterViewWithRegion(RegionNames.MergeMainRegion, typeof(MergeMain));

            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestMainRegion, typeof(PullRequestMain));

            _regionManager.RegisterViewWithRegion(RegionNames.PushMainRegion, typeof(PushMain));

            _regionManager.RegisterViewWithRegion(RegionNames.StatMainRegion, typeof(StatMain));

            _regionManager.RegisterViewWithRegion(RegionNames.RefMainRegion, typeof(RefMain));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}