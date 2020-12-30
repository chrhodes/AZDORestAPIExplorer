using AZDORestApiExplorer.Domain;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events
{
    public class GetWorkItemTypesEvent : PubSubEvent<GetWorkItemTypesEventArgs> { }
}
