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
using VNC.Core.Mvvm;
using VNC.Core.Presentation.ViewModels;
using VNC.Core.Presentation.Views;

namespace AZDORestApiExplorer
{
    public class AZDORestApiExplorerModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public AZDORestApiExplorerModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            containerRegistry.RegisterSingleton<CollectionMainViewModel, CollectionMainViewModel>();
            containerRegistry.RegisterSingleton<CollectionMain>();

            containerRegistry.RegisterSingleton<StatusBarViewModel>();
            containerRegistry.RegisterSingleton<StatusBar>();

            containerRegistry.RegisterSingleton<ContextMainViewModel, ContextMainViewModel>();
            containerRegistry.RegisterSingleton<ContextMain>();

            containerRegistry.RegisterSingleton<IShellService, ShellService>();

            containerRegistry.RegisterSingleton<CommandsGet>();
            containerRegistry.RegisterSingleton<CommandsPatch>();
            containerRegistry.RegisterSingleton<CommandsPost>();
            containerRegistry.RegisterSingleton<CommandsPut>();

            containerRegistry.RegisterSingleton<ProcessMain>();
            containerRegistry.RegisterSingleton<ProjectMain>();
            containerRegistry.RegisterSingleton<TeamMain>();

            containerRegistry.RegisterDialog<NotificationDialog, NotificationDialogViewModel>("NotificationDialog");

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            _regionManager.RegisterViewWithRegion(RegionNames.CollectionMainRegion, typeof(CollectionMain));
            _regionManager.RegisterViewWithRegion(RegionNames.ContextMainRegion, typeof(ContextMain));
            _regionManager.RegisterViewWithRegion(RegionNames.StatusBarRegion, typeof(StatusBar));

            _regionManager.RegisterViewWithRegion(RegionNames.CommandGetRegion, typeof(CommandsGet));
            _regionManager.RegisterViewWithRegion(RegionNames.CommandPatchRegion, typeof(CommandsPatch));
            _regionManager.RegisterViewWithRegion(RegionNames.CommandPostRegion, typeof(CommandsPost));
            _regionManager.RegisterViewWithRegion(RegionNames.CommandPutRegion, typeof(CommandsPut));

            _regionManager.RegisterViewWithRegion(RegionNames.ProcessMainRegion, typeof(ProcessMain));
            _regionManager.RegisterViewWithRegion(RegionNames.ProjectMainRegion, typeof(ProjectMain));
            _regionManager.RegisterViewWithRegion(RegionNames.TeamMainRegion, typeof(TeamMain));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}