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
        public class GetFieldsWITPEvent : PubSubEvent<GetFieldsWITPEventArgs> { }

        public class GetFieldsWITPEventArgs
        {
            public Domain.Organization Organization;

            public Domain.Core.Process Process;

            public Domain.WorkItemTrackingProcess.WorkItemType WorkItemType;
        }

        public class SelectedFieldWITPChangedEvent : PubSubEvent<Field> { }
    }

    public class FieldsRoot
    {
        public int count { get; set; }
        public Field[] value { get; set; }
    }

    public class Field
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string customization { get; set; }
        public string description { get; set; }
        public bool required { get; set; }
        public string defaultValue { get; set; }
    }

    public class FieldDef
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public bool required { get; set; }
        public string defaultValue { get; set; }
        public string url { get; set; }
        public string customization { get; set; }
    }
}
