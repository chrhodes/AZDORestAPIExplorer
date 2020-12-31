namespace AZDORestApiExplorer.Domain
{

    public class AvailableCollection
    {
        public string Name { get; set; }
        public CollectionDetails Details { get; set; }
    }

    public class CollectionDetails
    {
        public string Uri { get; set; }
        public string PAT { get; set; }
    }
}
