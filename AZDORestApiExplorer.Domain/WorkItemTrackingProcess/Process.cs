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
        public class GetProcessesWITPEvent : PubSubEvent<GetProcessesWITPEventArgs> { }

        public class GetProcessesWITPEventArgs
        {
            public Domain.Organization Organization;
        }

        public class SelectedProcessWITPChangedEvent : PubSubEvent<Process> { }
    }

    public class ProcessesRoot
    {
        public int count { get; set; }
        public Process[] value { get; set; }
    }

    public class Process
    {
        public string typeId { get; set; }
        public string name { get; set; }
        public string referenceName { get; set; }
        public string description { get; set; }
        public string parentProcessTypeId { get; set; }
        public bool isEnabled { get; set; }
        public bool isDefault { get; set; }
        public string customizationType { get; set; }
    }

}
