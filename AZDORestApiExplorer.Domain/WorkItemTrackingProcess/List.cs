namespace AZDORestApiExplorer.Domain.WorkItemTrackingProcess
{
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
