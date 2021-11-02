using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

            public Domain.Git.GitRepository Repository;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedCommitChangedEvent : PubSubEvent<Commit> { }
    }

    public class Commits
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

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/commits?$top=100"
                    + "&api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    Commits resultRoot = JsonConvert.DeserializeObject<Commits>(outJson);

                    Results.ResultItems = new ObservableCollection<Commit>(resultRoot.value);

                    bool hasMoreResults = false;

                    if (resultRoot.count == 100)
                    {
                        hasMoreResults = true;
                    }

                    int itemsToSkip = 100;

                    while (hasMoreResults)
                    {
                        var requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                            + $"git/repositories/{args.Repository.id}/commits?$top=100&$skip={itemsToSkip}"
                            + "&api-version=6.1-preview.1";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            Commits resultRoot2C = JsonConvert.DeserializeObject<Commits>(outJson2);

                            Results.ResultItems.AddRange(resultRoot2C.value);

                            if (resultRoot2C.count == 100)
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