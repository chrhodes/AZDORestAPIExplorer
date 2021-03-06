﻿using System;
using System.Windows;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.Views
{
    public partial class ContextMain : ViewBase, IContextMain, IInstanceCountV
    {

        public ContextMain(ViewModels.IContextMainViewModel viewModel)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            InstanceCountV++;
            InitializeComponent();

            ViewModel = viewModel;
            //Loaded += UserControl_Loaded;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        //private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Int64 startTicks = Log.EVENT_HANDLER("(ContextMain) Enter", Common.LOG_APPNAME);

        //    //await ((ViewModels.IContextMainViewModel)ViewModel).LoadAsync();

        //    Log.EVENT_HANDLER("(ContextMain) Exit", Common.LOG_APPNAME, startTicks);
        //}

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
