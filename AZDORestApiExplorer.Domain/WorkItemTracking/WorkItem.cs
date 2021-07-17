using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.WorkItemTracking.Events;

using Newtonsoft.Json;

using Prism.Events;

namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    namespace Events
    {
        public class GetWorkItemsEvent : PubSubEvent<GetWorkItemsEventArgs> { }

        public class GetWorkItemsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            // public Domain.Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedWorkItemChangedEvent : PubSubEvent<WorkItem> { }
    }

    public class WorkItemsRoot
    {
        public int count { get; set; }
        public WorkItem[] value { get; set; }
    }

    public class WorkItem
    {

    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class WorkItem

}
