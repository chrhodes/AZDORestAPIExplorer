using AZDORestApiExplorer.Domain.Accounts;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events.Accounts
{
    public class SelectedAccountChangedEvent : PubSubEvent<Account> { }
}