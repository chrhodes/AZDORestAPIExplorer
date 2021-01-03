using AZDORestApiExplorer.Domain;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess
{
    public class GetWorkItemTypesEvent : PubSubEvent<GetWorkItemTypesEventArgs> { }
}
