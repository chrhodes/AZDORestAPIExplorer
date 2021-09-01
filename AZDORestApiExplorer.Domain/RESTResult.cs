using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

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

        private string _requestUri;

        public string RequestUri
        {
            get => _requestUri;
            set
            {
                if (_requestUri == value)
                    return;
                _requestUri = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RequestUri)));
            }
        }

        private RequestResponseInfo _hTTPExchange;

        public RequestResponseInfo HTTPExchange
        {
            get => _hTTPExchange;
            set
            {
                if (_hTTPExchange == value)
                    return;
                _hTTPExchange = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HTTPExchange)));
            }
        }

        public RequestResponseInfo InitializeExchange(HttpClient client, string requestUri)
        {
            RequestUri = requestUri;

            RequestResponseExchange.Clear();
            RequestResponseInfo exchange = new RequestResponseInfo();

            exchange.Uri = requestUri;
            exchange.RequestHeaders.AddRange(client.DefaultRequestHeaders);

            return exchange;
        }

    }
}
