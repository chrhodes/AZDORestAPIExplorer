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
            public PullRequestIteration pullRequestIteration;
        }

        public class SelectedPullRequestIterationChangeChangedEvent : PubSubEvent<PullRequestIterationChange> { }
    }

    public class PullRequestIterationChanges
    {
        public int count { get; set; }
        public PullRequestIterationChange[] value { get; set; }
    }

    public class PullRequestIterationChange
    {
        // TODO(crhodes)
        // PasteSpecial from Json result text

        // Nest any additional classes inside class PullRequestIterationChange

        #region Nested Classes

        #endregion


        public RESTResult<PullRequestIterationChange> Results { get; set; } = new RESTResult<PullRequestIterationChange>();

        public async Task<RESTResult<PullRequestIterationChange>> GetList(GetPullRequestIterationChangesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestIterationChange)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // TODO(crhodes)
                // Update Uri  Use args for parameters.
                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"<UPDATE URI>?"
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

                    Results.ResultItems = new ObservableCollection<PullRequestIterationChange>(resultRoot.value);

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

                            Results.ResultItems.AddRange(resultRootC.value);

                            hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                        }
                    }

                    #endregion

                    // TODO(crhodes)
                    // Remove this if not using Top and Skip

                    #region Top and Skip

                    bool hasMoreResults = false;

                    if (resultRoot.count == 100)
                    {
                        hasMoreResults = true;
                    }

                    int itemsToSkip = 100;

                    // while (hasMoreResults)
                    // {
                    // var requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    // + $"<UPDATE URI>$top=100&$skip={itemsToSkip}"
                    // + "&api-version=6.1-preview.1";

                    // var exchange2 = Results.ContinueExchange(client, requestUri2);

                    // using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                    // {
                    // Results.RecordExchangeResponse(response2, exchange2);

                    // response2.EnsureSuccessStatusCode();
                    // string outJson2 = await response2.Content.ReadAsStringAsync();

                    // PullRequestIterationChanges resultRoot2C = JsonConvert.DeserializeObject<PullRequestIterationChanges>(outJson2);

                    // Results.ResultItems.AddRange(resultRoot2C.value);

                    // if (resultRoot2C.count == 100)
                    // {
                    // hasMoreResults = true;
                    // itemsToSkip += 100;
                    // }
                    // else
                    // {
                    // hasMoreResults = false;
                    // }
                    // }
                    // }

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
