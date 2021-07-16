using System;

using AZDORestApiExplorer.Domain.Test;
using AZDORestApiExplorer.Domain.Test.Events;
using AZDORestApiExplorer.Presentation.ViewModels;

using DevExpress.Xpf.Grid;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Test.Presentation.Views
{
    public partial class TestCaseMain : ViewBase, ITestCaseMain, IInstanceCountV
    {
        public TestCaseMain(DomainViewModel<TestCase, GetTestCasesEvent, GetTestCasesEventArgs, SelectedTestCaseChangedEvent> viewModel)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            InstanceCountV++;
            InitializeComponent();

            ViewModel = viewModel;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }
        //public TestCaseMain(ViewModels.ITestCaseMainViewModel viewModel)
        //{
        //    Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

        //    InstanceCountV++;
        //    InitializeComponent();

        //    ViewModel = viewModel;

        //    Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        //}

        private GridControl _targetGrid;

        public GridControl TargetGrid
        {
            get => _targetGrid;
            set => _targetGrid = value;

        }

        #region IInstanceCount

        private static int _instanceCountV;

        public int InstanceCountV
        {
            get => _instanceCountV;
            set => _instanceCountV = value;
        }

        #endregion

    }
}
