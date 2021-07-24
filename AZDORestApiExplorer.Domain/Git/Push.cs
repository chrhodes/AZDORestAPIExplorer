
using System;

namespace AZDORestApiExplorer.Domain.Git
{
    namespace Events
    {

    }
    public class PushesRoot
    {
        public int count { get; set; }
        public Push[] value { get; set; }
    }

    public class Push
    {
        public Repository repository { get; set; }
        public Pushedby pushedBy { get; set; }
        public int pushId { get; set; }
        public DateTime date { get; set; }
        public string url { get; set; }

        public class Pushedby
        {
            public string displayName { get; set; }
            public string url { get; set; }
            public _Links _links { get; set; }
            public string id { get; set; }
            public string uniqueName { get; set; }
            public string imageUrl { get; set; }
            public string descriptor { get; set; }
        }

        public class _Links
        {
            public Avatar avatar { get; set; }
        }

        public class Avatar
        {
            public string href { get; set; }
        }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Push

}
