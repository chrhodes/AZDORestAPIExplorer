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
        public class GetListsEvent : PubSubEvent<GetListsEventArgs> { }

        public class GetListsEventArgs
        {
            public Domain.Organization Organization;
        }

        public class SelectedListChangedEvent : PubSubEvent<List> { }
    }

    public class ListsRoot
    {
        public int count { get; set; }
        public List[] value { get; set; }
    }

    public class List
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public bool isSuggested { get; set; }
        public string url { get; set; }
    }
}
