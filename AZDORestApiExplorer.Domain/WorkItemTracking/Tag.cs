using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core.Events;
using AZDORestApiExplorer.Domain.WorkItemTracking.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.WorkItemTracking
{
    namespace Events
    {
        public class GetTagsEvent : PubSubEvent<GetTagsEventArgs> { }

        public class GetTagsEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;

        }
        public class SelectedTagChangedEvent : PubSubEvent<Tag> { }
    }

    public class TagsRoot
    {
        public int count { get; set; }
        public Tag[] value { get; set; }
    }
    public class Tag
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        public RESTResult<Tag> Results { get; set; } = new RESTResult<Tag>();

        public async Task<RESTResult<Tag>> GetList(GetTagsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Process)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + "/wit/tags"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    TagsRoot resultRoot = JsonConvert.DeserializeObject<TagsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Tag>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(Process)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
