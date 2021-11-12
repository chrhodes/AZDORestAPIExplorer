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
        public class GetPullRequestIterationChangesEvent : PubSubEvent<GetPullRequestIterationChangesEventArgs> { }

        public class GetPullRequestIterationChangesEventArgs
        {
            public Organization Organization;
            public Domain.Core.Project Project;

            public GitRepository Repository;
            public PullRequest PullRequest;
            public PullRequestIteration PullRequestIteration;
        }

        public class SelectedPullRequestIterationChangeChangedEvent : PubSubEvent<PullRequestIterationChange> { }
    }


    //public class Rootobject
    //{
    //    public Changeentry[] changeEntries { get; set; }
    //}

    //public class Changeentry
    //{
    //    public int changeTrackingId { get; set; }
    //    public int changeId { get; set; }
    //    public Item item { get; set; }
    //    public string changeType { get; set; }
    //}

    //public class Item
    //{
    //    public string objectId { get; set; }
    //    public string originalObjectId { get; set; }
    //    public string path { get; set; }
    //}

    public class PullRequestIterationChanges
    {
        public PullRequestIterationChange[] changeEntries { get; set; }
    }

    public class PullRequestIterationChange
    {
        public int changeTrackingId { get; set; }
        public int changeId { get; set; }
        public Item item { get; set; }
        public string changeType { get; set; }

        #region Nested Classes

        public class Item
        {
            public string objectId { get; set; }
            public string originalObjectId { get; set; }
            public string path { get; set; }
        }

        #endregion


        public RESTResult<PullRequestIterationChange> Results { get; set; } = new RESTResult<PullRequestIterationChange>();

        public async Task<RESTResult<PullRequestIterationChange>> GetList(GetPullRequestIterationChangesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestIterationChange)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/iterations/{iterationId}/changes?api-version=6.1-preview.1
                // GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/iterations/{iterationId}/changes?$top={$top}&$skip={$skip}&$compareTo={$compareTo}&api-version=6.1-preview.1

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/pullrequests"
                    + $"/{args.PullRequest.pullRequestId}/iterations/{args.PullRequestIteration.id}/changes?$top=100&"
                    + "api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    // TODO(crhodes)
                    // Use this for collections

                    PullRequestIterationChanges resultRoot = JsonConvert.DeserializeObject<PullRequestIterationChanges>(outJson);

                    Results.ResultItems = new ObservableCollection<PullRequestIterationChange>(resultRoot.changeEntries);

                    // Use this for single items                    

                    // BuildInfo result = JsonConvert.DeserializeObject<BuildInfo>(outJson);

                    // Results.ResultItem = result;
                    // Results.Count = 1;

                    #region Paging Support

                    // TODO(crhodes)

                    // If call has some kind of paging support ...
                    // otherwise remove all of this.

                    // Calls that support paging or partial results typically use one of these
                    //  continuationtoken in the header
                    //  top and skip

                    // Remove this if not using continuationHeaders

                    #region ContinuationHeaders

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    while (hasContinuationToken)
                    {
                        string continueToken = continuationHeaders.First();

                        var requestUri2 = $"{args.Organization.Uri}/_apis/"
                            + $"<UPDATE URI>"
                            + $"continuationToken={continueToken}"
                            + "?api-version=6.1-preview.1";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            PullRequestIterationChanges resultRootC = JsonConvert.DeserializeObject<PullRequestIterationChanges>(outJson2);

                            Results.ResultItems.AddRange(resultRootC.changeEntries);

                            hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                        }
                    }

                    #endregion

                    // TODO(crhodes)
                    // Remove this if not using Top and Skip

                    #region Top and Skip

                    bool hasMoreResults = false;

                    if (resultRoot.changeEntries.Count() == 100)
                    {
                        hasMoreResults = true;
                    }

                    int itemsToSkip = 100;

                    while (hasMoreResults)
                    {
                        var requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                             + $"git/repositories/{args.Repository.id}/pullrequests"
                             + $"/{args.PullRequest.pullRequestId}/iterations/{args.PullRequestIteration.id}/changes?$top=100&$skip={itemsToSkip}&"
                        + $"<UPDATE URI>$top=100&$skip={itemsToSkip}"
                        + "api-version=6.1-preview.1";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            PullRequestIterationChanges resultRoot2C = JsonConvert.DeserializeObject<PullRequestIterationChanges>(outJson2);

                            Results.ResultItems.AddRange(resultRoot2C.changeEntries);

                            if (resultRoot2C.changeEntries.Count() == 100)
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

                    #endregion

                    #endregion

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(PullRequestIterationChange)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
