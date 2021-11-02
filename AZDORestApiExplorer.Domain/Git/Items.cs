using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Git.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;
namespace AZDORestApiExplorer.Domain.Git
{
    namespace Events
    {
        public class GetItemsEvent : PubSubEvent<GetItemsEventArgs> { }

        public class GetItemsEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;

            public Domain.Git.GitRepository Repository;

            public string ScopePath;

            public string RecursionLevel;
        }

        public class SelectedItemChangedEvent : PubSubEvent<Item> { }
    }
    public class Items
    {
        public int count { get; set; }
        public Item[] value { get; set; }
    }

    public class Item
    {
        public string objectId { get; set; }
        public string gitObjectType { get; set; }
        public string commitId { get; set; }
        public string path { get; set; }
        public bool isFolder { get; set; }
        public string url { get; set; }

        public RESTResult<Item> Results { get; set; } = new RESTResult<Item>();

        public async Task<RESTResult<Item>> GetList(GetItemsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Repository)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // TODO(crhodes)
                // Extend GetItemsEventArgs to add Optional Parameters, e.g. recursionLevel and scopePath
                // Clean up on way in or here.

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/items"
                    + $"?scopePath={args.ScopePath}&recursionLevel={args.RecursionLevel}"
                    + "&api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    Items resultRoot = JsonConvert.DeserializeObject<Items>(outJson);

                    Results.ResultItems = new ObservableCollection<Item>(resultRoot.value);

                    // NOTE(crhodes)
                    // Hard to believe there is not continuation mechanism.  Can be a lot of stuff.

                    //bool hasMoreResults = false;

                    //if (resultRoot.count == 100)
                    //{
                    //    hasMoreResults = true;
                    //}

                    //int itemsToSkip = 100;

                    //while (hasMoreResults)
                    //{
                    //    var requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    //        + $"git/repositories/{args.Repository.id}/pushes?$top=100&$skip={itemsToSkip}"
                    //        + "&api-version=6.1-preview.1";

                    //    var exchange2 = Results.ContinueExchange(client, requestUri2);

                    //    using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                    //    {
                    //        Results.RecordExchangeResponse(response2, exchange2);

                    //        response2.EnsureSuccessStatusCode();
                    //        string outJson2 = await response2.Content.ReadAsStringAsync();

                    //        Pushes resultRoot2C = JsonConvert.DeserializeObject<Pushes>(outJson2);

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

                    Results.Count = Results.ResultItems.Count;
                }
            }

            Log.DOMAIN("Exit(Project)", Common.LOG_CATEGORY, startTicks);

            return Results;
        }
    }
}
