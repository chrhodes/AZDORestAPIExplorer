using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace AZDORestApiExplorer.Domain.Git
{
    namespace Events
    {
        //public class GetProjectRepositoriesEvent : PubSubEvent<GetRepositoriesEventArgs> { }

        //public class GetRepositoriesEvent : PubSubEvent<GetRepositoriesEventArgs> { }

        //public class GetRepositoriesEventArgs
        //{
        //    public Organization Organization;

        //    public Domain.Core.Project Project;
        //}

        //public class SelectedRepositoryChangedEvent : PubSubEvent<GitRepository> { }
    }

    public class PullRequestProperties
    {
        public int count { get; set; }
        public PullRequestProperty value { get; set; }
    }

    public class PullRequestProperty
    {
        [JsonProperty("Microsoft.Git.PullRequest.IsDraft")]
        public MicrosoftGitPullrequestIsdraft MicrosoftGitPullRequestIsDraft { get; set; }

        [JsonProperty("Microsoft.Git.PullRequest.SourceRefName")]
        public MicrosoftGitPullrequestSourcerefname MicrosoftGitPullRequestSourceRefName { get; set; }

        [JsonProperty("Microsoft.Git.PullRequest.TargetRefName")]
        public MicrosoftGitPullrequestTargetrefname MicrosoftGitPullRequestTargetRefName { get; set; }

        #region Nested Classes

        public class MicrosoftGitPullrequestIsdraft
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class MicrosoftGitPullrequestSourcerefname
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        public class MicrosoftGitPullrequestTargetrefname
        {
            public string type { get; set; }
            public string value { get; set; }
        }

        #endregion
    }
}