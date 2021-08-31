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

using VNC.HttpHelper;

namespace AZDORestApiExplorer.Domain.Core
{
    namespace Events
    {
        public class GetProcessesEvent : PubSubEvent<GetProcessesEventArgs> { }

        public class GetProcessesEventArgs
        {
            public Organization Organization;
        }

        public class SelectedProcessChangedEvent : PubSubEvent<Process> { }
    }

    public class ProcessesRoot
    {
        public int count { get; set; }
        public Process[] value { get; set; }
    }

    public class Process
    {
        public string id { get; set; }
        public string description { get; set; }
        public bool isDefault { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public string name { get; set; }

        public RESTResult<Process> Results { get; set; } = new RESTResult<Process>();

        public async Task<RESTResult<Process>> GetList(GetProcessesEventArgs args)
        {
            using (HttpClient client = new HttpClient())
            {
                InitializeHttpClient(args.Organization, client);

                var requestUri = $"{args.Organization.Uri}/_apis/"
                    + "process/processes?"
                    + "api-version=6.0-preview.1";

                //RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    //RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    ProcessesRoot resultRoot = JsonConvert.DeserializeObject<ProcessesRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Process>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count();
                }

                return Results;
            }
        }

        public static void InitializeHttpClient(Organization collection, HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //var username = @"Christopher.Rhodes@bd.com";
            //var password = @"HappyH0jnacki08";

            //string base64PAT = Convert.ToBase64String(
            //        ASCIIEncoding.ASCII.GetBytes($"{username}:{password}"));
            string base64PAT = Convert.ToBase64String(
                    ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", collection.PAT)));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64PAT);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("NTLM");
        }

    }
}
