using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    class WorkItemRevision
    {
    }


    public class WorkItemRevisions
    {
        public int count { get; set; }
        public Value[] value { get; set; }
    }

    public class Value
    {
        public int id { get; set; }
        public int rev { get; set; }
        public Fields fields { get; set; }
        public string url { get; set; }
        public Commentversionref commentVersionRef { get; set; }
    }

    public class Fields
    {
        public string SystemWorkItemType { get; set; }
        public string SystemState { get; set; }
        public string SystemReason { get; set; }
        public SystemAssignedto SystemAssignedTo { get; set; }
        public DateTime SystemCreatedDate { get; set; }
        public SystemCreatedby SystemCreatedBy { get; set; }
        public DateTime SystemChangedDate { get; set; }
        public SystemChangedby SystemChangedBy { get; set; }
        public int SystemCommentCount { get; set; }
        public string SystemTeamProject { get; set; }
        public string SystemAreaPath { get; set; }
        public string SystemIterationPath { get; set; }
        public string SystemTitle { get; set; }
        public string MicrosoftVSTSCommonIssue { get; set; }
        public int MicrosoftVSTSCommonPriority { get; set; }
        public string MicrosoftVSTSCommonTriage { get; set; }
        public string MicrosoftVSTSCommonExitCriteria { get; set; }
        public string MicrosoftVSTSCMMIBlocked { get; set; }
        public string MicrosoftVSTSCMMIRequiresReview { get; set; }
        public string MicrosoftVSTSCMMIRequiresTest { get; set; }
        public string CFVSTSHasRequirements { get; set; }
        public bool WEF_C2C0461D0DDD48DC9681522EE9E34763_KanbanColumnDone { get; set; }
        public string SystemDescription { get; set; }
        public string WEF_C2C0461D0DDD48DC9681522EE9E34763_KanbanColumn { get; set; }
        public DateTime MicrosoftVSTSCommonStateChangeDate { get; set; }
        public string MicrosoftVSTSCommonSeverity { get; set; }
        public string SystemBoardColumn { get; set; }
        public bool SystemBoardColumnDone { get; set; }
        public string DevCustomWitVersion { get; set; }
        public bool WEF_63985E4FD50B481A8B74B232BAE9BED1_KanbanColumnDone { get; set; }
        public string WEF_63985E4FD50B481A8B74B232BAE9BED1_KanbanColumn { get; set; }
        public string SystemHistory { get; set; }
        public float MicrosoftVSTSCommonStackRank { get; set; }
        public string DevCustomIsMilestone { get; set; }
        public string MicrosoftVSTSCommonValueArea { get; set; }
        public DateTime MicrosoftVSTSCommonActivatedDate { get; set; }
        public MicrosoftVSTSCommonActivatedby MicrosoftVSTSCommonActivatedBy { get; set; }
        public string CustomTaskReason { get; set; }
        public DateTime MicrosoftVSTSCommonResolvedDate { get; set; }
        public MicrosoftVSTSCommonResolvedby MicrosoftVSTSCommonResolvedBy { get; set; }
        public float MicrosoftVSTSSchedulingSize { get; set; }
        public string CustomUserStoryReason { get; set; }
        public DateTime MicrosoftVSTSCommonClosedDate { get; set; }
        public MicrosoftVSTSCommonClosedby MicrosoftVSTSCommonClosedBy { get; set; }
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

    public class MicrosoftVSTSCommonResolvedby
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

    public class MicrosoftVSTSCommonClosedby
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public _Links5 _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class _Links5
    {
        public Avatar5 avatar { get; set; }
    }

    public class Avatar5
    {
        public string href { get; set; }
    }

    public class Commentversionref
    {
        public int commentId { get; set; }
        public int version { get; set; }
        public string url { get; set; }
    }

}
