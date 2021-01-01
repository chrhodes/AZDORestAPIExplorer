using System;
using System.Collections.ObjectModel;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain;

using Prism.Events;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class HTTPExchangeMainViewModel : EventViewModelBase, IHTTPExchangeMainViewModel, IInstanceCountVM
    {

        #region Constructors, Initialization, and Load

        public HTTPExchangeMainViewModel(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            // TODO(crhodes)
            // Save constructor parameters here

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            InstanceCountVM++;

            // TODO(crhodes)
            //

            EventAggregator.GetEvent<HttpExchangeEvent>().Subscribe(NewHttpExchange);

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public ObservableCollection<RequestResponseInfo> RequestResponseExchange { get; set; }
            = new ObservableCollection<RequestResponseInfo>();

        //private int _requestResponseExchangeCount;

        //public int RequestResponseExchangeCount
        //{
        //    get => _requestResponseExchangeCount;
        //    set
        //    {
        //        if (_requestResponseExchangeCount == value)
        //            return;
        //        _requestResponseExchangeCount = value;
        //        OnPropertyChanged();
        //    }
        //}

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
            // NOTE(crhodes)
            // This does not work to refresh bindings
            //RequestResponseExchange = exchange;

            RequestResponseExchange.Clear();
            RequestResponseExchange.AddRange(exchange);

            ////foreach (RequestResponseInfo item in exchange)
            ////{
            ////    RequestResponseInfo rri = new RequestResponseInfo();
            ////    rri.Response = item.Response;
            ////    rri.ResponseStatusCode = item.ResponseStatusCode;
            ////    rri.ResponseContentHeaders = item.ResponseContentHeaders;
            ////    rri.ResponseHeadersX = item.ResponseHeadersX;

            ////    RequestResponseExchange.Add(rri);
            ////}

            //RequestResponseExchangeCount = exchange.Count();
        }

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
