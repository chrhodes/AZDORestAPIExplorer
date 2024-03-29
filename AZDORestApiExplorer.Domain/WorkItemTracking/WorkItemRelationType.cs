﻿using System;
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
        public class GetWorkItemRelationTypesEvent : PubSubEvent<GetWorkItemRelationTypesEventArgs> { }

        public class GetWorkItemRelationTypesEventArgs
        {
            public Organization Organization;

            // public Process Process;

            // public WorkItemType WorkItemType;

        }
        public class SelectedWorkItemRelationTypeChangedEvent : PubSubEvent<WorkItemRelationType> { }
    }

    public class WorkItemRelationTypesRoot
    {
        public int count { get; set; }
        public WorkItemRelationType[] value { get; set; }
    }
    public class WorkItemRelationType
    {
        public Attributes attributes { get; set; }
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        public class Attributes
        {
            public string usage { get; set; }
            public bool editable { get; set; }
            public bool enabled { get; set; }
            public bool acyclic { get; set; }
            public bool directional { get; set; }
            public bool singleTarget { get; set; }
            public string topology { get; set; }
            public bool remote { get; set; }
            public bool isForward { get; set; }
            public string oppositeEndReferenceName { get; set; }
        }

        public RESTResult<WorkItemRelationType> Results { get; set; } = new RESTResult<WorkItemRelationType>();

        public async Task<RESTResult<WorkItemRelationType>> GetList(GetWorkItemRelationTypesEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Process)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/_apis/"
                    + "wit/workitemrelationtypes"
                    + "?api-version=4.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    WorkItemRelationTypesRoot resultRoot = JsonConvert.DeserializeObject<WorkItemRelationTypesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<WorkItemRelationType>(resultRoot.value);

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
