namespace AZDORestApiExplorer.Domain
{

    public class AvailableCollection
    {
        public string Name { get; set; }
        public Organization Organization { get; set; }
    }

    public class Organization
    {
        public string Uri { get; set; }
        public string PAT { get; set; }
    }
}
