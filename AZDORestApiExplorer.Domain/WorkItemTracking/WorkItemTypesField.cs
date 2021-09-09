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
        public class GetWorkItemTypesFieldsEvent : PubSubEvent<GetWorkItemTypesFieldsEventArgs> { }

        public class GetWorkItemTypesFieldsEventArgs
        {
            public Domain.Organization Organization;

            public Domain.Core.Project Project;

            public Domain.WorkItemTracking.WorkItemType WorkItemType;

            string QueryString;
        }

        public class SelectedWorkItemTypesFieldChangedEvent : PubSubEvent<WorkItemTypesField> { }
    }

    public class WorkItemTypesFieldsRoot
    {
        public int count { get; set; }
        public WorkItemTypesField[] value { get; set; }
    }

    public class WorkItemTypesField
    {
        public string defaultValue { get; set; }
        public string[] allowedValues { get; set; }
        public string helpText { get; set; }
        public bool alwaysRequired { get; set; }
        public Dependentfield[] dependentFields { get; set; }
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        public class Dependentfield
        {
            public string referenceName { get; set; }
            public string name { get; set; }
            public string url { get; set; }
        }

        public RESTResult<WorkItemTypesField> Results { get; set; } = new RESTResult<WorkItemTypesField>();

        public async Task<RESTResult<WorkItemTypesField>> GetList(GetWorkItemTypesFieldsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Process)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"wit/workitemtypes/{args.WorkItemType.referenceName}/fields"
                    + "?$Expand=All&api-version=6.0";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    WorkItemTypesFieldsRoot resultRoot = JsonConvert.DeserializeObject<WorkItemTypesFieldsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<WorkItemTypesField>(resultRoot.value);

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
