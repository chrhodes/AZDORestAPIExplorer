using System;
using System.Collections.Generic;
using System.Linq;

namespace AZDORestApiExplorer.Domain.Git
{
    public class PullRequestThreads
    {
        public int count { get; set; }
        public PullRequestThread[] value { get; set; }
    }

    public class PullRequestThread
    {
        public object pullRequestThreadContext { get; set; }
        public int id { get; set; }
        public DateTime publishedDate { get; set; }
        public DateTime lastUpdatedDate { get; set; }
        public Comment[] comments { get; set; }
        public object threadContext { get; set; }
        public Properties properties { get; set; }
        public Identities identities { get; set; }
        public bool isDeleted { get; set; }
        public _Links1 _links { get; set; }

        #region Nested Classes

        public class Properties
        {
            public Codereviewthreadtype CodeReviewThreadType { get; set; }
            public Codereviewvoteresult CodeReviewVoteResult { get; set; }
            public Codereviewvotedbyidentity CodeReviewVotedByIdentity { get; set; }
            public Codereviewvotedbyinitiatoridentity CodeReviewVotedByInitiatorIdentity { get; set; }
            public Codereviewpolicytype CodeReviewPolicyType { get; set; }
            public Linkedworkitems LinkedWorkItems { get; set; }
            public Codereviewstatus CodeReviewStatus { get; set; }
            public Codereviewstatusupdateassociatedcommit CodeReviewStatusUpdateAssociatedCommit { get; set; }
            public Bypasspolicy BypassPolicy { get; set; }
            public Codereviewstatusupdatedbyidentity CodeReviewStatusUpdatedByIdentity { get; set; }
        }

        public class Codereviewthreadtype
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class Codereviewvoteresult
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class Codereviewvotedbyidentity
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class Codereviewvotedbyinitiatoridentity
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class Codereviewpolicytype
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class Linkedworkitems
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class Codereviewstatus
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class Codereviewstatusupdateassociatedcommit
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class Bypasspolicy
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class Codereviewstatusupdatedbyidentity
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class Identities
        {
            public _1 _1 { get; set; }
        }

        public class _1
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

        public class _Links1
        {
            public Self self { get; set; }
            public Repository repository { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Repository
        {
            public string href { get; set; }
        }

        public class Comment
        {
            public int id { get; set; }
            public int parentCommentId { get; set; }
            public Author author { get; set; }
            public string content { get; set; }
            public DateTime publishedDate { get; set; }
            public DateTime lastUpdatedDate { get; set; }
            public DateTime lastContentUpdatedDate { get; set; }
            public string commentType { get; set; }
            public object[] usersLiked { get; set; }
            public _Links3 _links { get; set; }
        }

        public class Author
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
            public Avatar1 avatar { get; set; }
        }

        public class Avatar1
        {
            public string href { get; set; }
        }

        public class _Links3
        {
            public Self1 self { get; set; }
            public Repository1 repository { get; set; }
            public Threads threads { get; set; }
            public Pullrequests pullRequests { get; set; }
        }

        public class Self1
        {
            public string href { get; set; }
        }

        public class Repository1
        {
            public string href { get; set; }
        }

        public class Threads
        {
            public string href { get; set; }
        }

        public class Pullrequests
        {
            public string href { get; set; }
        }

        #endregion
    }
}