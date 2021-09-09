using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    namespace Events
    {
        public class GetStatesWITEvent : PubSubEvent<GetStatesWITEventArgs> { }

        public class GetStatesWITEventArgs
        {
            public Domain.Organization Organization;

            public Domain.Core.Project Project;

            public Domain.WorkItemTracking.WorkItemType WorkItemType;
        }

        public class SelectedStateWITChangedEvent : PubSubEvent<State> { }
    }

    public class StatesRoot
    {
        public int count { get; set; }
        public State[] value { get; set; }
    }

    public class State
    {
        public string name { get; set; }
        public string color { get; set; }
        public string category { get; set; }

        public RESTResult<State> Results { get; set; } = new RESTResult<State>();
    }
}
