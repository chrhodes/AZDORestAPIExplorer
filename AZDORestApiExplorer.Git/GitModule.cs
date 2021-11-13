using System;

using AZDORestApiExplorer.Core;

using AZDORestApiExplorer.Git.Presentation.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using VNC;
using VNC.Core.Presentation.ViewModels;
using VNC.Core.Presentation.Views;

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

            //containerRegistry.Register<BlobMainViewModel>();
            containerRegistry.RegisterSingleton<BlobMain>();

            //containerRegistry.Register<ICommitMainViewModel, CommitMainViewModel>();
            containerRegistry.RegisterSingleton<CommitMain>();
            containerRegistry.RegisterSingleton<CommitChangeMain>();

            containerRegistry.RegisterSingleton<ImportRequestMain>();

            containerRegistry.RegisterSingleton<ItemMain>();

            containerRegistry.RegisterSingleton<MergeMain>();

            containerRegistry.RegisterSingleton<PullRequestMain>();
            containerRegistry.RegisterSingleton<PullRequestAttachmentMain>();
            containerRegistry.RegisterSingleton<PullRequestCommitMain>();
            containerRegistry.RegisterSingleton<PullRequestCommitChangeMain>();
            containerRegistry.RegisterSingleton<PullRequestIterationMain>();
            containerRegistry.RegisterSingleton<PullRequestIterationChangeMain>();
            containerRegistry.RegisterSingleton<PullRequestLabelMain>();
            containerRegistry.RegisterSingleton<PullRequestPropertyMain>();
            containerRegistry.RegisterSingleton<PullRequestReviewerMain>();
            containerRegistry.RegisterSingleton<PullRequestStatusMain>();
            containerRegistry.RegisterSingleton<PullRequestThreadMain>();
            containerRegistry.RegisterSingleton<PullRequestThreadCommentMain>();
            containerRegistry.RegisterSingleton<PullRequestWorkItemMain>();

            containerRegistry.RegisterSingleton<PushMain>();

            containerRegistry.RegisterSingleton<StatMain>();

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

            _regionManager.RegisterViewWithRegion(RegionNames.RepositoryCommitMainRegion, typeof(CommitMain));

            _regionManager.RegisterViewWithRegion(RegionNames.RepositoryCommitChangeMainRegion, typeof(CommitChangeMain));

            _regionManager.RegisterViewWithRegion(RegionNames.ImportRequestMainRegion, typeof(ImportRequestMain));

            _regionManager.RegisterViewWithRegion(RegionNames.ItemMainRegion, typeof(ItemMain));

            _regionManager.RegisterViewWithRegion(RegionNames.MergeMainRegion, typeof(MergeMain));

            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestMainRegion, typeof(PullRequestMain));

            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestAttachmentMainRegion, typeof(PullRequestAttachmentMain));

            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestCommitMainRegion, typeof(PullRequestCommitMain));
            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestCommitChangeMainRegion, typeof(PullRequestCommitChangeMain));

            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestIterationMainRegion, typeof(PullRequestIterationMain));
            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestIterationChangeMainRegion, typeof(PullRequestIterationChangeMain));

            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestLabelMainRegion, typeof(PullRequestLabelMain));
            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestPropertyMainRegion, typeof(PullRequestPropertyMain));
            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestReviewerMainRegion, typeof(PullRequestReviewerMain));
            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestStatusMainRegion, typeof(PullRequestStatusMain));
            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestThreadMainRegion, typeof(PullRequestThreadMain));
            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestThreadCommentMainRegion, typeof(PullRequestThreadCommentMain));
            _regionManager.RegisterViewWithRegion(RegionNames.PullRequestWorkItemMainRegion, typeof(PullRequestWorkItemMain));

            _regionManager.RegisterViewWithRegion(RegionNames.PushMainRegion, typeof(PushMain));

            _regionManager.RegisterViewWithRegion(RegionNames.StatMainRegion, typeof(StatMain));

            _regionManager.RegisterViewWithRegion(RegionNames.RefMainRegion, typeof(RefMain));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}