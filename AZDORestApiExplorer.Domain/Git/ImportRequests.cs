using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Core;
using AZDORestApiExplorer.Domain.Git.Events;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;

using VNC;
using VNC.Core.Net;
namespace AZDORestApiExplorer.Domain.Git
{
    namespace Events
    {
        public class GetImportRequestsEvent : PubSubEvent<GetImportRequestsEventArgs> { }

        public class GetImportRequestsEventArgs
        {
            public Organization Organization;

            public Domain.Core.Project Project;

            public Domain.Git.GitRepository Repository;
        }

        public class SelectedImportRequestChangedEvent : PubSubEvent<ImportRequest> { }
    }

    public class ImportRequests
    {
        public int count { get; set; }
        public ImportRequest[] value { get; set; }
    }

    public class ImportRequest
    {
        public int importRequestId { get; set; }
        public GitRepository repository { get; set; }
        public Parameters parameters { get; set; }
        public string status { get; set; }
        public Detailedstatus detailedStatus { get; set; }
        public _Links _links { get; set; }
        public string url { get; set; }

        #region Nested Classes

        public class Parameters
        {
            public object tfvcSource { get; set; }
            public Gitsource gitSource { get; set; }
            public bool deleteServiceEndpointAfterImportIsDone { get; set; }
        }

        public class Gitsource
        {
            public string url { get; set; }
            public bool overwrite { get; set; }
        }

        public class Detailedstatus
        {
            public int currentStep { get; set; }
            public string[] allSteps { get; set; }
        }

        public class _Links
        {
            public Self self { get; set; }
            public Repository1 repository { get; set; }
        }

        public class Self
        {
            public string href { get; set; }
        }

        public class Repository1
        {
            public string href { get; set; }
        }

        #endregion

        public RESTResult<ImportRequest> Results { get; set; } = new RESTResult<ImportRequest>();

        public async Task<RESTResult<ImportRequest>> GetList(GetImportRequestsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(ImportRequest)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"git/repositories/{args.Repository.id}/importRequests?"
                    + "api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    JObject o = JObject.Parse(outJson);

                    ImportRequests resultRoot = JsonConvert.DeserializeObject<ImportRequests>(outJson);

                    Results.ResultItems = new ObservableCollection<ImportRequest>(resultRoot.value);
                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(Project)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
