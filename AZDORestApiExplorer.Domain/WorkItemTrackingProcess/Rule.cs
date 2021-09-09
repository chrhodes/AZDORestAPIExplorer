using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.WorkItemTrackingProcess
{
    namespace Events
    {
        public class GetRulesEvent : PubSubEvent<GetRulesEventArgs> { }

        public class GetRulesEventArgs
        {
            public Domain.Organization Organization;

            public Domain.Core.Process Process;

            public Domain.WorkItemTrackingProcess.WorkItemType WorkItemType;
        }

        public class SelectedRuleChangedEvent : PubSubEvent<Rule> { }
    }

    public class RulesRoot
    {
        public int count { get; set; }
        public Rule[] value { get; set; }
    }

    public class Rule
    {
        public string id { get; set; }
        public object friendlyName { get; set; }
        public Condition[] conditions { get; set; }
        public Action[] actions { get; set; }
        public bool isDisabled { get; set; }
        public bool isSystem { get; set; }

        public class Condition
        {
            public string conditionType { get; set; }
            public string field { get; set; }
            public string value { get; set; }
        }

        public class Action
        {
            public string actionType { get; set; }
            public string targetField { get; set; }
            public string value { get; set; }
        }
    }

}
