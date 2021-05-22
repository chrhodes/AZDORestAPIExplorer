using System;
using AZDORestApiExplorer.Presentation.ViewModels;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.Views
{
    public partial class CommandsPatch : ViewBase
    {
        public CommandsPatch(CommandsPatchViewModel viewModel)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            //InstanceCountV++;
            InitializeComponent();

            ViewModel = viewModel;
            //Loaded += UserControl_Loaded;

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }
    }
}
