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
        public class GetPullRequestsEvent : PubSubEvent<GetPullRequestsEventArgs> { }

        public class GetPullRequestsEventArgs
        {
            public Organization Organization;
            public Domain.Core.Project Project;

            public PullRequest PullRequest;
            public GitRepository Repository;
        }

        // TODO(crhodes)
        // Add stuff to this or create custom ones and use above
        public class SelectedPullRequestChangedEvent : PubSubEvent<PullRequest> { }
    }

    public class PullRequests
    {
        public int count { get; set; }
        public PullRequest[] value { get; set; }
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

        public async Task<RESTResult<PullRequest>> GetList(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequest)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all&$top=100"
                    + "&api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    PullRequests resultRoot = JsonConvert.DeserializeObject<PullRequests>(outJson);

                    Results.ResultItems = new ObservableCollection<PullRequest>(resultRoot.value);

                    bool hasMoreResults = false;

                    if (resultRoot.count == 100)
                    {
                        hasMoreResults = true;
                    }

                    int itemsToSkip = 100;

                    while (hasMoreResults)
                    {
                        var requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                            + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all&$top=100&$skip={itemsToSkip}"
                            + "&api-version=6.1-preview.1";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            PullRequests resultRoot2C = JsonConvert.DeserializeObject<PullRequests>(outJson2);

                            Results.ResultItems.AddRange(resultRoot2C.value);

                            if (resultRoot2C.count == 100)
                            {
                                hasMoreResults = true;
                                itemsToSkip += 100;
                            }
                            else
                            {
                                hasMoreResults = false;
                            }
                        }
                    }

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

    //public class PullRequestCommitsRoot
    //{
    //    public int count { get; set; }
    //    public Commit[] value { get; set; }

    //    public class Commit
    //    {
    //        public Author author { get; set; }
    //        public string comment { get; set; }
    //        public string commitId { get; set; }
    //        public Committer committer { get; set; }
    //        public string url { get; set; }
    //    }
    //}

}