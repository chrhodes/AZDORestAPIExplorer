namespace AZDORestApiExplorer.Domain.WorkItemTrackingProcess
{
    namespace Events
    {

    }
    public class BehaviorsRoot
    {
        public int count { get; set; }
        public Behavior[] value { get; set; }
    }

    public class Behavior
    {
        public string id { get; set; }
        public string description { get; set; }
        public bool _abstract { get; set; }
        public bool overriden { get; set; }
        public bool custom { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public object icon { get; set; }
        public Field[] fields { get; set; }
        public string inherits { get; set; }
        public int rank { get; set; }

        public class Field
        {
            public string behaviorFieldId { get; set; }
            public string id { get; set; }
            public string name { get; set; }
        }
    }
}
