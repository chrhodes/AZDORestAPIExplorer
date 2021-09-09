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
        public class GetTagsEvent : PubSubEvent<GetTagsEventArgs> { }

        public class GetTagsEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;

        }
        public class SelectedTagChangedEvent : PubSubEvent<Tag> { }
    }

    public class TagsRoot
    {
        public int count { get; set; }
        public Tag[] value { get; set; }
    }
    public class Tag
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        public RESTResult<Tag> Results { get; set; } = new RESTResult<Tag>();
    }
}
