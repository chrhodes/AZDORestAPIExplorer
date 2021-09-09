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
        public class GetWorkItemTypeCategoriesEvent : PubSubEvent<GetWorkItemTypeCategoriesEventArgs> { }

        public class GetWorkItemTypeCategoriesEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;
        }
        public class SelectedWorkItemTypeCategoryChangedEvent : PubSubEvent<WorkItemTypeCategory> { }
    }

    public class WorkItemTypeCategoriesRoot
    {
        public int count { get; set; }
        public WorkItemTypeCategory[] value { get; set; }
    }

    public class WorkItemTypeCategory
    {
        public string name { get; set; }
        public string referenceName { get; set; }
        public Defaultworkitemtype defaultWorkItemType { get; set; }
        public Workitemtype[] workItemTypes { get; set; }
        public string url { get; set; }

        public class Defaultworkitemtype
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Workitemtype
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public RESTResult<WorkItemTypeCategory> Results { get; set; } = new RESTResult<WorkItemTypeCategory>();
    }
}
