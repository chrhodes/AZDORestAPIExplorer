using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Build.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Build
{
    namespace Events
    {
        public class GetResourcesEvent : PubSubEvent<GetResourcesEventArgs> { }

        public class GetResourcesEventArgs
        {
            public Organization Organization;
            public Core.Project Project;
            public Definition Definition;
        }

        public class SelectedResourceChangedEvent : PubSubEvent<Resource> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Resource

    public class ResourcesRoot
    {
        public int count { get; set; }
        public Resource[] value { get; set; }
    }

    public class Resource
    {
        public string type { get; set; }
        public string id { get; set; }
        public bool authorized { get; set; }
        public string name { get; set; }

        public RESTResult<Resource> Results { get; set; } = new RESTResult<Resource>();

        public async Task<RESTResult<Resource>> GetList(GetResourcesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Resource)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                //GET https://dev.azure.com/{organization}/{project}/_apis/build/definitions/{definitionId}/resources?api-version=6.1-preview.1
                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"build/definitions/{args.Definition.id}/resources?"
                    + "api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    ResourcesRoot resultRoot = JsonConvert.DeserializeObject<ResourcesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Resource>(resultRoot.value);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(Resource)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
