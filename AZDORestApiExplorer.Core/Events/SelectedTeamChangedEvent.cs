using AZDORestApiExplorer.Domain.Core;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events
{
    public class SelectedTeamChangedEvent : PubSubEvent<Team> { }
}
