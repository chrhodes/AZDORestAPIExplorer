using AZDORestApiExplorer.Domain.Git;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events.Git
{
    public class SelectedPullRequestChangedEvent : PubSubEvent<PullRequest> { }
}
