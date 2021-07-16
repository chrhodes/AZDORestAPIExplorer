using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.Views
{
    public partial class CommonGridHeader : UserControl, INotifyPropertyChanged
    {
        public CommonGridHeader()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Dependency Properties

        //#region OutputFileNameAndPath

        //public static readonly DependencyProperty OutputFileNameAndPathProperty = DependencyProperty.Register("OutputFileNameAndPath",
        //    typeof(string), typeof(CommonGridHeader), new PropertyMetadata(null, new PropertyChangedCallback(OnOutputFileNameAndPathChanged)));

        //private static void OnOutputFileNameAndPathChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        //{
        //    CommonGridHeader commonGridHeader = o as CommonGridHeader;
        //    if (commonGridHeader != null)
        //        commonGridHeader.OnOutputFileNameAndPathChanged((string)e.OldValue, (string)e.NewValue);
        //}

        //protected virtual void OnOutputFileNameAndPathChanged(string oldValue, string newValue)
        //{
        //    // TODO: Add your property changed side-effects. Descendants can override as well.
        //    teOutput.Text = newValue;
        //}

        //public string OutputFileNameAndPath
        //{
        //    // IMPORTANT: To maintain parity between setting a property in XAML and procedural code, do not touch the getter and setter inside this dependency property!
        //    get => (string)GetValue(OutputFileNameAndPathProperty);
        //    set => SetValue(OutputFileNameAndPathProperty, value);
        //}

        //#endregion

        #endregion

    }
}
