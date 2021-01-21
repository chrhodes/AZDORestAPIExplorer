using System;
using System.Windows;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.Views
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window, IView
    {
        public Shell()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
