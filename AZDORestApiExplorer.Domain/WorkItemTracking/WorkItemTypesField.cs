namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    public class WorkItemTypesFieldsRoot
    {
        public int count { get; set; }
        public WorkItemTypesField[] value { get; set; }
    }

    public class WorkItemTypesField
    {
        public string defaultValue { get; set; }
        public string[] allowedValues { get; set; }
        public string helpText { get; set; }
        public bool alwaysRequired { get; set; }
        public Dependentfield[] dependentFields { get; set; }
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        public class Dependentfield
        {
            public string referenceName { get; set; }
            public string name { get; set; }
            public string url { get; set; }
        }
    }
}
