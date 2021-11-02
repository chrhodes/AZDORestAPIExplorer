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
        public class GetPullRequestIterationsEvent : PubSubEvent<GetPullRequestIterationsEventArgs> { }

        public class GetPullRequestIterationsEventArgs
        {
            public Organization Organization;
            public Domain.Core.Project Project;

            public GitRepository Repository;
            public PullRequest PullRequest;
        }

        public class SelectedPullRequestIterationChangedEvent : PubSubEvent<PullRequestIteration> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class PullRequestIteration

    public class PullRequestIterations
    {
        public int count { get; set; }
        public PullRequestIteration[] value { get; set; }
    }

    public class PullRequestIteration
    {

            public int id { get; set; }
            public string description { get; set; }
            public Author author { get; set; }
            public DateTime createdDate { get; set; }
            public DateTime updatedDate { get; set; }
            public Sourcerefcommit sourceRefCommit { get; set; }
            public Targetrefcommit targetRefCommit { get; set; }
            public Commonrefcommit commonRefCommit { get; set; }
            public bool hasMoreCommits { get; set; }
            public string reason { get; set; }

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

        public class Sourcerefcommit
        {
            public string commitId { get; set; }
        }

        public class Targetrefcommit
        {
            public string commitId { get; set; }
        }

        public class Commonrefcommit
        {
            public string commitId { get; set; }
        }

        public RESTResult<PullRequestIteration> Results { get; set; } = new RESTResult<PullRequestIteration>();

        public async Task<RESTResult<PullRequestIteration>> GetList(GetPullRequestIterationsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestIteration)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/pullrequests"
                    + $"/{args.PullRequest.pullRequestId}/iterations"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    PullRequestIterations resultRoot = JsonConvert.DeserializeObject<PullRequestIterations>(outJson);

                    Results.ResultItems = new ObservableCollection<PullRequestIteration>(resultRoot.value);

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

                    //        PullRequestIterations resultRootC = JsonConvert.DeserializeObject<PullRequestIterations>(outJson2);

                    //        Results.ResultItems.AddRange(resultRootC.value);

                    //        hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                    //    }
                    //}

                    //#endregion

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(PullRequestIteration)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
