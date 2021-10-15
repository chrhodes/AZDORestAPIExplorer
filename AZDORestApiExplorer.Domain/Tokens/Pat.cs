using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Tokens.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Tokens
{
    namespace Events
    {
        public class GetPatsEvent : PubSubEvent<GetPatsEventArgs> { }

        public class GetPatsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            // public Domain.Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedPatChangedEvent : PubSubEvent<Pat> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Pat

    public class PatsRoot
    {
        public int count { get; set; }
        public Pat[] value { get; set; }
    }

    public class Pat
    {
        public RESTResult<Pat> Results { get; set; } = new RESTResult<Pat>();

        public async Task<RESTResult<Pat>> GetList(GetPatsEventArgs args)
        {
            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // TODO(crhodes)
                // Update Uri  Use args for parameters.
                var requestUri = $"{args.Organization.VsSpsUri}/_apis/"
                    + $"tokens/pats"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    PatsRoot resultRoot = JsonConvert.DeserializeObject<PatsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Pat>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }

                return Results;
            }
        }
    }
}
