using AZDORestApiExplorer.Domain.Git;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events
{
    public class SelectedRepositoryChangedEvent : PubSubEvent<Repository> { }
}
