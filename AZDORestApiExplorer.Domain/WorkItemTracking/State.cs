
namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    public class StatesRoot
    {
        public int count { get; set; }
        public State[] value { get; set; }
    }

    public class State
    {
        public string name { get; set; }
        public string color { get; set; }
        public string category { get; set; }
    }
}
