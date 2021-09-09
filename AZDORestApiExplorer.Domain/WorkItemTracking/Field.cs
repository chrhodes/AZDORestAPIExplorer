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
        public class GetFieldsWITEvent : PubSubEvent<GetFieldsWITEventArgs> { }

        public class GetFieldsWITEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;
            // public Process Process;

            // public WorkItemType WorkItemType;
        }

        public class SelectedFieldWITChangedEvent : PubSubEvent<Field> { }
    }

    public class FieldsRoot
    {
        public int count { get; set; }
        public Field[] value { get; set; }
    }

    public class Field
    {
        public string name { get; set; }
        public string referenceName { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string usage { get; set; }
        public bool readOnly { get; set; }
        public bool canSortBy { get; set; }
        public bool isQueryable { get; set; }
        public Supportedoperation[] supportedOperations { get; set; }
        public bool isIdentity { get; set; }
        public bool isPicklist { get; set; }
        public bool isPicklistSuggested { get; set; }
        public string url { get; set; }

        public class Supportedoperation
        {
            public string referenceName { get; set; }
            public string name { get; set; }
        }

        public RESTResult<Field> Results { get; set; } = new RESTResult<Field>();
    }
}
