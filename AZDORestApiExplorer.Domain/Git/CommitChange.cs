using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Git.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Git
{
    namespace Events
    {
        public class GetCommitChangesEvent : PubSubEvent<GetCommitChangesEventArgs> { }

        public class GetCommitChangesEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Domain.Core.Project Project;

            public Domain.Git.Repository Repository;

            public Domain.Git.Commit Commit;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedCommitChangeChangedEvent : PubSubEvent<Change> { }
    }

    public class CommitChangesRoot
    {
        public Changecounts changeCounts { get; set; }
        public Change[] changes { get; set; }
    }

    public class Changecounts
    {
        public int Edit { get; set; }
        public int Add { get; set; }
    }

    public class Change
    {
        public Item item { get; set; }
        public string changeType { get; set; }

        public class Item
        {
            public string objectId { get; set; }
            public string originalObjectId { get; set; }
            public string gitObjectType { get; set; }
            public string commitId { get; set; }
            public string path { get; set; }
            public bool isFolder { get; set; }
            public string url { get; set; }
        }
    }

}
