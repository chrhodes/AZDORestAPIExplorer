using System;

using VNC;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _title = "AZDORestApiExplorer - MainWindow";

        public string Title
        {
            get => _title;
            set
            {
                if (_title == value)
                    return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }
    }
}
