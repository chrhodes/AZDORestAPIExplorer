using AZDORestApiExplorer.Domain.Dashboard;

using Prism.Events;

using VNC.Core.Events;

namespace AZDORestApiExplorer.Core.Events.Dashboard
{
    public class SelectedDashboardChangedEvent : PubSubEvent<Domain.Dashboard.Dashboard> { }
}