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
        public class GetWorkItemTypesFieldsWITEvent : PubSubEvent<GetWorkItemTypesFieldsWITEventArgs> { }

        public class GetWorkItemTypesFieldsWITEventArgs
        {
            public Domain.Organization Organization;

            public Domain.Core.Project Project;

            public Domain.WorkItemTracking.WorkItemType WorkItemType;

            string QueryString;
        }

        public class SelectedWorkItemTypesFieldChangedEvent : PubSubEvent<WorkItemTypesField> { }
    }

    public class WorkItemTypesFieldsRoot
    {
        public int count { get; set; }
        public WorkItemTypesField[] value { get; set; }
    }

    public class WorkItemTypesField
    {
        public string defaultValue { get; set; }
        public string[] allowedValues { get; set; }
        public string helpText { get; set; }
        public bool alwaysRequired { get; set; }
        public Dependentfield[] dependentFields { get; set; }
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        public class Dependentfield
        {
            public string referenceName { get; set; }
            public string name { get; set; }
            public string url { get; set; }
        }

        public RESTResult<WorkItemTypesField> Results { get; set; } = new RESTResult<WorkItemTypesField>();
    }
}
