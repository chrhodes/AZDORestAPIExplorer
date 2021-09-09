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
        public class GetClassificationNodesEvent : PubSubEvent<GetClassificationNodesEventArgs> { }

        public class GetClassificationNodesEventArgs
        {
            public Organization Organization;

            // public Process Process;

            public Domain.Core.Project Project;

            // public Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedClassificationNodeChangedEvent : PubSubEvent<ClassificationNode> { }
    }

    public class ClassificationNodesRoot
    {
        public int count { get; set; }
        public ClassificationNode[] value { get; set; }
    }

    public class ClassificationNode
    {
        public int id { get; set; }
        public string identifier { get; set; }
        public string name { get; set; }
        public string structureType { get; set; }
        public bool hasChildren { get; set; }
        public string path { get; set; }
        public string url { get; set; }
        public Attributes attributes { get; set; }

        public class Attributes
        {
            public DateTime startDate { get; set; }
            public DateTime finishDate { get; set; }
        }

        public RESTResult<ClassificationNode> Results { get; set; } = new RESTResult<ClassificationNode>();
    }

}
