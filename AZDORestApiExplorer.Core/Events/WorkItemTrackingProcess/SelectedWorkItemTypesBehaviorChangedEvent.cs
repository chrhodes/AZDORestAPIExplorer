using AZDORestApiExplorer.WorkItemTrackingProcess.Domain;

using Prism.Events;

namespace AZDORestApiExplorer.WorkItemTrackingProcess.Core.Events
{
    public class SelectedWorkItemTypesBehaviorChangedEvent : PubSubEvent<WorkItemTypesBehavior> { }
}