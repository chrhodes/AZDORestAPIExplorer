using System;

using AZDORestApiExplorer.Core;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Core.Events;
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
    public class ProcessModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public ProcessModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            //containerRegistry.Register<IProcessMainViewModel, ProcessMainViewModel>();
            //containerRegistry.Register<IProcessMainViewModel, DomainViewModel<Process, GetProcessesEvent, GetProcessesEventArgs, SelectedProcessChangedEvent>>();

            containerRegistry.RegisterSingleton<IProcessMain, ProcessMain>();

            //containerRegistry.Register<IProcessNavigationViewModel, ProcessNavigationViewModel>();
            //containerRegistry.RegisterSingleton<IProcessNavigation, ProcessNavigation>();

            //containerRegistry.Register<IProcessDetailViewModel, ProcessDetailViewModel>();
            //containerRegistry.RegisterSingleton<IProcessDetail, ProcessDetail>();

            //containerRegistry.RegisterSingleton<IProcessLookupDataService, ProcessLookupDataService>();
            //containerRegistry.Register<IProcessDataService, ProcessDataService>();

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            ////this loads ProcessMain into the Shell loaded in CreateShell() in App.Xaml.cs
            _regionManager.RegisterViewWithRegion(RegionNames.ProcessMainRegion, typeof(IProcessMain));

            //// These load into ProcessMain.xaml
            //_regionManager.RegisterViewWithRegion(RegionNames.ProcessNavigationRegion, typeof(IProcessNavigation));
            //_regionManager.RegisterViewWithRegion(RegionNames.ProcessDetailRegion, typeof(IProcessDetail));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}