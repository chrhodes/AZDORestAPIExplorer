using AZDORestApiExplorer.Domain.Dashboard;

using Prism.Events;

namespace AZDORestApiExplorer.Core.Events
{
    public class SelectedWidgetChangedEvent : PubSubEvent<Widget> { }
}