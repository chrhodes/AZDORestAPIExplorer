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
        public class GetBuildLogsEvent : PubSubEvent<GetBuildLogsEventArgs> { }

        public class GetBuildLogsEventArgs
        {
            public Organization Organization;

            public Core.Project Project;

            public Build Build;
        }

        public class SelectedBuildLogChangedEvent : PubSubEvent<BuildLog> { }
    }

    public class BuildLogs
    {
        public int count { get; set; }
        public BuildLog[] value { get; set; }
    }

    public class BuildLog
    {
        public int lineCount { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime lastChangedOn { get; set; }
        public int id { get; set; }
        public string type { get; set; }
        public string url { get; set; }

        public RESTResult<BuildLog> Results { get; set; } = new RESTResult<BuildLog>();

        public async Task<RESTResult<BuildLog>> GetList(GetBuildLogsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(BuildLog)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"build/builds/{args.Build.id}/logs?"
                    + "api-version=6.1-preview.2";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    BuildLogs resultRoot = JsonConvert.DeserializeObject<BuildLogs>(outJson);

                    Results.ResultItems = new ObservableCollection<BuildLog>(resultRoot.value);

                    #region Paging Support

                    // TODO(crhodes)

                    // If call has some kind of paging support ...
                    // otherwise remove all of this.

                    // Calls that support paging or partial results typically use one of these
                    //  continuationtoken in the header
                    //  top and skip

                    // Remove this if not using continuationHeaders

                    #region ContinuationHeaders

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    while (hasContinuationToken)
                    {
                        string continueToken = continuationHeaders.First();

                        var requestUri2 = $"{args.Organization.Uri}/_apis/"
                            + $"<UPDATE URI>"
                            + $"continuationToken={continueToken}"
                            + "?api-version=6.1-preview.1";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            BuildLogs resultRootC = JsonConvert.DeserializeObject<BuildLogs>(outJson2);

                            Results.ResultItems.AddRange(resultRootC.value);

                            hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                        }
                    }

                    #endregion

                    // TODO(crhodes)
                    // Remove this if not using Top and Skip

                    #region Top and Skip

                    bool hasMoreResults = false;

                    if (resultRoot.count == 100)
                    {
                        hasMoreResults = true;
                    }

                    int itemsToSkip = 100;

                    while (hasMoreResults)
                    {
                        var requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                            + $"<UPDATE URI>$top=100&$skip={itemsToSkip}"
                            + "&api-version=6.1-preview.1";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            BuildLogs resultRoot2C = JsonConvert.DeserializeObject<BuildLogs>(outJson2);

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

                    #endregion

                    #endregion

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(BuildLog)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
