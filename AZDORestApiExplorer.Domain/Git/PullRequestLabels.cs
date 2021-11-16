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
        public class GetPullRequestLabelsEvent : PubSubEvent<GetPullRequestLabelsEventArgs> { }

        public class ClearPullRequestLabelsEvent : PubSubEvent { }

        public class GetPullRequestLabelsEventArgs
        {
            public Organization Organization;
            public Domain.Core.Project Project;

            public PullRequest PullRequest;
            public GitRepository Repository;
        }

        public class SelectedPullRequestLabelChangedEvent : PubSubEvent<PullRequestLabel> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class PullRequestLabel

    public class PullRequestLabelsRoot
    {
        public int count { get; set; }
        public PullRequestLabel[] value { get; set; }
    }

    public class PullRequestLabel
    {
        public RESTResult<PullRequestLabel> Results { get; set; } = new RESTResult<PullRequestLabel>();

        public async Task<RESTResult<PullRequestLabel>> GetList(GetPullRequestLabelsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestLabel)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                //GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/labels?api-version=6.1-preview.1
                //GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/labels?projectId={projectId}&api-version=6.1-preview.1
                //GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/labels/{labelIdOrName}?api-version=6.1-preview.1
                //GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/labels/{labelIdOrName}?projectId={projectId}&api-version=6.1-preview.1

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                      + $"git/repositories/{args.Repository.id}/pullrequests"
                      + $"/{args.PullRequest.pullRequestId}/labels"
                      + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    PullRequestLabelsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestLabelsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<PullRequestLabel>(resultRoot.value);

                    // TODO(crhodes)
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

                            PullRequestLabelsRoot resultRootC = JsonConvert.DeserializeObject<PullRequestLabelsRoot>(outJson2);

                            Results.ResultItems.AddRange(resultRootC.value);

                            hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                        }
                    }

                    #endregion

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(PullRequestLabel)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
