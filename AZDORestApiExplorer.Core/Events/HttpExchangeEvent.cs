using System.Collections.ObjectModel;

using Prism.Events;

using VNC.HttpHelper;

namespace AZDORestApiExplorer.Core.Events
{
    public class HttpExchangeEvent : PubSubEvent<ObservableCollection<RequestResponseInfo>> { }
}
