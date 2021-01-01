namespace AZDORestApiExplorer.Domain.WorkItemTrackingProcess
{
    public class StatesRoot
    {
        public int count { get; set; }
        public State[] value { get; set; }
    }

    public class State
    {
        public string id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string stateCategory { get; set; }
        public int order { get; set; }
        public string url { get; set; }
        public string customizationType { get; set; }
    }
}
