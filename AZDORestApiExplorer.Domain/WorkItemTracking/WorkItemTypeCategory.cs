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
        public class GetWorkItemTypeCategoriesEvent : PubSubEvent<GetWorkItemTypeCategoriesEventArgs> { }

        public class GetWorkItemTypeCategoriesEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;
        }
        public class SelectedWorkItemTypeCategoryChangedEvent : PubSubEvent<WorkItemTypeCategory> { }
    }

    public class WorkItemTypeCategoriesRoot
    {
        public int count { get; set; }
        public WorkItemTypeCategory[] value { get; set; }
    }

    public class WorkItemTypeCategory
    {
        public string name { get; set; }
        public string referenceName { get; set; }
        public Defaultworkitemtype defaultWorkItemType { get; set; }
        public Workitemtype[] workItemTypes { get; set; }
        public string url { get; set; }

        public class Defaultworkitemtype
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public class Workitemtype
        {
            public string name { get; set; }
            public string url { get; set; }
        }

        public RESTResult<WorkItemTypeCategory> Results { get; set; } = new RESTResult<WorkItemTypeCategory>();

        public async Task<RESTResult<WorkItemTypeCategory>> GetList(GetWorkItemTypeCategoriesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Process)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + "wit/workitemtypecategories"
                    + "?api-version=4.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    WorkItemTypeCategoriesRoot resultRoot = JsonConvert.DeserializeObject<WorkItemTypeCategoriesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<WorkItemTypeCategory>(resultRoot.value);

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
