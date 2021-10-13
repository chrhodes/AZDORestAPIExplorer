using System;

using AZDORestApiExplorer.Core;

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
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            // NOTE(crhodes)
            // Don't need ViewModels for basic View with Grid.  Use DomainViewModel

            //containerRegistry.Register<ITestPlanMainViewModel, TestPlanMainViewModel>();
            containerRegistry.RegisterSingleton<TestPlanMain>();

            //containerRegistry.Register<ITestSuiteMainViewModel, TestSuiteMainViewModel>();
            containerRegistry.RegisterSingleton<TestSuiteMain>();

            //containerRegistry.Register<ITestCaseMainViewModel, TestCaseMainViewModel>();
            containerRegistry.RegisterSingleton<TestCaseMain>();

            //containerRegistry.Register<ITestPointMainViewModel, TestPointMainViewModel>();
            containerRegistry.RegisterSingleton<TestPointMain>();

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            Int64 startTicks = Log.MODULE("Enter", Common.LOG_CATEGORY);

            // NOTE(crhodes)
            // using typeof(TYPE) calls constructor
            // using typeof(ITYPE) resolves type (see RegisterTypes)

            _regionManager.RegisterViewWithRegion(RegionNames.TestPlanMainRegion, typeof(TestPlanMain));

            _regionManager.RegisterViewWithRegion(RegionNames.TestSuiteMainRegion, typeof(TestSuiteMain));

            _regionManager.RegisterViewWithRegion(RegionNames.TestCaseMainRegion, typeof(TestCaseMain));

            _regionManager.RegisterViewWithRegion(RegionNames.TestPointMainRegion, typeof(TestPointMain));

            Log.MODULE("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}