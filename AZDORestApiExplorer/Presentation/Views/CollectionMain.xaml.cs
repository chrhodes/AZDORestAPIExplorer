﻿using System;
using System.Windows;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.Views
{
    public partial class CollectionMain : ViewBase, ICollectionMain //, IInstanceCountV
    {

        public CollectionMain(ViewModels.ICollectionMainViewModel viewModel)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            //InstanceCountV++;
            InitializeComponent();

            ViewModel = viewModel;
            //Loaded += UserControl_Loaded;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        //private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Int64 startTicks = Log.EVENT_HANDLER("(CollectionMain) Enter", Common.LOG_APPNAME);

        //    await ((ViewModels.ICollectionMainViewModel)ViewModel).LoadAsync();

        //    Log.EVENT_HANDLER("(CollectionMain) Exit", Common.LOG_APPNAME, startTicks);
        //}

        //#region IInstanceCount

        //private static int _instanceCountV;

        //public int InstanceCountV
        //{
        //    get => _instanceCountV;
        //    set => _instanceCountV = value;
        //}

        //#endregion

    }
}
