using System;

namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    namespace Events
    {

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
    }

}
