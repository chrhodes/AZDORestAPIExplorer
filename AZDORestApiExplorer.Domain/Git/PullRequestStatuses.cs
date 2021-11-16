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
        public class GetPullRequestStatusesEvent : PubSubEvent<GetPullRequestStatusesEventArgs> { }

        public class ClearPullRequestStatusesEvent : PubSubEvent { }

        public class GetPullRequestStatusesEventArgs
        {
            public Organization Organization;
            public Domain.Core.Project Project;

            public PullRequest PullRequest;
            public GitRepository Repository;
        }

        public class SelectedPullRequestStatusChangedEvent : PubSubEvent<PullRequestStatus> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class PullRequestStatus

    public class PullRequestStatuses
    {
        public int count { get; set; }
        public PullRequestStatus[] value { get; set; }
    }

    public class PullRequestStatus
    {
        public RESTResult<PullRequestStatus> Results { get; set; } = new RESTResult<PullRequestStatus>();

        public async Task<RESTResult<PullRequestStatus>> GetList(GetPullRequestStatusesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestStatus)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                //GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/statuses?api-version=6.1-preview.1
                //GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/statuses/{statusId}?api-version=6.1-preview.1

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/pullrequests"
                    + $"/{args.PullRequest.pullRequestId}/statuses"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    PullRequestStatuses resultRoot = JsonConvert.DeserializeObject<PullRequestStatuses>(outJson);

                    Results.ResultItems = new ObservableCollection<PullRequestStatus>(resultRoot.value);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(PullRequestStatus)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
