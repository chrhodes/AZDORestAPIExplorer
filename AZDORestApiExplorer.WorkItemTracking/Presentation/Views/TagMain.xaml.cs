﻿using System;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.WorkItemTracking.Presentation.Views
{
    public partial class TagMain : ViewBase, ITagMain, IInstanceCountV
    {

        public TagMain(ViewModels.ITagMainViewModel viewModel)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            InstanceCountV++;
            InitializeComponent();

            ViewModel = viewModel;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
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
