using System;
using System.Windows;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.Views
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window, IView, IInstanceCountV
    {
        public Shell()
        {
            InstanceCountV++;
            InitializeComponent();
        }

        public IViewModel ViewModel
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
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
