using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Git.Events;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Git
{
    namespace Events
    {
        public class GetCommitChangesEvent : PubSubEvent<GetCommitChangesEventArgs> { }

        public class GetCommitChangesEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;

            public Domain.Git.GitRepository Repository;

            public Domain.Git.Commit Commit;
        }

        public class SelectedCommitChangeChangedEvent : PubSubEvent<CommitChange> { }
    }

    public class CommitChanges
    {
        public Changecounts changeCounts { get; set; }
        public CommitChange[] changes { get; set; }
    }

    public class Changecounts
    {
        public int Edit { get; set; }
        public int Add { get; set; }
    }

    public class CommitChange
    {
        public Item item { get; set; }
        public string changeType { get; set; }

        #region Nested Classes

        public class Item
        {
            public string objectId { get; set; }
            public string originalObjectId { get; set; }
            public string gitObjectType { get; set; }
            public string commitId { get; set; }
            public string path { get; set; }
            public bool isFolder { get; set; }
            public string url { get; set; }
        }

        #endregion

        public RESTResult<CommitChange> Results { get; set; } = new RESTResult<CommitChange>();

        public async Task<RESTResult<CommitChange>> GetList(GetCommitChangesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(ImportRequest)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/commits/{args.Commit.commitId}/changes"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    JObject o = JObject.Parse(outJson);

                    CommitChanges resultRoot = JsonConvert.DeserializeObject<CommitChanges>(outJson);

                    // TODO(crhodes)
                    // Figure out how to get Commitcounts back

                    Results.ResultItems = new ObservableCollection<CommitChange>(resultRoot.changes);
                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(Project)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }

}
