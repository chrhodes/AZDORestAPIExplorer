
using System;

using AZDORestApiExplorer.Domain.Core;

namespace AZDORestApiExplorer.Domain.Test
{
    public class TestSuitesRoot
    {
        public int count { get; set; }
        public TestSuite[] value { get; set; }
    }

    public class TestSuite
    {
        public int id { get; set; }
        public int revision { get; set; }
        public Project project { get; set; }
        public Lastupdatedby lastUpdatedBy { get; set; }
        public DateTime lastUpdatedDate { get; set; }
        public Plan plan { get; set; }
        public _Links1 _links { get; set; }
        public string suiteType { get; set; }
        public string name { get; set; }
        public bool inheritDefaultConfigurations { get; set; }
        public Defaultconfiguration[] defaultConfigurations { get; set; }

        public class Lastupdatedby
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

        public class Plan
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class _Links1
        {
            public _Self _self { get; set; }
            public Testcases testCases { get; set; }
            public Testpoints testPoints { get; set; }
        }

        public class _Self
        {
            public string href { get; set; }
        }

        public class Testcases
        {
            public string href { get; set; }
        }

        public class Testpoints
        {
            public string href { get; set; }
        }

        public class Defaultconfiguration
        {
            public int id { get; set; }
            public string name { get; set; }
        }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class TestSuite

}
