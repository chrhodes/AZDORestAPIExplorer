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
        public class GetPullRequestPropertiesEvent : PubSubEvent<GetPullRequestPropertiesEventArgs> { }

        public class GetPullRequestPropertiesEventArgs
        {
            public Organization Organization;
            public Domain.Core.Project Project;

            public PullRequest PullRequest;
            public GitRepository Repository;
        }

        public class SelectedPullRequestPropertyChangedEvent : PubSubEvent<PullRequestProperty> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class PullRequestProperty

    public class PullRequestProperties
    {
        public int count { get; set; }
        public PullRequestProperty[] value { get; set; }
    }

    public class PullRequestProperty
    {
        public RESTResult<PullRequestProperty> Results { get; set; } = new RESTResult<PullRequestProperty>();

        public async Task<RESTResult<PullRequestProperty>> GetList(GetPullRequestPropertiesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestProperty)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // TODO(crhodes)
                // Update Uri  Use args for parameters.
                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/pullrequests"
                    + $"/{args.PullRequest.pullRequestId}/properties"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    // HACK(crhodes)
                    // There is only one item coming back, not a count and collection.
                    // Hack it into the collection.

                    //PullRequestProperties resultRoot = JsonConvert.DeserializeObject<PullRequestProperties>(outJson);
                    PullRequestProperty result = JsonConvert.DeserializeObject<PullRequestProperty>(outJson);

                    //Results.ResultItems = new ObservableCollection<PullRequestProperty>(resultRoot.value);
                    Results.ResultItems = new ObservableCollection<PullRequestProperty>();

                    Results.ResultItems.Add(result);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(PullRequestProperty)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
