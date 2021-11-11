using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Git.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Git
{
    namespace Events
    {
        public class GetPullRequestThreadsEvent : PubSubEvent<GetPullRequestThreadsEventArgs> { }

        public class GetPullRequestThreadsEventArgs
        {
            public Organization Organization;
            public Domain.Core.Project Project;

            public PullRequest PullRequest;
            public GitRepository Repository;
            public PullRequestThread PullRequestThread;
        }

        public class SelectedPullRequestThreadChangedEvent : PubSubEvent<PullRequestThread> { }
    }

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
            [JsonProperty("$type")]
            public string type { get; set; }
            [JsonProperty("$value")]
            public string value { get; set; }
        }

        public class Codereviewvoteresult
        {
            [JsonProperty("$type")]
            public string type { get; set; }
            [JsonProperty("$value")]
            public string value { get; set; }
        }

        public class Codereviewvotedbyidentity
        {
            [JsonProperty("$type")]
            public string type { get; set; }
            [JsonProperty("$value")]
            public string value { get; set; }
        }

        public class Codereviewvotedbyinitiatoridentity
        {
            [JsonProperty("$type")]
            public string type { get; set; }
            [JsonProperty("$value")]
            public string value { get; set; }
        }

        public class Codereviewpolicytype
        {
            [JsonProperty("$type")]
            public string type { get; set; }
            [JsonProperty("$value")]
            public string value { get; set; }
        }

        public class Linkedworkitems
        {
            [JsonProperty("$type")]
            public string type { get; set; }
            [JsonProperty("$value")]
            public string value { get; set; }
        }

        public class Codereviewstatus
        {
            [JsonProperty("$type")]
            public string type { get; set; }
            [JsonProperty("$value")]
            public string value { get; set; }
        }

        public class Codereviewstatusupdateassociatedcommit
        {
            [JsonProperty("$type")]
            public string type { get; set; }
            [JsonProperty("$value")]
            public string value { get; set; }
        }

        public class Bypasspolicy
        {
            [JsonProperty("$type")]
            public string type { get; set; }
            [JsonProperty("$value")]
            public string value { get; set; }
        }

        public class Codereviewstatusupdatedbyidentity
        {
            [JsonProperty("$type")]
            public string type { get; set; }
            [JsonProperty("$value")]
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

        public RESTResult<PullRequestThread> Results { get; set; } = new RESTResult<PullRequestThread>();

        public async Task<RESTResult<PullRequestThread>> GetList(GetPullRequestThreadsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestThread)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                //GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/threads?api-version=6.1-preview.1
                //GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/threads?$iteration={$iteration}&$baseIteration={$baseIteration}&api-version=6.1-preview.1
                //GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/threads/{threadId}?api-version=6.1-preview.1
                //GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/threads/{threadId}?$iteration={$iteration}&$baseIteration={$baseIteration}&api-version=6.1-preview.1

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/pullrequests"
                    + $"/{args.PullRequest.pullRequestId}/threads"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    PullRequestThreads resultRoot = JsonConvert.DeserializeObject<PullRequestThreads>(outJson);

                    Results.ResultItems = new ObservableCollection<PullRequestThread>(resultRoot.value);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(PullRequestThread)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}