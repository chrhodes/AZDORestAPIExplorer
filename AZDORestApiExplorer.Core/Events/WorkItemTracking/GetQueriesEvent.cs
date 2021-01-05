using AZDORestApiExplorer.Core.Events.WorkItemTracking;

using Prism.Events;

namespace AZDORestApiExplorer.WorkItemTracking.Core.Events
{
    public class GetQueriesEvent : PubSubEvent<GetQueriesEventArgs> { }
}