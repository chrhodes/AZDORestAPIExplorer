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
        public class GetPushesEvent : PubSubEvent<GetPushesEventArgs> { }

        public class GetPushesEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;

            public Domain.Git.GitRepository Repository;
        }

        public class SelectedPushChangedEvent : PubSubEvent<Push> { }
    }

    public class Pushes
    {
        public int count { get; set; }
        public Push[] value { get; set; }
    }

    public class Push
    {
        public GitRepository repository { get; set; }
        public Pushedby pushedBy { get; set; }
        public int pushId { get; set; }
        public DateTime date { get; set; }
        public string url { get; set; }

        #region Nested Classes

        public class Pushedby
        {
            public string displayName { get; set; }
            public string url { get; set; }
            public _Links _links { get; set; }
            public string id { get; set; }
            public string uniqueName { get; set; }
            public string imageUrl { get; set; }
            public string descriptor { get; set; }
        }

        public class _Links
        {
            public Avatar avatar { get; set; }
        }

        public class Avatar
        {
            public string href { get; set; }
        }

        #endregion

        public RESTResult<Push> Results { get; set; } = new RESTResult<Push>();

        public async Task<RESTResult<Push>> GetList(GetPushesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Repository)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pushes?api-version=6.1-preview.2
                // GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pushes?
                //          $skip={$skip}
                //          &$top={$top}
                //          &searchCriteria.fromDate={searchCriteria.fromDate}
                //          &searchCriteria.includeLinks={searchCriteria.includeLinks}
                //          &searchCriteria.includeRefUpdates={searchCriteria.includeRefUpdates}
                //          &searchCriteria.pusherId={searchCriteria.pusherId}
                //          &searchCriteria.refName={searchCriteria.refName}
                //          &searchCriteria.toDate={searchCriteria.toDate}
                //          &api-version=6.1-preview.2

                // GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pushes/{pushId}?api-version=6.1-preview.2
                // GET https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pushes/{pushId}?
                //          includeCommits={includeCommits}
                //          &includeRefUpdates={includeRefUpdates}
                //          &api-version=6.1-preview.2

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/pushes?$top=100"
                    + "&api-version=6.1-preview.2";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    Pushes resultRoot = JsonConvert.DeserializeObject<Pushes>(outJson);

                    Results.ResultItems = new ObservableCollection<Push>(resultRoot.value);

                    bool hasMoreResults = false;

                    if (resultRoot.count == 100)
                    {
                        hasMoreResults = true;
                    }

                    int itemsToSkip = 100;

                    while (hasMoreResults)
                    {
                        var requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                            + $"git/repositories/{args.Repository.id}/pushes?$top=100&$skip={itemsToSkip}"
                            + "&api-version=6.1-preview.1";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            Pushes resultRoot2C = JsonConvert.DeserializeObject<Pushes>(outJson2);

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
    }
}
