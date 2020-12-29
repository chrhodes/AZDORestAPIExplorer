﻿using System;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.Views
{
    public partial class ProjectNavigation : ViewBase, IProjectNavigation, IInstanceCountV
    {
        public ProjectNavigation()
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            InstanceCountV++;
            InitializeComponent();

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
