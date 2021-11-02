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
        public class GetStatsEvent : PubSubEvent<GetStatsEventArgs> { }

        public class GetStatsEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;

            public Domain.Git.GitRepository Repository;
        }

        public class SelectedStatChangedEvent : PubSubEvent<Stat> { }
    }

    public class Stats
    {
        public int count { get; set; }
        public Stat[] value { get; set; }
    }

    public class Stat
    {
        public Commit commit { get; set; }
        public string name { get; set; }
        public int aheadCount { get; set; }
        public int behindCount { get; set; }
        public bool isBaseVersion { get; set; }

        #region Nested Classes

        public class Commit
        {
            public string commitId { get; set; }
            public Author author { get; set; }
            public Committer committer { get; set; }
            public string comment { get; set; }
            public string url { get; set; }
            public string treeId { get; set; }
            public string[] parents { get; set; }
            public bool commentTruncated { get; set; }
        }

        #endregion

        public RESTResult<Stat> Results { get; set; } = new RESTResult<Stat>();

        public async Task<RESTResult<Stat>> GetList(GetStatsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Repository)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/stats/branches"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    Stats resultRoot = JsonConvert.DeserializeObject<Stats>(outJson);

                    Results.ResultItems = new ObservableCollection<Stat>(resultRoot.value);

                    //bool hasMoreResults = false;

                    //if (resultRoot.count == 100)
                    //{
                    //    hasMoreResults = true;
                    //}

                    //int itemsToSkip = 100;

                    //while (hasMoreResults)
                    //{
                    //    var requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    //        + $"git/repositories/{args.Repository.id}/pushes?$top=100&$skip={itemsToSkip}"
                    //        + "&api-version=6.1-preview.1";

                    //    var exchange2 = Results.ContinueExchange(client, requestUri2);

                    //    using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                    //    {
                    //        Results.RecordExchangeResponse(response2, exchange2);

                    //        response2.EnsureSuccessStatusCode();
                    //        string outJson2 = await response2.Content.ReadAsStringAsync();

                    //        Stats resultRoot2C = JsonConvert.DeserializeObject<Stats>(outJson2);

                    //        Results.ResultItems.AddRange(resultRoot2C.value);

                    //        if (resultRoot2C.count == 100)
                    //        {
                    //            hasMoreResults = true;
                    //            itemsToSkip += 100;
                    //        }
                    //        else
                    //        {
                    //            hasMoreResults = false;
                    //        }
                    //    }
                    //}

                    Results.Count = Results.ResultItems.Count;
                }
            }

            Log.DOMAIN("Exit(Project)", Common.LOG_CATEGORY, startTicks);

            return Results;
        }
    }
}
