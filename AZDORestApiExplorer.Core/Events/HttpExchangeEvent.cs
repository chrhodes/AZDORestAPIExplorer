using System.Collections.ObjectModel;

using AZDORestApiExplorer.Domain;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events
{
    public class HttpExchangeEvent : PubSubEvent<ObservableCollection<RequestResponseInfo>> { }
}
