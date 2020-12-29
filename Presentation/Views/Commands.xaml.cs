using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using AZDORestApiExplorer.Presentation.ViewModels;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.Views
{
    /// <summary>
    /// Interaction logic for Commands.xaml
    /// </summary>
    public partial class Commands : ViewBase
    {
        public Commands(CommandsViewModel viewModel)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            //InstanceCountV++;
            InitializeComponent();

            ViewModel = viewModel;
            //Loaded += UserControl_Loaded;

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }
    }
}
