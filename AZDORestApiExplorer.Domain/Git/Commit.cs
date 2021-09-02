using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public class GetCommitsEvent : PubSubEvent<GetCommitsEventArgs> { }

        public class GetCommitsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Domain.Core.Project Project;

            public Domain.Git.Repository Repository;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedCommitChangedEvent : PubSubEvent<Commit> { }
    }

    public class CommitsRoot
    {
        public int count { get; set; }
        public Commit[] value { get; set; }
    }

    public partial class Commit
    {
        public Author author { get; set; }
        public Changecounts changeCounts { get; set; }
        public string comment { get; set; }
        public bool commentTruncated { get; set; }
        public string commitId { get; set; }
        public Committer committer { get; set; }
        public string remoteUrl { get; set; }
        public string url { get; set; }

        public RESTResult<Commit> Results { get; set; } = new RESTResult<Commit>();

        public async Task<RESTResult<Commit>> GetList(GetCommitsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Repository)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // TODO(crhodes)
                // Update Uri  Use args for parameters.
                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/commits"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    CommitsRoot resultRoot = JsonConvert.DeserializeObject<CommitsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Commit>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }
            }

            Log.DOMAIN("Exit(Project)", Common.LOG_CATEGORY, startTicks);

            return Results;
        }

        public class Changecounts
        {
            public int Add { get; set; }
            public int Delete { get; set; }
            public int Edit { get; set; }
        }
    }
}