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
        public class GetWorkItemTypesWITPEvent : PubSubEvent<GetWorkItemTypesWITPEventArgs> { }

        public class GetWorkItemTypesWITPEventArgs
        {
            public Organization Organization;

            public Domain.Core.Process Process;
        }

        public class SelectedWorkItemTypeWITPChangedEvent : PubSubEvent<WorkItemType> { }
    }

    public class WorkItemTypesRoot
    {
        public int count { get; set; }
        public WorkItemType[] value { get; set; }
    }

    public class WorkItemType
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string customization { get; set; }
        public string color { get; set; }
        public string icon { get; set; }
        public bool isDisabled { get; set; }
        public object inherits { get; set; }
    }
}
