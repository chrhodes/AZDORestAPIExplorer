using AZDORestApiExplorer.Domain;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events.Git
{
    public class GetProjectRepositoriesEvent : PubSubEvent<GetProjectRepositoriesEventArgs> { }
}