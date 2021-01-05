using System;

namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    public class QueriesRoot
    {
        public int count { get; set; }
        public Query[] value { get; set; }
    }

    public class Query
    {
        public string id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime lastModifiedDate { get; set; }
        public bool isFolder { get; set; }
        public bool hasChildren { get; set; }
        public bool isPublic { get; set; }
        public _Links _links { get; set; }
        public string url { get; set; }
        public Createdby createdBy { get; set; }
        public Lastmodifiedby lastModifiedBy { get; set; }

        public class _Links
        {
            public Self self { get; set; }
            public Html html { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Html
        {
            public string href { get; set; }
        }

        public class Createdby
        {
            public string id { get; set; }
            public string name { get; set; }
            public string displayName { get; set; }
            public string url { get; set; }
            public _Links1 _links { get; set; }
            public string uniqueName { get; set; }
            public string imageUrl { get; set; }
            public string descriptor { get; set; }
        }

        public class _Links1
        {
            public Avatar avatar { get; set; }
        }

        public class Avatar
        {
            public string href { get; set; }
        }

        public class Lastmodifiedby
        {
            public string id { get; set; }
            public string name { get; set; }
            public string displayName { get; set; }
            public string url { get; set; }
            public _Links2 _links { get; set; }
            public string uniqueName { get; set; }
            public string imageUrl { get; set; }
            public string descriptor { get; set; }
        }

        public class _Links2
        {
            public Avatar1 avatar { get; set; }
        }

        public class Avatar1
        {
            public string href { get; set; }
        }
    }
}
