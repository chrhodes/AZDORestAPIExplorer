using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        public class GetPullRequestThreadCommentsEvent : PubSubEvent<GetPullRequestThreadCommentsEventArgs> { }

        public class GetPullRequestThreadCommentsEventArgs
        {
            public Organization Organization;
            public Domain.Core.Project Project;

            public PullRequest PullRequest;
            public GitRepository Repository;
            public PullRequestThread PullRequestThread;
        }

        public class SelectedPullRequestThreadCommentChangedEvent : PubSubEvent<PullRequestThreadComment> { }
    }

    public class PullRequestThreadComments
    {
        public int count { get; set; }
        public PullRequestThreadComment[] value { get; set; }
    }

    public class PullRequestThreadComment
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
            public _Links1 _links { get; set; }

        #region Nested Classes
        public class Author
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
            public Threads threads { get; set; }
            public Pullrequests pullRequests { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Repository
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

        public RESTResult<PullRequestThreadComment> Results { get; set; } = new RESTResult<PullRequestThreadComment>();

        public async Task<RESTResult<PullRequestThreadComment>> GetList(GetPullRequestThreadCommentsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestThreadComment)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/threads/{threadId}/comments?api-version=6.1-preview.1

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/pullRequests/"
                    + $"{args.PullRequest.pullRequestId}/threads/{args.PullRequestThread.id}/comments?"
                    + "api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    PullRequestThreadComments resultRoot = JsonConvert.DeserializeObject<PullRequestThreadComments>(outJson);

                    Results.ResultItems = new ObservableCollection<PullRequestThreadComment>(resultRoot.value);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(PullRequestThreadComment)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
