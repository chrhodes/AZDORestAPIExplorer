using AZDORestApiExplorer.Domain.WorkItemTrackingProcess;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess
{
    public class SelectedProcessChangedEvent : PubSubEvent<Process>
    { 
    }
}
