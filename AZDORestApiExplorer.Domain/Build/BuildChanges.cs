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
        public class GetBuildChangesEvent : PubSubEvent<GetBuildChangesEventArgs> { }

        public class GetBuildChangesEventArgs
        {
            public Organization Organization;

            public Core.Project Project;

            public Build Build;
        }

        public class SelectedBuildChangeChangedEvent : PubSubEvent<BuildChange> { }
    }

    public class BuildChanges
    {
        public int count { get; set; }
        public BuildChange[] value { get; set; }
    }

    public class BuildChange
    {
        public string id { get; set; }
        public string message { get; set; }
        public string type { get; set; }
        public Author author { get; set; }
        public DateTime timestamp { get; set; }
        public string location { get; set; }
        public string displayUri { get; set; }
        public string pusher { get; set; }

        #region Nested Classes

        public class Author
        {
            public string displayName { get; set; }
            public _Links _links { get; set; }
            public object id { get; set; }
            public string uniqueName { get; set; }
        }

        public class _Links
        {
            public Avatar avatar { get; set; }
        }

        public class Avatar
        {
            public string href { get; set; }
        }

        #endregion

        public RESTResult<BuildChange> Results { get; set; } = new RESTResult<BuildChange>();

        public async Task<RESTResult<BuildChange>> GetList(GetBuildChangesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(BuildChange)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // GET https://dev.azure.com/{organization}/{project}/_apis/build/builds/{buildId}/changes?api-version=6.1-preview.2
                // GET https://dev.azure.com/{organization}/{project}/_apis/build/builds/{buildId}/changes?continuationToken={continuationToken}&$top={$top}&includeSourceChange={includeSourceChange}&api-version=6.1-preview.2
                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"build/builds/{args.Build.id}/changes?"
                    + "api-version=6.1-preview.2";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    BuildChanges resultRoot = JsonConvert.DeserializeObject<BuildChanges>(outJson);

                    Results.ResultItems = new ObservableCollection<BuildChange>(resultRoot.value);

                    #region Paging Support

                    #region ContinuationHeaders

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    // NOTE(crhodes)
                    // For some reason can only get 200 changes back???

                    while (hasContinuationToken)
                    {
                        string continueToken = continuationHeaders.First();

                        var requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                            + $"build/builds/{args.Build.id}/changes?"
                            + $"continuationToken={continueToken}"
                            + "&api-version=6.1-preview.2";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            BuildChanges resultRootC = JsonConvert.DeserializeObject<BuildChanges>(outJson2);

                            Results.ResultItems.AddRange(resultRootC.value);

                            hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                        }
                    }

                    #endregion

                    #endregion

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(BuildChange)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
