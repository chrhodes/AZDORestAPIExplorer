using AZDORestApiExplorer.Domain.WorkItemTracking;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events.WorkItemTracking
{
    public class SelectedQueryChangedEvent : PubSubEvent<Query> { }
}