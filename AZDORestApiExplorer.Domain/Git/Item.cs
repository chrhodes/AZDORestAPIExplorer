
namespace AZDORestApiExplorer.Domain.Git
{
    namespace Events
    {

    }
    public class ItemsRoot
    {
        public int count { get; set; }
        public Item[] value { get; set; }
    }

    public class Item
    {
        public string objectId { get; set; }
        public string gitObjectType { get; set; }
        public string commitId { get; set; }
        public string path { get; set; }
        public bool isFolder { get; set; }
        public string url { get; set; }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Item

}
