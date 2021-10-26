
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

using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Build
{
    namespace Events
    {
        public class GetBuildsEvent : PubSubEvent<GetBuildsEventArgs> { }

        public class GetBuildsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            // public Domain.Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedBuildChangedEvent : PubSubEvent<Build> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Build

    public class BuildsRoot
    {
        public int count { get; set; }
        public Build[] value { get; set; }
    }

    public class Build
    {
        public RESTResult<Build> Results { get; set; } = new RESTResult<Build>();

        public async Task<RESTResult<Build>> GetList(GetBuildsEventArgs args)
        {
            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // TODO(crhodes)
                // Update Uri  Use args for parameters.
                var requestUri = $"{args.Organization.Uri}/_apis/"
                    + $"<UPDATE URI>"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    BuildsRoot resultRoot = JsonConvert.DeserializeObject<BuildsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Build>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }

                return Results;
            }
        }
    }
}
