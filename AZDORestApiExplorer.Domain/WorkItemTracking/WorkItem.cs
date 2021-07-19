using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.WorkItemTracking.Events;

using Newtonsoft.Json;

using Prism.Events;

namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    namespace Events
    {
        public class GetWorkItemEvent : PubSubEvent<GetWorkItemEventArgs> { }

        public class GetWorkItemEventArgs
        {
            public Organization Organization;

            public int Id;

            // public Domain.Core.Process Process;

            public Domain.Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedWorkItemChangedEvent : PubSubEvent<WorkItem> { }
    }

    public class WorkItemsRoot
    {
        public int count { get; set; }
        public WorkItem[] value { get; set; }
    }

    public class WorkItem
    {

    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class WorkItem


    public class Rootobject
    {
        public int id { get; set; }
        public int rev { get; set; }
        public Fields fields { get; set; }
        public _Links5 _links { get; set; }
        public string url { get; set; }
    }

    public class Fields
    {
        public string SystemAreaPath { get; set; }
        public string SystemTeamProject { get; set; }
        public string SystemIterationPath { get; set; }
        public string SystemWorkItemType { get; set; }
        public string SystemState { get; set; }
        public string SystemReason { get; set; }
        public SystemAssignedto SystemAssignedTo { get; set; }
        public DateTime SystemCreatedDate { get; set; }
        public SystemCreatedby SystemCreatedBy { get; set; }
        public DateTime SystemChangedDate { get; set; }
        public SystemChangedby SystemChangedBy { get; set; }
        public int SystemCommentCount { get; set; }
        public string SystemTitle { get; set; }
        public string SystemBoardColumn { get; set; }
        public bool SystemBoardColumnDone { get; set; }
        public string MicrosoftVSTSCommonIssue { get; set; }
        public DateTime MicrosoftVSTSCommonStateChangeDate { get; set; }
        public DateTime MicrosoftVSTSCommonActivatedDate { get; set; }
        public MicrosoftVSTSCommonActivatedby MicrosoftVSTSCommonActivatedBy { get; set; }
        public DateTime MicrosoftVSTSCommonClosedDate { get; set; }
        public MicrosoftVSTSCommonClosedby MicrosoftVSTSCommonClosedBy { get; set; }
        public int MicrosoftVSTSCommonPriority { get; set; }
        public string MicrosoftVSTSCommonTriage { get; set; }
        public string MicrosoftVSTSCommonExitCriteria { get; set; }
        public string MicrosoftVSTSCommonSeverity { get; set; }
        public string MicrosoftVSTSCMMIBlocked { get; set; }
        public string MicrosoftVSTSCMMIRequiresReview { get; set; }
        public string MicrosoftVSTSCMMIRequiresTest { get; set; }
        public string DevCustomWitVersion { get; set; }
        public string DevCustomIsMilestone { get; set; }
        public float MicrosoftVSTSCommonStackRank { get; set; }
        public float MicrosoftVSTSSchedulingSize { get; set; }
        public string WEF_C2C0461D0DDD48DC9681522EE9E34763_KanbanColumn { get; set; }
        public string CFVSTSHasRequirements { get; set; }
        public bool WEF_C2C0461D0DDD48DC9681522EE9E34763_KanbanColumnDone { get; set; }
        public string MicrosoftVSTSCommonValueArea { get; set; }
        public string CustomUserStoryReason { get; set; }
        public string WEF_63985E4FD50B481A8B74B232BAE9BED1_KanbanColumn { get; set; }
        public bool WEF_63985E4FD50B481A8B74B232BAE9BED1_KanbanColumnDone { get; set; }
        public string SystemDescription { get; set; }
    }

    public class SystemAssignedto
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

    public class SystemCreatedby
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links1 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links1
    {
        public Avatar1 avatar { get; set; }
    }

    public class Avatar1
    {
        public string href { get; set; }
    }

    public class SystemChangedby
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links2 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links2
    {
        public Avatar2 avatar { get; set; }
    }

    public class Avatar2
    {
        public string href { get; set; }
    }

    public class MicrosoftVSTSCommonActivatedby
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links3 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links3
    {
        public Avatar3 avatar { get; set; }
    }

    public class Avatar3
    {
        public string href { get; set; }
    }

    public class MicrosoftVSTSCommonClosedby
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links4 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links4
    {
        public Avatar4 avatar { get; set; }
    }

    public class Avatar4
    {
        public string href { get; set; }
    }

    public class _Links5
    {
        public Self self { get; set; }
        public Workitemupdates workItemUpdates { get; set; }
        public Workitemrevisions workItemRevisions { get; set; }
        public Workitemcomments workItemComments { get; set; }
        public Html html { get; set; }
        public Workitemtype workItemType { get; set; }
        public Fields1 fields { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Workitemupdates
    {
        public string href { get; set; }
    }

    public class Workitemrevisions
    {
        public string href { get; set; }
    }

    public class Workitemcomments
    {
        public string href { get; set; }
    }

    public class Html
    {
        public string href { get; set; }
    }

    public class Workitemtype
    {
        public string href { get; set; }
    }

    public class Fields1
    {
        public string href { get; set; }
    }


}
