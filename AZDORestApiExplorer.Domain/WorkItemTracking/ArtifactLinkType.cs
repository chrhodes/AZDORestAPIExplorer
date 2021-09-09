using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.WorkItemTracking.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    namespace Events
    {
        public class GetArtifactLinkTypesEvent : PubSubEvent<GetArtifactLinkTypesEventArgs> { }

        public class GetArtifactLinkTypesEventArgs
        {
            public Organization Organization;

            public Core.Project Project;
        }

        public class SelectedArtifactLinkTypeChangedEvent : PubSubEvent<ArtifactLinkType> { }
    }

    public class ArtifactLinkTypesRoot
    {
        public int count { get; set; }
        public ArtifactLinkType[] value { get; set; }
    }

    public class ArtifactLinkType
    {
        public string linkType { get; set; }
        public string artifactType { get; set; }
        public string toolType { get; set; }

        public RESTResult<ArtifactLinkType> Results { get; set; } = new RESTResult<ArtifactLinkType>();

        public async Task<RESTResult<ArtifactLinkType>> GetList(GetArtifactLinkTypesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Process)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/_apis/"
                    + "wit/artifactlinktypes"
                    + "?api-version=4.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    ArtifactLinkTypesRoot resultRoot = JsonConvert.DeserializeObject<ArtifactLinkTypesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<ArtifactLinkType>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count();
                }

                Log.DOMAIN("Exit(Process)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
