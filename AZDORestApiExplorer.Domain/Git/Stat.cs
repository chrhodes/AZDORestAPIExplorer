
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
        public class GetStatsEvent : PubSubEvent<GetStatsEventArgs> { }

        public class GetStatsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Domain.Core.Project Project;

            public Domain.Git.Repository Repository;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedStatChangedEvent : PubSubEvent<Stat> { }
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
    }
}
