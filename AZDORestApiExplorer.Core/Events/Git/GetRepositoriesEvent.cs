using AZDORestApiExplorer.Domain;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events.Git
{
    public class GetRepositoriesEvent : PubSubEvent<GetRepositoriesEventArgs> { }

    //public class GetPullRequestsEvent : PubSubEvent { }

}