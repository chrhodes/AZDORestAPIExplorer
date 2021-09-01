using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using VNC.HttpHelper;

namespace AZDORestApiExplorer.Domain
{
    public class RESTResult<T> : INotifyPropertyChanged where T : class
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private int _count;

        public int Count
        {
            get => _count;
            set
            {
                if (_count == value)
                    return;
                _count = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            }
        }

        private T _selectedItem;

        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value)
                    return;
                _selectedItem = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }

        private ObservableCollection<T> _items = new ObservableCollection<T>();

        // Replacing collection does not fire PCN.  Need to raise ourselves.

        //public ObservableCollection<T> ResultItems
        //{
        //    get => _items;
        //    set => _items = value;
        //}

        public ObservableCollection<T> ResultItems
        {
            get => _items;
            set
            {
                if (_items == value)
                    return;
                _items = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResultItems)));
            }
        }

        // NOTE(crhodes)
        // 2021.08.31 HACK
        //private string _requestUri;

        //public string RequestUri
        //{
        //    get => _requestUri;
        //    set
        //    {
        //        if (_requestUri == value)
        //            return;
        //        _requestUri = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RequestUri)));
        //    }
        //}

        public ObservableCollection<RequestResponseInfo> RequestResponseExchange { get; set; }
            = new ObservableCollection<RequestResponseInfo>();

        public void InitializeHttpClient(Organization collection, HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //var username = @"Christopher.Rhodes@bd.com";
            //var password = @"HappyH0jnacki08";

            //string base64PAT = Convert.ToBase64String(
            //        ASCIIEncoding.ASCII.GetBytes($"{username}:{password}"));
            string base64PAT = Convert.ToBase64String(
                    ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", collection.PAT)));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64PAT);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("NTLM");
        }

        public RequestResponseInfo InitializeExchange(HttpClient client, string requestUri)
        {
            // NOTE(crhodes)
            // 2021.08.31 HACK
            //RequestUri = requestUri;

            //RequestResponseExchange.Clear();
            RequestResponseInfo exchange = new RequestResponseInfo();

            exchange.Uri = requestUri;
            exchange.RequestHeaders.AddRange(client.DefaultRequestHeaders);

            return exchange;
        }

        public void RecordExchangeResponse(HttpResponseMessage response, RequestResponseInfo exchange)
        {
            var statusCode = response.StatusCode;
            var statusCode2 = (int)response.StatusCode;

            exchange.Response = response;
            exchange.ResponseStatusCode = statusCode2;

            exchange.ResponseContentHeaders.AddRange(response.Content.Headers);
            exchange.ResponseHeaders.AddRange(response.Headers);

            RequestResponseExchange.Add(exchange);
        }

        public RequestResponseInfo ContinueExchange(HttpClient client, string requestUri)
        {
            // NOTE(crhodes)
            // 2021.08.31 HACK
            //RequestUri = requestUri;

            //RequestResponseExchange.Clear();
            RequestResponseInfo exchange = new RequestResponseInfo();

            exchange.Uri = requestUri;
            exchange.RequestHeaders.AddRange(client.DefaultRequestHeaders);

            return exchange;
        }

    }
}
