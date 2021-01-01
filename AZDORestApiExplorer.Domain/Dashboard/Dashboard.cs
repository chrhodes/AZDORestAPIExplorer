using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.Domain.Dashboard
{
    public class DashboardsRoot
    {
        public int count { get; set; }
        public Dashboard[] value { get; set; }
    }

    public class Dashboard
    {
        public string id { get; set; }
        public string name { get; set; }
        public int refreshInterval { get; set; }
        public int position { get; set; }
        public string groupId { get; set; }
        public string url { get; set; }
        public _Links _links { get; set; }

        public class _Links
        {
            public Self self { get; set; }
            public Group group { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Group
        {
            public string href { get; set; }
        }
    }
}
