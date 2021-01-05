using AZDORestApiExplorer.Domain.WorkItemTracking;

using Prism.Events;

namespace AZDORestApiExplorer.WorkItemTracking.Core.Events
{
    public class SelectedWorkItemTypeChangedEvent : PubSubEvent<WorkItemType> { }
}