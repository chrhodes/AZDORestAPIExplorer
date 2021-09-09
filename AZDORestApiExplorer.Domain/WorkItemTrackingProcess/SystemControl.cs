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
        public class GetSystemControlsEvent : PubSubEvent<GetSystemControlsEventArgs> { }

        public class GetSystemControlsEventArgs
        {
            public Domain.Organization Organization;

            public Domain.Core.Process Process;

            public Domain.WorkItemTrackingProcess.WorkItemType WorkItemType;
        }

        public class SelectedSystemControlChangedEvent : PubSubEvent<SystemControl> { }
    }

    public class SystemControlsRoot
    {
        public int count { get; set; }
        public SystemControl[] value { get; set; }
    }

    public class SystemControl
    {
        public string id { get; set; }
        public string label { get; set; }
        public string controlType { get; set; }
        public bool readOnly { get; set; }
        public bool visible { get; set; }
        public bool isContribution { get; set; }
    }

}
