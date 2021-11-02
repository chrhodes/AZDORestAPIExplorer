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
        public class GetPullRequestReviewersEvent : PubSubEvent<GetPullRequestReviewersEventArgs> { }

        public class GetPullRequestReviewersEventArgs
        {
            public Organization Organization;
            public Domain.Core.Project Project;

            public PullRequest PullRequest;
            public GitRepository Repository;
        }

        public class SelectedPullRequestReviewerChangedEvent : PubSubEvent<PullRequestReviewer> { }
    }

    public class PullRequestReviewers
    {
        public int count { get; set; }
        public PullRequestReviewer[] value { get; set; }
    }

    public class PullRequestReviewer
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

        #region Nested Classes

        public class _Links
        {
            public Avatar avatar { get; set; }
        }

        public class Avatar
        {
            public string href { get; set; }
        }

        #endregion

        public RESTResult<PullRequestReviewer> Results { get; set; } = new RESTResult<PullRequestReviewer>();

        public async Task<RESTResult<PullRequestReviewer>> GetList(GetPullRequestReviewersEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestReviewer)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/pullrequests"
                    + $"/{args.PullRequest.pullRequestId}/reviewers"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    PullRequestReviewers resultRoot = JsonConvert.DeserializeObject<PullRequestReviewers>(outJson);

                    Results.ResultItems = new ObservableCollection<PullRequestReviewer>(resultRoot.value);

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

                    //        PullRequestReviewersRoot resultRootC = JsonConvert.DeserializeObject<PullRequestReviewersRoot>(outJson2);

                    //        Results.ResultItems.AddRange(resultRootC.value);

                    //        hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                    //    }
                    //}

                    //#endregion

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(PullRequestReviewer)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}