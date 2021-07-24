namespace AZDORestApiExplorer.Domain.WorkItemTrackingProcess
{
    namespace Events
    {

    }
    public class ProcessesRoot
    {
        public int count { get; set; }
        public Process[] value { get; set; }
    }

    public class Process
    {
        public string typeId { get; set; }
        public string name { get; set; }
        public string referenceName { get; set; }
        public string description { get; set; }
        public string parentProcessTypeId { get; set; }
        public bool isEnabled { get; set; }
        public bool isDefault { get; set; }
        public string customizationType { get; set; }
    }

}
