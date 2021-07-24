using System;

using AZDORestApiExplorer.Accounts.Presentation.Views;
using AZDORestApiExplorer.Core;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using VNC;

namespace AZDORestApiExplorer.Accounts
{
    public class AccountsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public AccountsModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            //containerRegistry.Register<IAccountMainViewModel, xAccountMainViewModel>();
            containerRegistry.RegisterSingleton<IAccountMain, AccountMain>();

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
            _regionManager.RegisterViewWithRegion(RegionNames.AccountMainRegion, typeof(IAccountMain));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}