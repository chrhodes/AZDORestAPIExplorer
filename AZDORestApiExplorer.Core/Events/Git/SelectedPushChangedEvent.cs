using AZDORestApiExplorer.Domain.Git;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events.Git
{
    public class SelectedPushChangedEvent : PubSubEvent<Push> { }
}
