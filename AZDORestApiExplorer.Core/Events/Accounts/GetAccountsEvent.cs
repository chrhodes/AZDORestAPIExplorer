using AZDORestApiExplorer.Domain;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events.Accounts
{
    public class GetAccountsEvent : PubSubEvent<GetAccountsEventArgs> { }
}