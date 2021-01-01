using AZDORestApiExplorer.Domain;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events.Core
{
    public class Selected_Project_ChangedEvent : PubSubEvent<Domain.Core.Project> { }
}
