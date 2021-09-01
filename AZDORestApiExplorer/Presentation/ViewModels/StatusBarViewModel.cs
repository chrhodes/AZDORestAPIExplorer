using System;
using System.Collections.ObjectModel;

using AZDORestApiExplorer.Core.Events;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class StatusBarViewModel : EventViewModelBase, IInstanceCountVM
    {

        #region Constructors, Initialization, and Load

        public StatusBarViewModel(
            IEventAggregator eventAggregator,
            DialogService dialogService) : base(eventAggregator, dialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            InstanceCountVM++;

            EventAggregator.GetEvent<HttpExchangeEvent>().Subscribe(NewHttpExchange);
            //EventAggregator.GetEvent<HttpUriEvent>().Subscribe(NewHttpUri);

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        private string _currentHTTPRequest;
        private string _title = "StatusBar";


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

        
        public string CurrentHTTPRequest
        {
            get => _currentHTTPRequest;
            set
            {
                if (_currentHTTPRequest == value)
                    return;
                _currentHTTPRequest = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private void NewHttpExchange(ObservableCollection<RequestResponseInfo> exchange)
        {
            if (exchange.Count != 0)
            {
                CurrentHTTPRequest = exchange[0].Uri;
            }

        }

        //private void NewHttpUri(string uri)
        //{
        //    CurrentHTTPRequest = uri;
        //}

        #endregion

        #region IInstanceCount

        private static int _instanceCountVM;

        public int InstanceCountVM
        {
            get => _instanceCountVM;
            set => _instanceCountVM = value;
        }

        #endregion
    }
}
