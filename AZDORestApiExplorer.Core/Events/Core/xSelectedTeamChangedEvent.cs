using AZDORestApiExplorer.Domain.Core;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events.Core
{
    public class xSelectedTeamChangedEvent : PubSubEvent<Team> { }
}
