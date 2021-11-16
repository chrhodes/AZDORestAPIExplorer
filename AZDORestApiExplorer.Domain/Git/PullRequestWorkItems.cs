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
        public class GetPullRequestWorkItemsEvent : PubSubEvent<GetPullRequestWorkItemsEventArgs> { }

        public class ClearPullRequestWorkItemsEvent : PubSubEvent { }

        public class GetPullRequestWorkItemsEventArgs
        {
            public Organization Organization;
            public Domain.Core.Project Project;

            public PullRequest PullRequest;
            public GitRepository Repository;
        }

        public class SelectedPullRequestWorkItemChangedEvent : PubSubEvent<PullRequestWorkItem> { }
    }

    public class PullRequestWorkItems
    {
        public int count { get; set; }
        public PullRequestWorkItem[] value { get; set; }
    }

    public class PullRequestWorkItem
    {
        public string id { get; set; }
        public string url { get; set; }


        public RESTResult<PullRequestWorkItem> Results { get; set; } = new RESTResult<PullRequestWorkItem>();

        public async Task<RESTResult<PullRequestWorkItem>> GetList(GetPullRequestWorkItemsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestWorkItem)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                //GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/workitems?api-version=6.1-preview.1

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/pullrequests"
                    + $"/{args.PullRequest.pullRequestId}/workitems"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    PullRequestWorkItems resultRoot = JsonConvert.DeserializeObject<PullRequestWorkItems>(outJson);

                    Results.ResultItems = new ObservableCollection<PullRequestWorkItem>(resultRoot.value);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(PullRequestWorkItem)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}