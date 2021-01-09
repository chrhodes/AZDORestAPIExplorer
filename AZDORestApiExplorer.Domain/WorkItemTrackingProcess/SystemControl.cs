namespace AZDORestApiExplorer.Domain.WorkItemTrackingProcess
{
    public class SystemControlsRoot
    {
        public int count { get; set; }
        public SystemControl[] value { get; set; }
    }

    public class SystemControl
    {
        public string id { get; set; }
        public string label { get; set; }
        public string controlType { get; set; }
        public bool readOnly { get; set; }
        public bool visible { get; set; }
        public bool isContribution { get; set; }
    }

}
