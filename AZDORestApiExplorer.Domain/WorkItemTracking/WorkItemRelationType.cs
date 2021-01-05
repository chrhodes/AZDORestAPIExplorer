namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    public class WorkItemRelationTypesRoot
    {
        public int count { get; set; }
        public WorkItemRelationType[] value { get; set; }
    }
    public class WorkItemRelationType
    {
        public Attributes attributes { get; set; }
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        public class Attributes
        {
            public string usage { get; set; }
            public bool editable { get; set; }
            public bool enabled { get; set; }
            public bool acyclic { get; set; }
            public bool directional { get; set; }
            public bool singleTarget { get; set; }
            public string topology { get; set; }
            public bool remote { get; set; }
            public bool isForward { get; set; }
            public string oppositeEndReferenceName { get; set; }
        }
    }
}
