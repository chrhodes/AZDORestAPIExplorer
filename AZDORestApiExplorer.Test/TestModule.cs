﻿using System;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.Test.Presentation.ViewModels;
using AZDORestApiExplorer.Test.Presentation.Views;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using VNC;

namespace AZDORestApiExplorer.Test
{
    public class TestModule : IModule
    {
        private readonly IRegionManager _regionManager;

        // 01

        public TestModule(IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            containerRegistry.Register<ITestPlanMainViewModel, TestPlanMainViewModel>();
            containerRegistry.RegisterSingleton<ITestPlanMain, TestPlanMain>();

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_APPNAME);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            _regionManager.RegisterViewWithRegion(RegionNames.TestPlanMainRegion, typeof(ITestPlanMain));

            Log.MODULE("Exit", Common.LOG_APPNAME, startTicks);
        }
    }
}