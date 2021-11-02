using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public class GetRefsEvent : PubSubEvent<GetRefsEventArgs> { }

        public class GetRefsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Domain.Core.Project Project;

            public Domain.Git.GitRepository Repository;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedRefChangedEvent : PubSubEvent<Ref> { }
    }

    public class Refs
    {
        public int count { get; set; }
        public Ref[] value { get; set; }
    }

    public class Ref
    {
        public string name { get; set; }
        public string objectId { get; set; }
        public Creator creator { get; set; }
        public string url { get; set; }
        public Islockedby isLockedBy { get; set; }
        public bool isLocked { get; set; }

        #region Nested Classes

        public class Creator
        {
            public string displayName { get; set; }
            public string url { get; set; }
            public _Links _links { get; set; }
            public string id { get; set; }
            public string uniqueName { get; set; }
            public string imageUrl { get; set; }
            public string descriptor { get; set; }
        }

        public class _Links
        {
            public Avatar avatar { get; set; }
        }

        public class Avatar
        {
            public string href { get; set; }
        }

        public class Islockedby
        {
            public string displayName { get; set; }
            public string url { get; set; }
            public _Links1 _links { get; set; }
            public string id { get; set; }
            public string uniqueName { get; set; }
            public string imageUrl { get; set; }
            public string descriptor { get; set; }
        }

        public class _Links1
        {
            public Avatar1 avatar { get; set; }
        }

        public class Avatar1
        {
            public string href { get; set; }
        }

        #endregion

        public RESTResult<Ref> Results { get; set; } = new RESTResult<Ref>();

    public async Task<RESTResult<Ref>> GetList(GetRefsEventArgs args)
    {
        Int64 startTicks = Log.DOMAIN("Enter(Repository)", Common.LOG_CATEGORY);

        using (HttpClient client = new HttpClient())
        {
            Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/refs?"
                    + "api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

            using (HttpResponseMessage response = await client.GetAsync(requestUri))
            {
                Results.RecordExchangeResponse(response, exchange);

                response.EnsureSuccessStatusCode();

                string outJson = await response.Content.ReadAsStringAsync();

                Refs resultRoot = JsonConvert.DeserializeObject<Refs>(outJson);

                Results.ResultItems = new ObservableCollection<Ref>(resultRoot.value);

                #region ContinuationHeaders

                IEnumerable<string> continuationHeaders = default;

                bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                while (hasContinuationToken)
                {
                    string continueToken = continuationHeaders.First();

                        var requestUri2 = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                            + $"git/repositories/{args.Repository.id}/refs?"
                            + $"continuationToken={continueToken}"
                            + "&api-version=6.1-preview.1";

                    var exchange2 = Results.ContinueExchange(client, requestUri2);

                    using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                    {
                        Results.RecordExchangeResponse(response2, exchange2);

                        response2.EnsureSuccessStatusCode();
                        string outJson2 = await response2.Content.ReadAsStringAsync();

                        Refs resultRootC = JsonConvert.DeserializeObject<Refs>(outJson2);

                        Results.ResultItems.AddRange(resultRootC.value);

                        hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                    }
                }

                #endregion

                Results.Count = Results.ResultItems.Count;
            }
        }

        Log.DOMAIN("Exit(Project)", Common.LOG_CATEGORY, startTicks);

        return Results;
    }
    }
}
