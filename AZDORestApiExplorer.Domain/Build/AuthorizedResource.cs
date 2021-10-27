using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Build.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Build
{
    namespace Events
    {
        public class GetAuthorizedResourcesEvent : PubSubEvent<GetAuthorizedResourcesEventArgs> { }

        public class GetAuthorizedResourcesEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedAuthorizedResourceChangedEvent : PubSubEvent<AuthorizedResource> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class AuthorizedResource

    public class AuthorizedResourcesRoot
    {
        public int count { get; set; }
        public AuthorizedResource[] value { get; set; }
    }

    public class AuthorizedResource
    {
            public string type { get; set; }
            public string id { get; set; }
            public bool authorized { get; set; }
            public string name { get; set; }


        public RESTResult<AuthorizedResource> Results { get; set; } = new RESTResult<AuthorizedResource>();

        public async Task<RESTResult<AuthorizedResource>> GetList(GetAuthorizedResourcesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(AuthorizedResource)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"build/authorizedresources?"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    AuthorizedResourcesRoot resultRoot = JsonConvert.DeserializeObject<AuthorizedResourcesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<AuthorizedResource>(resultRoot.value);

                    //IEnumerable<string> continuationHeaders = default;

                    //bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(AuthorizedResource)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
