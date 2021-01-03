using AZDORestApiExplorer.Domain;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess
{
    public class GetBehaviorsEvent : PubSubEvent<GetBehaviorsEventArgs> { }
}