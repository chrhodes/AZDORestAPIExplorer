using System;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.Presentation.Views;
using AZDORestApiExplorer.WorkItemTracking.Presentation.Views;

using Prism.Regions;

using Unity;

using VNC;
using VNC.Core.Mvvm;
using VNC.WPF.Presentation.Dx.Views;

namespace AZDORestApiExplorer
{
    public class ShellService : IShellService
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        public ShellService(IUnityContainer container, IRegionManager regionManager)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _container = container;
            _regionManager = regionManager;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public void ShowShell(string uri)
        {
            Int64 startTicks = Log.VIEWMODEL($"Enter: ({uri})", Common.LOG_CATEGORY);

            var shell = _container.Resolve<Shell>();

            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);

            scopedRegion.RequestNavigate(RegionNames.ContentRegion, uri);

            shell.Show();

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        public static DxThemedWindowHost vncMVVM_V1_Modal_Host = null;
        public void ShowShell()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            var foo = _container.Resolve<CreateWorkItemMain>();

            DxThemedWindowHost.DisplayUserControlInHost(ref vncMVVM_V1_Modal_Host,
                "MVVM View First (View is passed ViewModel) Modal",
                600, 450,
                //Common.DEFAULT_WINDOW_WIDTH, Common.DEFAULT_WINDOW_HEIGHT,
                VNC.Core.Presentation.ShowWindowMode.Modeless_Show, foo);

            //var shell = _container.Resolve<Shell>();

            //var scopedRegion = _regionManager.CreateRegionManager();
            //RegionManager.SetRegionManager(shell, scopedRegion);

            //scopedRegion.RequestNavigate(RegionNames.ContentRegion, uri);

            //shell.Show();

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}
