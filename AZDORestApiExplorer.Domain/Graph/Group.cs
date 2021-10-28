using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Graph.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Graph
{
    namespace Events
    {
        public class GetGroupsEvent : PubSubEvent<GetGroupsEventArgs> { }

        public class GetGroupsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            // public Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedGroupChangedEvent : PubSubEvent<Group> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Group

    public class GroupsRoot
    {
        public int count { get; set; }
        public Group[] value { get; set; }
    }

    public class Group
    {

            public string subjectKind { get; set; }
            public string description { get; set; }
            public string domain { get; set; }
            public string principalName { get; set; }
            public string mailAddress { get; set; }
            public string origin { get; set; }
            public string originId { get; set; }
            public string displayName { get; set; }
            public _Links _links { get; set; }
            public string url { get; set; }
            public string descriptor { get; set; }
            public bool isCrossProject { get; set; }


        public class _Links
        {
            public Self self { get; set; }
            public Memberships memberships { get; set; }
            public Membershipstate membershipState { get; set; }
            public Storagekey storageKey { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Memberships
        {
            public string href { get; set; }
        }

        public class Membershipstate
        {
            public string href { get; set; }
        }

        public class Storagekey
        {
            public string href { get; set; }
        }

        public RESTResult<Group> Results { get; set; } = new RESTResult<Group>();

        public async Task<RESTResult<Group>> GetList(GetGroupsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Group)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.VsSpsUri}/_apis/"
                    + $"graph/groups?"
                    + "api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    GroupsRoot resultRoot = JsonConvert.DeserializeObject<GroupsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Group>(resultRoot.value);

                    // TODO(crhodes)
                    // Remove this if not using continuationHeaders

                    #region ContinuationHeaders

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    while (hasContinuationToken)
                    {
                        string continueToken = continuationHeaders.First();

                        //GET https://vssps.dev.azure.com/{organization}/_apis/graph/groups?scopeDescriptor={scopeDescriptor}&subjectTypes={subjectTypes}&continuationToken={continuationToken}&api-version=6.0-preview.1
                        var requestUri2 = $"{args.Organization.VsSpsUri}/_apis/"
                            + $"graph/groups?"
                            + $"continuationToken={continueToken}"
                            + "&api-version=6.1-preview.1";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            GroupsRoot resultRootC = JsonConvert.DeserializeObject<GroupsRoot>(outJson2);

                            Results.ResultItems.AddRange(resultRootC.value);

                            hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                        }
                    }

                    #endregion

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(Group)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
