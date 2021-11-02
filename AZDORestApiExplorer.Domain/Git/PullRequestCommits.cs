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
        public class GetPullRequestCommitsEvent : PubSubEvent<GetPullRequestCommitsEventArgs> { }

        public class GetPullRequestCommitsEventArgs
        {
            public Organization Organization;
            public Domain.Core.Project Project;

            public GitRepository Repository;
            public PullRequest PullRequest;
        }

        public class SelectedPullRequestCommitChangedEvent : PubSubEvent<PullRequestCommit> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class PullRequestCommit

    public class PullRequestCommits
    {
        public int count { get; set; }
        public PullRequestCommit[] value { get; set; }
    }

    public class PullRequestCommit
    {
        public Author author { get; set; }
        public string comment { get; set; }
        public string commitId { get; set; }
        public Committer committer { get; set; }
        public string url { get; set; }
 
        public RESTResult<PullRequestCommit> Results { get; set; } = new RESTResult<PullRequestCommit>();

        public async Task<RESTResult<PullRequestCommit>> GetList(GetPullRequestCommitsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestCommit)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/pullrequests"
                    + $"/{args.PullRequest.pullRequestId}/commits"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    PullRequestCommits resultRoot = JsonConvert.DeserializeObject<PullRequestCommits>(outJson);

                    Results.ResultItems = new ObservableCollection<PullRequestCommit>(resultRoot.value);

                    //// TODO(crhodes)
                    //// Remove this if not using continuationHeaders

                    //#region ContinuationHeaders

                    //IEnumerable<string> continuationHeaders = default;

                    //bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    //while (hasContinuationToken)
                    //{
                    //    string continueToken = continuationHeaders.First();

                    //    var requestUri2 = $"{args.Organization.Uri}/_apis/"
                    //        + $"<UPDATE URI>"
                    //        + $"continuationToken={continueToken}"
                    //        + "?api-version=6.1-preview.1";

                    //    var exchange2 = Results.ContinueExchange(client, requestUri2);

                    //    using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                    //    {
                    //        Results.RecordExchangeResponse(response2, exchange2);

                    //        response2.EnsureSuccessStatusCode();
                    //        string outJson2 = await response2.Content.ReadAsStringAsync();

                    //        PullRequestCommits resultRootC = JsonConvert.DeserializeObject<PullRequestCommits>(outJson2);

                    //        Results.ResultItems.AddRange(resultRootC.value);

                    //        hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                    //    }
                    //}

                    //#endregion

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(PullRequestCommit)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
