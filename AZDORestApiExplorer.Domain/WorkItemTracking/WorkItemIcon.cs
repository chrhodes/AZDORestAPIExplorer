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
        public class GetWorkItemIconsEvent : PubSubEvent<GetWorkItemIconsEventArgs> { }

        public class GetWorkItemIconsEventArgs
        {
            public Organization Organization;

            // public Process Process;

            // public WorkItemType WorkItemType;
        }

        public class SelectedWorkItemIconChangedEvent : PubSubEvent<WorkItemIcon> { }
    }

    public class WorkItemIconsRoot
    {
        public int count { get; set; }
        public WorkItemIcon[] value { get; set; }
    }

    public class WorkItemIcon
    {
        public string id { get; set; }
        public string url { get; set; }

        public RESTResult<WorkItemIcon> Results { get; set; } = new RESTResult<WorkItemIcon>();
    }
}
