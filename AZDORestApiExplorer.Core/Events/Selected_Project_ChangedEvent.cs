using AZDORestApiExplorer.Domain;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events
{
    public class Selected_Project_ChangedEvent : PubSubEvent<Domain.Project> { }
}
