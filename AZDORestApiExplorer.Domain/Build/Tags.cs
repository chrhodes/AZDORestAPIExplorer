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
        public class GetBuildTagsEvent : PubSubEvent<GetBuildTagsEventArgs> { }

        public class GetBuildTagsEventArgs
        {
            public Organization Organization;

            public Core.Project Project;
        }

        public class SelectedBuildTagChangedEvent : PubSubEvent<Tag> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Tag

    public class Tags
    {
        public int count { get; set; }
        public Tag[] value { get; set; }
    }

    public class Tag
    {
        #region Nested Classes

        #endregion

        public RESTResult<Tag> Results { get; set; } = new RESTResult<Tag>();

        public async Task<RESTResult<Tag>> GetList(GetBuildTagsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Tag)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"build/tags?"
                    + "?api-version=6.1-preview.3";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    Tags resultRoot = JsonConvert.DeserializeObject<Tags>(outJson);

                    Results.ResultItems = new ObservableCollection<Tag>(resultRoot.value);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(Tag)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
