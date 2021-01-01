using System.Collections.ObjectModel;
using System.Net.Http;

using AZDORestApiExplorer.Domain;

using Prism.Events;

using VNC.Core.Mvvm;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class HTTPExchangeBase : EventViewModelBase, IInstanceCountVM
    {
        public HTTPExchangeBase(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            InstanceCountVM++;
        }

        public ObservableCollection<RequestResponseInfo> RequestResponseExchange { get; set; }
            = new ObservableCollection<RequestResponseInfo>();

        internal RequestResponseInfo InitializeExchange(HttpClient client, string requestUri)
        {
            RequestResponseExchange.Clear();
            RequestResponseInfo exchange = new RequestResponseInfo();

            exchange.Uri = requestUri;
            exchange.RequestHeadersX.AddRange(client.DefaultRequestHeaders);

            return exchange;
        }

        internal void RecordExchangeResponse(HttpResponseMessage response, RequestResponseInfo exchange)
        {
            var statusCode = response.StatusCode;
            var statusCode2 = (int)response.StatusCode;

            exchange.Response = response;
            exchange.ResponseStatusCode = statusCode2;

            exchange.ResponseContentHeaders.AddRange(response.Content.Headers);
            exchange.ResponseHeadersX.AddRange(response.Headers);

            RequestResponseExchange.Add(exchange);
        }

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
