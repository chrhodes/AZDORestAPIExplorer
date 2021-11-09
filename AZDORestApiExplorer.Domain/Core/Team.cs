using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Core
{
    namespace Events
    {
        public class GetTeamsEvent : PubSubEvent<GetTeamsEventArgs> { }

        public class GetTeamsEventArgs
        {
            public Organization Organization;
        }

        public class SelectedTeamChangedEvent : PubSubEvent<Team> { }
    }

    public class TeamsRoot
    {
        public int count { get; set; }
        public Team[] value { get; set; }
    }

    public class Team
    {
        public string description { get; set; }
        public string id { get; set; }
        public string identityUrl { get; set; }
        public string name { get; set; }
        public string projectId { get; set; }
        public string projectName { get; set; }

        public string url { get; set; }

        public RESTResult<Team> Results { get; set; } = new RESTResult<Team>();

        public string CallMe()
        {
            return "You Called Team!";
        }

        public async Task<RESTResult<Team>> GetList(GetTeamsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Team)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                //GET https://dev.azure.com/{organization}/_apis/projects/{projectId}/teams/{teamId}?api-version=6.1-preview.3
                //GET https://dev.azure.com/{organization}/_apis/projects/{projectId}/teams/{teamId}?$expandIdentity={$expandIdentity}&api-version=6.1-preview.3

                //GET https://dev.azure.com/{organization}/_apis/projects/{projectId}/teams?api-version=6.1-preview.3
                //GET https://dev.azure.com/{organization}/_apis/projects/{projectId}/teams?$mine={$mine}&$top={$top}&$skip={$skip}&$expandIdentity={$expandIdentity}&api-version=6.1-preview.3
                //GET https://dev.azure.com/{organization}/_apis/teams?api-version=6.1-preview.3
                //GET https://dev.azure.com/{organization}/_apis/teams?$mine={$mine}&$top={$top}&$skip={$skip}&$expandIdentity={$expandIdentity}&api-version=6.1-preview.3

                var requestUri = $"{args.Organization.Uri}/_apis/"
                    + "teams?$top=100"
                    + "&api-version=6.1-preview.3";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    TeamsRoot resultRoot = JsonConvert.DeserializeObject<TeamsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Team>(resultRoot.value);

                    bool hasMoreResults = false;

                    if (resultRoot.count == 100)
                    {
                        hasMoreResults = true;
                    }

                    int itemsToSkip = 100;

                    while (hasMoreResults)
                    {
                        var requestUri2 = $"{args.Organization.Uri}/_apis/"
                            + $"teams?$top=100&$skip={itemsToSkip}"
                            + "&api-version=6.1-preview.3";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            TeamsRoot resultRoot2C = JsonConvert.DeserializeObject<TeamsRoot>(outJson2);

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

                    Results.Count = Results.ResultItems.Count();
                }
            }

            Log.DOMAIN("Exit(Team)", Common.LOG_CATEGORY, startTicks);

            return Results;
        }
    }
}