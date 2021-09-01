using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Git.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Git
{
    namespace Events
    {
        public class GetProjectRepositoriesEvent : PubSubEvent<GetProjectRepositoriesEventArgs> { }

        public class GetProjectRepositoriesEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Domain.Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class GetRepositoriesEvent : PubSubEvent<GetRepositoriesEventArgs> { }

        public class GetRepositoriesEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            //public Domain.Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedRepositoryChangedEvent : PubSubEvent<Repository> { }
    }

    public class RepositoriesRoot
    {
        public int count { get; set; }
        public Repository[] value { get; set; }
    }

    public class Repository
    {
        public string defaultBranch { get; set; }
        public string id { get; set; }
        public bool isDisabled { get; set; }
        public bool isFork { get; set; }
        public string name { get; set; }
        public Project project { get; set; }
        public string remoteUrl { get; set; }
        public RESTResult<Repository> Results { get; set; } = new RESTResult<Repository>();
        public int size { get; set; }
        public string sshUrl { get; set; }
        public string url { get; set; }
        public string webUrl { get; set; }

        public async Task<RESTResult<Repository>> GetProjectRepositories(GetProjectRepositoriesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Repository)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    RepositoriesRoot resultRoot = JsonConvert.DeserializeObject<RepositoriesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Repository>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }
            }

            Log.DOMAIN("Exit(Project)", Common.LOG_CATEGORY, startTicks);

            return Results;
        }

        public async Task<RESTResult<Repository>> GetRepositories(GetRepositoriesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Repository)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // TODO(crhodes)
                // Update Uri  Use args for parameters.
                var requestUri = $"{args.Organization.Uri}/_apis/"
                    + $"git/repositories"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    RepositoriesRoot resultRoot = JsonConvert.DeserializeObject<RepositoriesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Repository>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }
            }

            Log.DOMAIN("Exit(Project)", Common.LOG_CATEGORY, startTicks);

            return Results;
        }
    }
}