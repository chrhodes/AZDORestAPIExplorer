namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    namespace Events
    {

    }
    public class WorkItemTypesRoot
    {
        public int count { get; set; }
        public WorkItemType[] value { get; set; }
    }

    public class WorkItemType
    {
        public string name { get; set; }
        public string referenceName { get; set; }
        public string description { get; set; }
        public string color { get; set; }
        public Icon icon { get; set; }
        public bool isDisabled { get; set; }
        public string xmlForm { get; set; }
        public Field[] fields { get; set; }
        public Fieldinstance[] fieldInstances { get; set; }
        //public Transitions transitions { get; set; }
        public object transitions { get; set; }
        public State[] states { get; set; }
        public string url { get; set; }

        public class Icon
        {
            public string id { get; set; }
            public string url { get; set; }
        }

        public class Transitions
        {
            public TeamReview[] TeamReview { get; set; }
            public SDLReview[] SDLReview { get; set; }
            public VerifyFix[] VerifyFix { get; set; }
            public Child[] _ { get; set; }
            public Deferred[] Deferred { get; set; }
            public FixIssue[] FixIssue { get; set; }
            public DeferReview[] DeferReview { get; set; }
            public AdminReview[] AdminReview { get; set; }
            public Closed[] Closed { get; set; }
            public Proposed[] Proposed { get; set; }
            public Active[] Active { get; set; }
            public Resolved[] Resolved { get; set; }
            public Accepted[] Accepted { get; set; }
            public Requested[] Requested { get; set; }
            public Approved[] Approved { get; set; }
            public InReview[] InReview { get; set; }
            public Implement[] Implements { get; set; }
            public OnHold[] OnHold { get; set; }
            public TechReviewed[] TechReviewed { get; set; }
            public Archive[] Archive { get; set; }
            public Ready[] Ready { get; set; }
            public Design[] Design { get; set; }
            public Repair[] Repair { get; set; }
            public Assigned[] Assigned { get; set; }
            public ReviewCompleted[] ReviewCompleted { get; set; }
            public Preliminary[] Preliminary { get; set; }
            public Inactive[] Inactive { get; set; }
            public Completed[] Completed { get; set; }
            public InPlanning[] InPlanning { get; set; }
            public InProgress[] InProgress { get; set; }
        }


        public class TeamReview
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class SDLReview
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class VerifyFix
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class Child
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class Deferred
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class FixIssue
        {
            public string to { get; set; }
            public string[] actions { get; set; }
        }

        public class DeferReview
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class AdminReview
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class Closed
        {
            public string to { get; set; }
            public string[] actions { get; set; }
        }

        public class Proposed
        {
            public string to { get; set; }
            public string[] actions { get; set; }
        }

        public class Active
        {
            public string to { get; set; }
            public string[] actions { get; set; }
        }

        public class Resolved
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class Accepted
        {
            public string to { get; set; }
            public string[] actions { get; set; }
        }

        public class Requested
        {
            public string to { get; set; }
            public string[] actions { get; set; }
        }

        public class Approved
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class InReview
        {
            public string to { get; set; }
            public string[] actions { get; set; }
        }

        public class Implement
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class OnHold
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class TechReviewed
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class Archive
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class Ready
        {
            public string to { get; set; }
            public string[] actions { get; set; }
        }

        public class Design
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class Repair
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class Assigned
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class ReviewCompleted
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class Preliminary
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class Inactive
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class Completed
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class InPlanning
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class InProgress
        {
            public string to { get; set; }
            public object actions { get; set; }
        }

        public class Field
        {
            public string defaultValue { get; set; }
            public string helpText { get; set; }
            public bool alwaysRequired { get; set; }
            public string referenceName { get; set; }
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Fieldinstance
        {
            public string defaultValue { get; set; }
            public string helpText { get; set; }
            public bool alwaysRequired { get; set; }
            public string referenceName { get; set; }
            public string name { get; set; }
            public string url { get; set; }
        }

        public class State
        {
            public string name { get; set; }
            public string color { get; set; }
            public string category { get; set; }
        }

    }
}

// BD-STS-PROD

//public class Rootobject
//{
//    public int count { get; set; }
//    public Value[] value { get; set; }
//}

//public class Value
//{
//    public string name { get; set; }
//    public string referenceName { get; set; }
//    public string description { get; set; }
//    public string color { get; set; }
//    public Icon icon { get; set; }
//    public bool isDisabled { get; set; }
//    public string xmlForm { get; set; }
//    public Field[] fields { get; set; }
//    public Fieldinstance[] fieldInstances { get; set; }
//    public Transitions transitions { get; set; }
//    public State[] states { get; set; }
//    public string url { get; set; }
//}

//public class Icon
//{
//    public string id { get; set; }
//    public string url { get; set; }
//}

//public class Transitions
//{
//    public Closed[] Closed { get; set; }
//    public New[] New { get; set; }
//    public Child[] _ { get; set; }
//    public Deferred[] Deferred { get; set; }
//    public Fix[] Fix { get; set; }
//    public AdminReview[] AdminReview { get; set; }
//    public Verify[] Verify { get; set; }
//    public Requested[] Requested { get; set; }
//    public Accepted[] Accepted { get; set; }
//    public Active[] Active { get; set; }
//    public Resolved[] Resolved { get; set; }
//    public Proposed[] Proposed { get; set; }
//    public Removed[] Removed { get; set; }
//    public Design[] Design { get; set; }
//    public Ready[] Ready { get; set; }
//    public Approved[] Approved { get; set; }
//    public Archive[] Archive { get; set; }
//    public Repair[] Repair { get; set; }
//    public Inactive[] Inactive { get; set; }
//    public Completed[] Completed { get; set; }
//    public InPlanning[] InPlanning { get; set; }
//    public InProgress[] InProgress { get; set; }
//    public InReview[] InReview { get; set; }
//    public NotStarted[] NotStarted { get; set; }
//    public Assigned[] Assigned { get; set; }
//    public ReviewCompleted[] ReviewCompleted { get; set; }
//    public Implement[] Implements { get; set; }
//    public Preliminary[] Preliminary { get; set; }
//    public OnHold[] OnHold { get; set; }
//}

//public class Closed
//{
//    public string to { get; set; }
//    public string[] actions { get; set; }
//}

//public class New
//{
//    public string to { get; set; }
//    public string[] actions { get; set; }
//}

//public class Child
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Deferred
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Fix
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class AdminReview
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Verify
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Requested
//{
//    public string to { get; set; }
//    public string[] actions { get; set; }
//}

//public class Accepted
//{
//    public string to { get; set; }
//    public string[] actions { get; set; }
//}

//public class Active
//{
//    public string to { get; set; }
//    public string[] actions { get; set; }
//}

//public class Resolved
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Proposed
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Removed
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Design
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Ready
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Approved
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Archive
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Repair
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Inactive
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Completed
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class InPlanning
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class InProgress
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class InReview
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class NotStarted
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Assigned
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class ReviewCompleted
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Implement
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Preliminary
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class OnHold
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Field
//{
//    public string defaultValue { get; set; }
//    public string helpText { get; set; }
//    public bool alwaysRequired { get; set; }
//    public string referenceName { get; set; }
//    public string name { get; set; }
//    public string url { get; set; }
//}

//public class Fieldinstance
//{
//    public string defaultValue { get; set; }
//    public string helpText { get; set; }
//    public bool alwaysRequired { get; set; }
//    public string referenceName { get; set; }
//    public string name { get; set; }
//    public string url { get; set; }
//}

//public class State
//{
//    public string name { get; set; }
//    public string color { get; set; }
//    public string category { get; set; }
//}

// QA2

//public class Rootobject
//{
//    public int count { get; set; }
//    public Value[] value { get; set; }
//}

//public class Value
//{
//    public string name { get; set; }
//    public string referenceName { get; set; }
//    public string description { get; set; }
//    public string color { get; set; }
//    public Icon icon { get; set; }
//    public bool isDisabled { get; set; }
//    public string xmlForm { get; set; }
//    public Field[] fields { get; set; }
//    public Fieldinstance[] fieldInstances { get; set; }
//    public Transitions transitions { get; set; }
//    public State[] states { get; set; }
//    public string url { get; set; }
//}

//public class Icon
//{
//    public string id { get; set; }
//    public string url { get; set; }
//}

//public class Transitions
//{
//    public Closed[] Closed { get; set; }
//    public New[] New { get; set; }
//    public Child[] _ { get; set; }
//    public Deferred[] Deferred { get; set; }
//    public Fix[] Fix { get; set; }
//    public AdminReview[] AdminReview { get; set; }
//    public Verify[] Verify { get; set; }
//    public Requested[] Requested { get; set; }
//    public Accepted[] Accepted { get; set; }
//    public Active[] Active { get; set; }
//    public Resolved[] Resolved { get; set; }
//    public Proposed[] Proposed { get; set; }
//    public Removed[] Removed { get; set; }
//    public Design[] Design { get; set; }
//    public Ready[] Ready { get; set; }
//    public Approved[] Approved { get; set; }
//    public Archive[] Archive { get; set; }
//    public Repair[] Repair { get; set; }
//    public Inactive[] Inactive { get; set; }
//    public Completed[] Completed { get; set; }
//    public InPlanning[] InPlanning { get; set; }
//    public InProgress[] InProgress { get; set; }
//    public InReview[] InReview { get; set; }
//    public NotStarted[] NotStarted { get; set; }
//    public Assigned[] Assigned { get; set; }
//    public ReviewCompleted[] ReviewCompleted { get; set; }
//    public Implement[] Implements { get; set; }
//    public OnHold[] OnHold { get; set; }
//    public Preliminary[] Preliminary { get; set; }
//}

//public class Closed
//{
//    public string to { get; set; }
//    public string[] actions { get; set; }
//}

//public class New
//{
//    public string to { get; set; }
//    public string[] actions { get; set; }
//}

//public class Child
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Deferred
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Fix
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class AdminReview
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Verify
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Requested
//{
//    public string to { get; set; }
//    public string[] actions { get; set; }
//}

//public class Accepted
//{
//    public string to { get; set; }
//    public string[] actions { get; set; }
//}

//public class Active
//{
//    public string to { get; set; }
//    public string[] actions { get; set; }
//}

//public class Resolved
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Proposed
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Removed
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Design
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Ready
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Approved
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Archive
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Repair
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Inactive
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Completed
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class InPlanning
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class InProgress
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class InReview
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class NotStarted
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Assigned
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class ReviewCompleted
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Implement
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class OnHold
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Preliminary
//{
//    public string to { get; set; }
//    public object actions { get; set; }
//}

//public class Field
//{
//    public string defaultValue { get; set; }
//    public string helpText { get; set; }
//    public bool alwaysRequired { get; set; }
//    public string referenceName { get; set; }
//    public string name { get; set; }
//    public string url { get; set; }
//}

//public class Fieldinstance
//{
//    public string defaultValue { get; set; }
//    public string helpText { get; set; }
//    public bool alwaysRequired { get; set; }
//    public string referenceName { get; set; }
//    public string name { get; set; }
//    public string url { get; set; }
//}

//public class State
//{
//    public string name { get; set; }
//    public string color { get; set; }
//    public string category { get; set; }
//}


public class Rootobject
{
    public Action[] Actions { get; set; }
}

public class Action
{
    public string Name { get; set; }
    public ActionTarget[] Target { get; set; }

    public class ActionTarget
    {
        public string to { get; set; }
        public string[] actions { get; set; }
    }
}

public class Requested
{
    public string to { get; set; }
    public string[] actions { get; set; }
}

public class Child
{
    public string to { get; set; }
    public object actions { get; set; }
}


//public class Rootobject
//{
//    public string to { get; set; }
//    public string[] actions { get; set; }
//}

