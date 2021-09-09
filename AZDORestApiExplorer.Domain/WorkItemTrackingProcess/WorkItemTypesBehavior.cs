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
        public class GetWorkItemTypesBehaviorsEvent : PubSubEvent<GetWorkItemTypesBehaviorsEventArgs> { }

        public class GetWorkItemTypesBehaviorsEventArgs
        {
            public Domain.Organization Organization;

            public Domain.Core.Process Process;

            public Domain.WorkItemTrackingProcess.WorkItemType WorkItemType;
        }

        public class SelectedWorkItemTypesBehaviorChangedEvent : PubSubEvent<WorkItemTypesBehavior> { }
    }

    public class WorkItemTypesBehaviorsRoot
    {
        public int count { get; set; }
        public WorkItemTypesBehavior[] value { get; set; }
    }

    public class WorkItemTypesBehavior
    {
    }
    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Rename classes to use TYPEsRoot and TYPE classes

}
