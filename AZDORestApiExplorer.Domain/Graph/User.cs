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
        public class GetUsersEvent : PubSubEvent<GetUsersEventArgs> { }

        public class GetUsersEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            // public Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedUserChangedEvent : PubSubEvent<User> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class User

    public class UsersRoot
    {
        public int count { get; set; }
        public User[] value { get; set; }
    }

    public class User
    {

            public string subjectKind { get; set; }
            public string domain { get; set; }
            public string principalName { get; set; }
            public string mailAddress { get; set; }
            public string origin { get; set; }
            public string originId { get; set; }
            public string displayName { get; set; }
            public _Links _links { get; set; }
            public string url { get; set; }
            public string descriptor { get; set; }
            public string directoryAlias { get; set; }
            public string metaType { get; set; }


        public class _Links
        {
            public Self self { get; set; }
            public Memberships memberships { get; set; }
            public Membershipstate membershipState { get; set; }
            public Storagekey storageKey { get; set; }
            public Avatar avatar { get; set; }
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

        public class Avatar
        {
            public string href { get; set; }
        }

        public RESTResult<User> Results { get; set; } = new RESTResult<User>();

        public async Task<RESTResult<User>> GetList(GetUsersEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(User)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.VsSpsUri}/_apis/"
                    + $"graph/users?"
                    + "api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    UsersRoot resultRoot = JsonConvert.DeserializeObject<UsersRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<User>(resultRoot.value);

                    // TODO(crhodes)
                    // Remove this if not using continuationHeaders

                    #region ContinuationHeaders

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    while (hasContinuationToken)
                    {
                        string continueToken = continuationHeaders.First();

                        var requestUri2 = $"{args.Organization.VsSpsUri}/_apis/"
                            + $"graph/users?"
                            + $"continuationToken={continueToken}"
                            + "&api-version=6.1-preview.1";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            UsersRoot resultRootC = JsonConvert.DeserializeObject<UsersRoot>(outJson2);

                            Results.ResultItems.AddRange(resultRootC.value);

                            hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                        }
                    }

                    #endregion

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(User)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
