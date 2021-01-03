namespace AZDORestApiExplorer.Domain.Core
{
    public class ProcessesRoot
    {
        public int count { get; set; }
        public Process[] value { get; set; }
    }

    public class Process
    {
        public string id { get; set; }
        public string description { get; set; }
        public bool isDefault { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string name { get; set; }
    }
}
