namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    namespace Events
    {

    }
    public class WorkItemIconsRoot
    {
        public int count { get; set; }
        public WorkItemIcon[] value { get; set; }
    }

    public class WorkItemIcon
    {
        public string id { get; set; }
        public string url { get; set; }
    }
}
