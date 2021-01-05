namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
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
    }
}
