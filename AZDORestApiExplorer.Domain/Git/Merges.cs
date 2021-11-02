using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

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
        public class GetMergesEvent : PubSubEvent<GetMergesEventArgs> { }

        public class GetMergesEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;

            public Domain.Git.GitRepository Repository;

            public Merge Merge;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedMergeChangedEvent : PubSubEvent<Merge> { }
    }
        public class Merges
    {
        public int count { get; set; }
        public Merge[] value { get; set; }
    }

    public class Merge
    {
        public int id { get; set; }

        public RESTResult<Merge> Results { get; set; } = new RESTResult<Merge>();

        public async Task<RESTResult<Merge>> GetList(GetMergesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(ImportRequest)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // TODO(crhodes)
                // Need to figure out how to get Merge Id

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/merges/{args.Merge.id}?"
                    + "api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    JObject o = JObject.Parse(outJson);

                    Merges resultRoot = JsonConvert.DeserializeObject<Merges>(outJson);

                     Results.ResultItems = new ObservableCollection<Merge>(resultRoot.value);
                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(Project)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}