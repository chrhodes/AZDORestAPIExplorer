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
        public class GetBuildWorkItemRefsEvent : PubSubEvent<GetBuildWorkItemRefsEventArgs> { }

        public class GetBuildWorkItemRefsEventArgs
        {
            public Organization Organization;

            public Core.Project Project;

            public Build Build;
        }

        public class SelectedBuildWorkItemRefChangedEvent : PubSubEvent<BuildWorkItemRef> { }
    }

    public class BuildWorkItemRefs
    {
        public int count { get; set; }
        public BuildWorkItemRef[] value { get; set; }
    }

    public class BuildWorkItemRef
    {
        public string id { get; set; }
        public string url { get; set; }

        #region Nested Classes

        #endregion

        public RESTResult<BuildWorkItemRef> Results { get; set; } = new RESTResult<BuildWorkItemRef>();

        public async Task<RESTResult<BuildWorkItemRef>> GetList(GetBuildWorkItemRefsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(BuildWorkItemRef)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // GET https://dev.azure.com/{organization}/{project}/_apis/build/builds/{buildId}/workitems?api-version=6.1-preview.2
                // GET https://dev.azure.com/{organization}/{project}/_apis/build/builds/{buildId}/workitems?$top={$top}&api-version=6.1-preview.2

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"build/builds/{args.Build.id}/workitems?$top=250"
                    + "&api-version=6.1-preview.2";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    BuildWorkItemRefs resultRoot = JsonConvert.DeserializeObject<BuildWorkItemRefs>(outJson);

                    Results.ResultItems = new ObservableCollection<BuildWorkItemRef>(resultRoot.value);

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

                            BuildWorkItemRefs resultRootC = JsonConvert.DeserializeObject<BuildWorkItemRefs>(outJson2);

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

                    //int itemsToSkip = 100;

                    //while (hasMoreResults)
                    //{
                    //    var requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    //        + $"<UPDATE URI>$top=100&$skip={itemsToSkip}"
                    //        + "&api-version=6.1-preview.1";

                    //    var exchange2 = Results.ContinueExchange(client, requestUri2);

                    //    using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                    //    {
                    //        Results.RecordExchangeResponse(response2, exchange2);

                    //        response2.EnsureSuccessStatusCode();
                    //        string outJson2 = await response2.Content.ReadAsStringAsync();

                    //        BuildWorkItemRefs resultRoot2C = JsonConvert.DeserializeObject<BuildWorkItemRefs>(outJson2);

                    //        Results.ResultItems.AddRange(resultRoot2C.value);

                    //        if (resultRoot2C.count == 100)
                    //        {
                    //            hasMoreResults = true;
                    //            itemsToSkip += 100;
                    //        }
                    //        else
                    //        {
                    //            hasMoreResults = false;
                    //        }
                    //    }
                    //}

                    #endregion

                    #endregion

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(BuildWorkItemRef)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
