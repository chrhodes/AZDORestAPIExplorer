using AZDORestApiExplorer.Domain;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events
{
    public class SelectedTeamChangedEvent : PubSubEvent<Team> { }
}
