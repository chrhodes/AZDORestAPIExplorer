using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public class GetPullRequestAttachmentsEvent : PubSubEvent<GetPullRequestsEventArgs> { }

        public class GetPullRequestCommitsEvent : PubSubEvent<GetPullRequestsEventArgs> { }

        public class GetPullRequestIterationsEvent : PubSubEvent<GetPullRequestsEventArgs> { }

        public class GetPullRequestLabelsEvent : PubSubEvent<GetPullRequestsEventArgs> { }

        public class GetPullRequestPropertiesEvent : PubSubEvent<GetPullRequestsEventArgs> { }

        public class GetPullRequestReviewersEvent : PubSubEvent<GetPullRequestsEventArgs> { }

        public class GetPullRequestsEvent : PubSubEvent<GetPullRequestsEventArgs> { }

        public class GetPullRequestsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Domain.Core.Project Project;

            public Domain.Git.PullRequest PullRequest;
            public Domain.Git.Repository Repository;
            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class GetPullRequestStatusesEvent : PubSubEvent<GetPullRequestsEventArgs> { }

        public class GetPullRequestThreadsEvent : PubSubEvent<GetPullRequestsEventArgs> { }

        public class GetPullRequestWorkItemsEvent : PubSubEvent<GetPullRequestsEventArgs> { }

        // TODO(crhodes)
        // Add stuff to this or create custom ones and use above
        public class SelectedPullRequestChangedEvent : PubSubEvent<PullRequest> { }
    }

    public class PullRequest
    {
        public Autocompletesetby autoCompleteSetBy { get; set; }
        public int codeReviewId { get; set; }
        public Completionoptions completionOptions { get; set; }
        public Createdby createdBy { get; set; }
        public DateTime creationDate { get; set; }
        public string description { get; set; }
        public bool isDraft { get; set; }
        public Lastmergecommit lastMergeCommit { get; set; }
        public Lastmergesourcecommit lastMergeSourceCommit { get; set; }
        public Lastmergetargetcommit lastMergeTargetCommit { get; set; }
        public string mergeId { get; set; }
        public string mergeStatus { get; set; }
        public int pullRequestId { get; set; }
        public Repository repository { get; set; }
        public RESTResult<PullRequest> Results { get; set; } = new RESTResult<PullRequest>();
        public Reviewer[] reviewers { get; set; }
        public string sourceRefName { get; set; }
        public string status { get; set; }
        public bool supportsIterations { get; set; }
        public string targetRefName { get; set; }
        public string title { get; set; }
        public string url { get; set; }

        //---
        public async Task<RESTResult<PullRequest>> GetList(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequest)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // TODO(crhodes)
                // Update Uri  Use args for parameters.
                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all"
                    + "&api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    PullRequestsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<PullRequest>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }
            }

            Log.DOMAIN("Exit(PullRequest)", Common.LOG_CATEGORY, startTicks);

            return Results;
        }

        public class _Links
        {
            public Avatar avatar { get; set; }
        }

        public class _Links1
        {
            public Avatar1 avatar { get; set; }
        }

        public class _Links2
        {
            public Avatar2 avatar { get; set; }
        }

        public class Autocompletesetby
        {
            public _Links1 _links { get; set; }
            public string descriptor { get; set; }
            public string displayName { get; set; }
            public string id { get; set; }
            public string imageUrl { get; set; }
            public string uniqueName { get; set; }
            public string url { get; set; }
        }

        public class Avatar
        {
            public string href { get; set; }
        }

        public class Avatar1
        {
            public string href { get; set; }
        }

        public class Avatar2
        {
            public string href { get; set; }
        }

        public class Completionoptions
        {
            public object[] autoCompleteIgnoreConfigIds { get; set; }
            public bool deleteSourceBranch { get; set; }
            public string mergeCommitMessage { get; set; }
            public string mergeStrategy { get; set; }
        }

        public class Createdby
        {
            public _Links _links { get; set; }
            public string descriptor { get; set; }
            public string displayName { get; set; }
            public string id { get; set; }
            public string imageUrl { get; set; }
            public string uniqueName { get; set; }
            public string url { get; set; }
        }

        public class Lastmergecommit
        {
            public string commitId { get; set; }
            public string url { get; set; }
        }

        public class Lastmergesourcecommit
        {
            public string commitId { get; set; }
            public string url { get; set; }
        }

        public class Lastmergetargetcommit
        {
            public string commitId { get; set; }
            public string url { get; set; }
        }

        public class Project
        {
            public string id { get; set; }
            public DateTime lastUpdateTime { get; set; }
            public string name { get; set; }
            public string state { get; set; }
            public string visibility { get; set; }
        }

        public class Repository
        {
            public string id { get; set; }
            public string name { get; set; }
            public Project project { get; set; }
            public string url { get; set; }
        }

        public class Reviewer
        {
            public _Links2 _links { get; set; }
            public string displayName { get; set; }
            public bool hasDeclined { get; set; }
            public string id { get; set; }
            public string imageUrl { get; set; }
            public bool isFlagged { get; set; }
            public bool isRequired { get; set; }
            public string reviewerUrl { get; set; }
            public string uniqueName { get; set; }
            public string url { get; set; }
            public int vote { get; set; }
        }

        //---
    }

    public class PullRequestCommitsRoot
    {
        public int count { get; set; }
        public Value[] value { get; set; }

        public class Value
        {
            public Author author { get; set; }
            public string comment { get; set; }
            public string commitId { get; set; }
            public Committer committer { get; set; }
            public string url { get; set; }
        }
    }

    public class PullRequestIterations
    {
        public int count { get; set; }
        public Value[] value { get; set; }

        public class _Links
        {
            public Avatar avatar { get; set; }
        }

        public class Author
        {
            public _Links _links { get; set; }
            public string descriptor { get; set; }
            public string displayName { get; set; }
            public string id { get; set; }
            public string imageUrl { get; set; }
            public string uniqueName { get; set; }
            public string url { get; set; }
        }

        public class Avatar
        {
            public string href { get; set; }
        }

        public class Commonrefcommit
        {
            public string commitId { get; set; }
        }

        public class Push
        {
            public int pushId { get; set; }
        }

        public class Sourcerefcommit
        {
            public string commitId { get; set; }
        }

        public class Targetrefcommit
        {
            public string commitId { get; set; }
        }

        public class Value
        {
            public Author author { get; set; }
            public Commonrefcommit commonRefCommit { get; set; }
            public DateTime createdDate { get; set; }
            public string description { get; set; }
            public bool hasMoreCommits { get; set; }
            public int id { get; set; }
            public Push push { get; set; }
            public string reason { get; set; }
            public Sourcerefcommit sourceRefCommit { get; set; }
            public Targetrefcommit targetRefCommit { get; set; }
            public DateTime updatedDate { get; set; }
        }
    }

    public class PullRequestsRoot
    {
        public int count { get; set; }
        public PullRequest[] value { get; set; }
    }

    public class ReviewersRoot
    {
        public int count { get; set; }
        public Value[] value { get; set; }

        public class _Links
        {
            public Avatar avatar { get; set; }
        }

        public class Avatar
        {
            public string href { get; set; }
        }

        public class Value
        {
            public _Links _links { get; set; }
            public string displayName { get; set; }
            public bool hasDeclined { get; set; }
            public string id { get; set; }
            public string imageUrl { get; set; }
            public bool isFlagged { get; set; }
            public bool isRequired { get; set; }
            public string reviewerUrl { get; set; }
            public string uniqueName { get; set; }
            public string url { get; set; }
            public int vote { get; set; }
        }
    }

    public class WorkItemsRoot
    {
        public int count { get; set; }
        public Value[] value { get; set; }

        public class Value
        {
            public string id { get; set; }
            public string url { get; set; }
        }
    }


    public class PullRequestProperties
    {
        public int count { get; set; }
        public Value value { get; set; }

        public class Value
        {
            public MicrosoftGitPullrequestIsdraft MicrosoftGitPullRequestIsDraft { get; set; }
            public MicrosoftGitPullrequestSourcerefname MicrosoftGitPullRequestSourceRefName { get; set; }
            public MicrosoftGitPullrequestTargetrefname MicrosoftGitPullRequestTargetRefName { get; set; }
        }

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
    }
}