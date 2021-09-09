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
        public class GetTemplatesEvent : PubSubEvent<GetTemplatesEventArgs> { }

        public class GetTemplatesEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;

            public Domain.Core.Team Team;
        }

        public class SelectedTemplateChangedEvent : PubSubEvent<Template> { }
    }


    public class TemplatesRoot
    {
        public int count { get; set; }
        public object[] value { get; set; }
    }

    public class Template
    {

        public RESTResult<Template> Results { get; set; } = new RESTResult<Template>();

        public async Task<RESTResult<Template>> GetList(GetTemplatesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Process)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);
                //https://dev.azure.com/{organization}/{project}/{team}/_apis/wit/templates?api-version=6.1-preview.1
                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/{args.Team.id}/_apis/"
                    + "wit/templates"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    TemplatesRoot resultRoot = JsonConvert.DeserializeObject<TemplatesRoot>(outJson);

                    //Results.ResultItems = new ObservableCollection<Template>(resultRoot.value);

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
