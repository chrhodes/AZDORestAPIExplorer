namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    public class WorkItemTypeCategoriesRoot
    {
        public int count { get; set; }
        public WorkItemTypeCategory[] value { get; set; }
    }

    public class WorkItemTypeCategory
    {
        public string name { get; set; }
        public string referenceName { get; set; }
        public Defaultworkitemtype defaultWorkItemType { get; set; }
        public Workitemtype[] workItemTypes { get; set; }
        public string url { get; set; }

        public class Defaultworkitemtype
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Workitemtype
        {
            public string name { get; set; }
            public string url { get; set; }
        }
    }
}
