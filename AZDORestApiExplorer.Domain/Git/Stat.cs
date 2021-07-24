
using System;

namespace AZDORestApiExplorer.Domain.Git
{
    namespace Events
    {

    }
    public class StatsRoot
    {
        public int count { get; set; }
        public Stat[] value { get; set; }
    }

    public class Stat
    {
        public Commit commit { get; set; }
        public string name { get; set; }
        public int aheadCount { get; set; }
        public int behindCount { get; set; }
        public bool isBaseVersion { get; set; }

        public class Commit
        {
            public string commitId { get; set; }
            public Author author { get; set; }
            public Committer committer { get; set; }
            public string comment { get; set; }
            public string url { get; set; }
            public string treeId { get; set; }
            public string[] parents { get; set; }
            public bool commentTruncated { get; set; }
        }

        public class Author
        {
            public string name { get; set; }
            public string email { get; set; }
            public DateTime date { get; set; }
        }

        public class Committer
        {
            public string name { get; set; }
            public string email { get; set; }
            public DateTime date { get; set; }
        }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Stat

}
