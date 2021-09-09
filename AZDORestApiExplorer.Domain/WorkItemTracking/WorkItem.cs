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

    public class WorkItem
    {

    }

    public class WorkItemsRoot
    {
        public int id { get; set; }
        public int rev { get; set; }
        public Fields fields { get; set; }
        public _Links5 _links { get; set; }
        public string url { get; set; }


    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class WorkItem


    public class Fields
    {
        [JsonProperty("System.AreaPath")]
        public string SystemAreaPath { get; set; }

        [JsonProperty("System.TeamProject")]
        public string SystemTeamProject { get; set; }

        [JsonProperty("System.IterationPath")]
        public string SystemIterationPath { get; set; }

        [JsonProperty("System.WorkItemType")]
        public string SystemWorkItemType { get; set; }

        [JsonProperty("System.State")]
        public string SystemState { get; set; }

        [JsonProperty("System.Reason")]
        public string SystemReason { get; set; }

            [JsonProperty("System.AssignedTo")]
            public SystemAssignedto SystemAssignedTo { get; set; }

            [JsonProperty("System.CreatedDate")]
            public DateTime SystemCreatedDate { get; set; }

            [JsonProperty("System.CreatedBy")]
            public SystemCreatedby SystemCreatedBy { get; set; }

            [JsonProperty("System.ChangedDate")]
            public DateTime SystemChangedDate { get; set; }

            [JsonProperty("System.ChangedBy")]
            public SystemChangedby SystemChangedBy { get; set; }

            [JsonProperty("System.CommentCount")]
            public int SystemCommentCount { get; set; }

            [JsonProperty("System.Title")]
            public string SystemTitle { get; set; }

            [JsonProperty("System.BoardColumn")]
            public string SystemBoardColumn { get; set; }

            [JsonProperty("System.BoardColumnDone")]
            public bool SystemBoardColumnDone { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.Issue")]
            public string MicrosoftVSTSCommonIssue { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.StateChangedDate")]
            public DateTime MicrosoftVSTSCommonStateChangeDate { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.ActivatedDate")]
            public DateTime MicrosoftVSTSCommonActivatedDate { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.ActivatedBy")]
            public MicrosoftVSTSCommonActivatedby MicrosoftVSTSCommonActivatedBy { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.ClosedDate")]
            public DateTime MicrosoftVSTSCommonClosedDate { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.ClosedBy")]
            public MicrosoftVSTSCommonClosedby MicrosoftVSTSCommonClosedBy { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.Priority")]
            public int MicrosoftVSTSCommonPriority { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.Triage")]
            public string MicrosoftVSTSCommonTriage { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.ExitCriteria")]
            public string MicrosoftVSTSCommonExitCriteria { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.Severity")]
            public string MicrosoftVSTSCommonSeverity { get; set; }

            [JsonProperty("Microsoft.VSTS.CMMI.Blocked")]
            public string MicrosoftVSTSCMMIBlocked { get; set; }

            [JsonProperty("Microsoft.VSTS.CMMI.RequiresReview")]
            public string MicrosoftVSTSCMMIRequiresReview { get; set; }

            [JsonProperty("Microsoft.VSTS.CMMI.RequiresTest")]
            public string MicrosoftVSTSCMMIRequiresTest { get; set; }

            [JsonProperty("DevCustom.WitVersion")]
            public string DevCustomWitVersion { get; set; }

            [JsonProperty("DevCustom.IsMilestone")]
            public string DevCustomIsMilestone { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.StackRank")]
            public float MicrosoftVSTSCommonStackRank { get; set; }

            [JsonProperty("Microsoft.VSTS.Scheduling.Size")]
            public float MicrosoftVSTSSchedulingSize { get; set; }

            [JsonProperty("WEF_C2C0461D0DDD48DC9681522EE9E34763_Kanban.Column")]
            public string WEF_C2C0461D0DDD48DC9681522EE9E34763_KanbanColumn { get; set; }

            [JsonProperty("CF.VSTS.HasRequirements")]
            public string CFVSTSHasRequirements { get; set; }

            [JsonProperty("WEF_C2C0461D0DDD48DC9681522EE9E34763_Kanban.Column.Done")]
            public bool WEF_C2C0461D0DDD48DC9681522EE9E34763_KanbanColumnDone { get; set; }

            [JsonProperty("Microsoft.VSTS.Common.ValueArea")]
            public string MicrosoftVSTSCommonValueArea { get; set; }

            [JsonProperty("Custom.UserStoryReason")]
            public string CustomUserStoryReason { get; set; }

            [JsonProperty("WEF_63985E4FD50B481A8B74B232BAE9BED1_Kanban.Column")]
            public string WEF_63985E4FD50B481A8B74B232BAE9BED1_KanbanColumn { get; set; }

            [JsonProperty("WEF_63985E4FD50B481A8B74B232BAE9BED1_Kanban.Column.Done")]
            public bool WEF_63985E4FD50B481A8B74B232BAE9BED1_KanbanColumnDone { get; set; }

            [JsonProperty("System.Description")]
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
}
