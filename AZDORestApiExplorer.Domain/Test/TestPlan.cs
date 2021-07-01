
using AZDORestApiExplorer.Domain.Core;

namespace AZDORestApiExplorer.Domain.Test
{
    public class TestPlansRoot
    {
        public int count { get; set; }
        public TestPlan[] value { get; set; }
    }

    public class TestPlan
    {
        public int id { get; set; }
        public Project project { get; set; }
        public Rootsuite rootSuite { get; set; }
        public _Links _links { get; set; }
        public int revision { get; set; }
        public string name { get; set; }
        public string areaPath { get; set; }
        public string iteration { get; set; }
        public object owner { get; set; }
        public string state { get; set; }
        public int buildId { get; set; }

        public class Rootsuite
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class _Links
        {
            public _Self _self { get; set; }
            public Clienturl clientUrl { get; set; }
            public Rootsuite1 rootSuite { get; set; }
            public Build build { get; set; }
        }

        public class _Self
        {
            public string href { get; set; }
        }

        public class Clienturl
        {
            public string href { get; set; }
        }

        public class Rootsuite1
        {
            public string href { get; set; }
        }

        public class Build
        {
            public string href { get; set; }
        }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class TestPlan

}
