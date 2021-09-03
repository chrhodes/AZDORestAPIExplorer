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
            containerRegistry.RegisterSingleton<IRepositoryMain, RepositoryMain>();

            containerRegistry.Register<IBlobMainViewModel, BlobMainViewModel>();
            containerRegistry.RegisterSingleton<IBlobMain, BlobMain>();

            //containerRegistry.Register<ICommitMainViewModel, CommitMainViewModel>();
            containerRegistry.RegisterSingleton<ICommitMain, CommitMain>();

            containerRegistry.Register<ICommitChangeMainViewModel, CommitChangeMainViewModel>();
            containerRegistry.RegisterSingleton<ICommitChangeMain, CommitChangeMain>();

            containerRegistry.Register<IImportRequestMainViewModel, ImportRequestMainViewModel>();
            containerRegistry.RegisterSingleton<IImportRequestMain, ImportRequestMain>();

            containerRegistry.Register<IItemMainViewModel, ItemMainViewModel>();
            containerRegistry.RegisterSingleton<IItemMain, ItemMain>();

            containerRegistry.Register<IMergeMainViewModel, MergeMainViewModel>();
            containerRegistry.RegisterSingleton<IMergeMain, MergeMain>();

            containerRegistry.Register<IPullRequestMainViewModel, PullRequestMainViewModel>();
            containerRegistry.RegisterSingleton<IPullRequestMain, PullRequestMain>();

            containerRegistry.Register<IPushMainViewModel, PushMainViewModel>();
            containerRegistry.RegisterSingleton<IPushMain, PushMain>();

            containerRegistry.Register<IStatMainViewModel, StatMainViewModel>();
            containerRegistry.RegisterSingleton<IStatMain, StatMain>();

            containerRegistry.Register<IRefMainViewModel, RefMainViewModel>();
            containerRegistry.RegisterSingleton<IRefMain, RefMain>();

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
            _regionManager.RegisterViewWithRegion(RegionNames.RepositoryMainRegion, typeof(IRepositoryMain));

            _regionManager.RegisterViewWithRegion(RegionNames.BlobMainRegion, typeof(IBlobMain));

            _regionManager.RegisterViewWithRegion(RegionNames.CommitMainRegion, typeof(ICommitMain));

            _regionManager.RegisterViewWithRegion(RegionNames.CommitChangeMainRegion, typeof(ICommitChangeMain));

            _regionManager.RegisterViewWithRegion(RegionNames.ImportRequestMainRegion, typeof(IImportRequestMain));

            _regionManager.RegisterViewWithRegion(RegionNames.ItemMainRegion, typeof(IItemMain));

            _regionManager.RegisterViewWithRegion(RegionNames.MergeMainRegion, typeof(IMergeMain));

            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestMainRegion, typeof(IPullRequestMain));

            _regionManager.RegisterViewWithRegion(RegionNames.PushMainRegion, typeof(IPushMain));

            _regionManager.RegisterViewWithRegion(RegionNames.StatMainRegion, typeof(IStatMain));

            _regionManager.RegisterViewWithRegion(RegionNames.RefMainRegion, typeof(IRefMain));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}