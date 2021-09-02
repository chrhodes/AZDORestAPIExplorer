using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Artifacts.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Artifacts
{
    namespace Events
    {
        public class GetFeedsEvent : PubSubEvent<GetFeedsEventArgs> { }

        public class GetFeedsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            // public Domain.Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedFeedChangedEvent : PubSubEvent<Feed> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Feed

    public class FeedsRoot
    {
        public int count { get; set; }
        public Feed[] value { get; set; }
    }

    public class Feed
    {
        // TODO(crhodes)
        // Keep these for GetList support

        public RESTResult<Feed> Results { get; set; } = new RESTResult<Feed>();

        public async Task<RESTResult<Feed>> GetList(GetFeedsEventArgs args)
        {
            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // TODO(crhodes)
                // Update Uri  Use args for parameters.
                var requestUri = $"{args.Organization.Uri}/_apis/"
                    + $"<UPDATE URI>"
                    + "?api-version=6.1-preview.1";

                //var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    //Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    FeedsRoot resultRoot = JsonConvert.DeserializeObject<FeedsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Feed>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }

                return Results;
            }
        }

        // TODO(crhodes)
        // Need to extract this

        public static void InitializeHttpClient(Organization collection, HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //var username = @"Christopher.Rhodes@bd.com";
            //var password = @"HappyH0jnacki08";

            //string base64PAT = Convert.ToBase64String(
            //        ASCIIEncoding.ASCII.GetBytes($"{username}:{password}"));
            string base64PAT = Convert.ToBase64String(
                    ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", collection.PAT)));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64PAT);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("NTLM");
        }
    }

}
