
using System;

using AZDORestApiExplorer.Domain.Core;

namespace AZDORestApiExplorer.Domain.Test
{
    public class TestCasesRoot
    {
        public int count { get; set; }
        public TestCase[] value { get; set; }
    }

    public class TestCase
    {
        public TestPlan testPlan { get; set; }
        public Project project { get; set; }
        public TestSuite testSuite { get; set; }
        public Workitem workItem { get; set; }
        public Pointassignment[] pointAssignments { get; set; }
        public Links links { get; set; }
        public int order { get; set; }



        public class Workitem
        {
            public int id { get; set; }
            public string name { get; set; }
            public Workitemfield[] workItemFields { get; set; }
        }

        public class Workitemfield
        {
            public string MicrosoftVSTSTCMSteps { get; set; }
            public string MicrosoftVSTSCommonActivatedBy { get; set; }
            public DateTime MicrosoftVSTSCommonActivatedDate { get; set; }
            public string MicrosoftVSTSTCMAutomationStatus { get; set; }
            public string SystemDescription { get; set; }
            public string SystemState { get; set; }
            public string SystemAssignedTo { get; set; }
            public int MicrosoftVSTSCommonPriority { get; set; }
            public DateTime MicrosoftVSTSCommonStateChangeDate { get; set; }
            public string SystemWorkItemType { get; set; }
            public int SystemRev { get; set; }
        }

        public class Links
        {
            public object testPoints { get; set; }
            public object configuration { get; set; }
            public _Self _self { get; set; }
            public Sourceplan sourcePlan { get; set; }
            public Sourcesuite sourceSuite { get; set; }
            public Sourceproject sourceProject { get; set; }
        }

        public class _Self
        {
            public string href { get; set; }
        }

        public class Sourceplan
        {
            public string href { get; set; }
        }

        public class Sourcesuite
        {
            public string href { get; set; }
        }

        public class Sourceproject
        {
            public string href { get; set; }
        }

        public class Pointassignment
        {
            public int id { get; set; }
            public string configurationName { get; set; }
            public Tester tester { get; set; }
            public int configurationId { get; set; }
        }

        public class Tester
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

    // Nest any additional classes inside class TestCase


}
