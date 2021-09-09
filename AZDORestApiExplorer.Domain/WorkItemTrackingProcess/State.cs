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

namespace AZDORestApiExplorer.Domain.WorkItemTrackingProcess
{
    namespace Events
    {
        public class GetStatesWITPEvent : PubSubEvent<GetStatesWITPEventArgs> { }

        public class GetStatesWITPEventArgs
        {
            public Domain.Organization Organization;

            public Domain.Core.Process Process;

            public Domain.WorkItemTrackingProcess.WorkItemType WorkItemType;
        }

        public class SelectedStateWITPChangedEvent : PubSubEvent<State> { }
    }

    public class StatesRoot
    {
        public int count { get; set; }
        public State[] value { get; set; }
    }

    public class State
    {
        public string id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string stateCategory { get; set; }
        public int order { get; set; }
        public string url { get; set; }
        public string customizationType { get; set; }
    }
}
