
namespace AZDORestApiExplorer.Domain.Git
{
    public class CommitChangesRoot
    {
        public int count { get; set; }
        public CommitChange[] value { get; set; }
    }

    public class CommitChange
    {

    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class CommitChange

}
