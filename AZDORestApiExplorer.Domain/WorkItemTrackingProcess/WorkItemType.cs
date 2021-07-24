namespace AZDORestApiExplorer.Domain.WorkItemTrackingProcess
{
    namespace Events
    {

    }
    public class WorkItemTypesRoot
    {
        public int count { get; set; }
        public WorkItemType[] value { get; set; }
    }

    public class WorkItemType
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string customization { get; set; }
        public string color { get; set; }
        public string icon { get; set; }
        public bool isDisabled { get; set; }
        public object inherits { get; set; }
    }
}
