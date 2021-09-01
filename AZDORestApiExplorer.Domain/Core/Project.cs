using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core.Events;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;

using VNC;

namespace AZDORestApiExplorer.Domain.Core
{
    namespace Events
    {
        public class GetProjectsEvent : PubSubEvent<GetProjectsEventArgs> { }

        public class GetProjectsEventArgs
        {
            public Organization Organization;
        }

        public class SelectedProjectChangedEvent : PubSubEvent<Project> { }
    }

    public class ProjectsRoot
    {
        public int count { get; set; }
        public Project[] value { get; set; }
    }

    public class Project
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string state { get; set; }
        public int revision { get; set; }
        public string visibility { get; set; }
        public DateTime lastUpdateTime { get; set; }
        public string description { get; set; }

        public RESTResult<Project> Results { get; set; } = new RESTResult<Project>();

        public string CallMe()
        {
            return "You Called Project!";
        }

        public async Task<RESTResult<Project>> GetList(GetProjectsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Project)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(args.Organization, client);

                var requestUri = $"{args.Organization.Uri}/_apis/"
                    + "projects?"
                    + "api-version=6.1-preview.4";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    //RecordExchangeResponse(response, exchange);
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    ProjectsRoot resultRoot = JsonConvert.DeserializeObject<ProjectsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Project>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    while (hasContinuationToken)
                    {
                        //RequestResponseInfo exchange2 = new RequestResponseInfo();

                        string continueToken = continuationHeaders.First();

                        string requestUri2 = $"{args.Organization.Uri}/_apis/"
                            + "projects?"
                            + $"continuationToken={continueToken}"
                            + "&api-version=6.1-preview.4";

                        var exchange2 = Results.ContinueExchange(client, requestUri2);

                        //exchange2.Uri = requestUri2;
                        //exchange2.RequestHeadersX.AddRange(client.DefaultRequestHeaders);

                        using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                        {
                        //    exchange2.Response = response2;
                        //    exchange2.ResponseHeadersX.AddRange(response2.Headers);

                            //RequestResponseExchange.Add(exchange2);

                            Results.RecordExchangeResponse(response2, exchange2);

                            response2.EnsureSuccessStatusCode();
                            string outJson2 = await response2.Content.ReadAsStringAsync();

                            ProjectsRoot projects2C = JsonConvert.DeserializeObject<ProjectsRoot>(outJson2);

                            Results.ResultItems.AddRange(projects2C.value);

                            hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                        }
                    }

                    Results.Count = Results.ResultItems.Count();
                }
            }

            Log.DOMAIN("Exit(Project)", Common.LOG_CATEGORY, startTicks);

            return Results;
        }

        //// TODO(crhodes)
        //// Put this somewhere common.  Maybe VNC.HttpHelper

        //public static void InitializeHttpClient(Organization collection, HttpClient client)
        //{
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));

        //    //var username = @"Christopher.Rhodes@bd.com";
        //    //var password = @"HappyH0jnacki08";

        //    //string base64PAT = Convert.ToBase64String(
        //    //        ASCIIEncoding.ASCII.GetBytes($"{username}:{password}"));
        //    string base64PAT = Convert.ToBase64String(
        //            ASCIIEncoding.ASCII.GetBytes(
        //                string.Format("{0}:{1}", "", collection.PAT)));

        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64PAT);
        //    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("NTLM");
        //}
    }
}
