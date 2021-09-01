using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Git.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Dashboard
{
    namespace Events
    {
        public class GetWidgetsEvent : PubSubEvent<GetWidgetsEventArgs> { }

        public class GetWidgetsEventArgs
        {
            public Organization Organization;

            public Project Project;

            public Team Team;

            public Domain.Dashboard.Dashboard Dashboard;
        }

        public class SelectedWidgetChangedEvent : PubSubEvent<Widget> { }
    }
    public class Widget
    {

    }

    // TODO(crhodes)
    // Add info from Json TYPEsRoot and TYPE classes

}
