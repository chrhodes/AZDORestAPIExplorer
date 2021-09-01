using System.Collections.ObjectModel;

using Prism.Events;

using VNC.Core.Net;

namespace AZDORestApiExplorer.Core.Events
{
    public class HttpExchangeEvent : PubSubEvent<ObservableCollection<RequestResponseInfo>> { }
    public class HttpUriEvent : PubSubEvent<string> { }
}
