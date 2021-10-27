using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Build.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Build
{
    namespace Events
    {
        public class GetOptionsEvent : PubSubEvent<GetOptionsEventArgs> { }

        public class GetOptionsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedOptionChangedEvent : PubSubEvent<Option> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Option

    public class OptionsRoot
    {
        public int count { get; set; }
        public Option[] value { get; set; }
    }

    public class Option
    {
        public int ordinal { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Input[] inputs { get; set; }
        public object[] groups { get; set; }
        public string id { get; set; }

        public class Input
        {
            public Options options { get; set; }
            public Help help { get; set; }
            public string name { get; set; }
            public string label { get; set; }
            public string defaultValue { get; set; }
            public string type { get; set; }
            public bool required { get; set; }
        }

        public class Options
        {
            public string Bug { get; set; }
            public string Feature { get; set; }
            public string Task { get; set; }
            public string TestCase { get; set; }
            public string UserStory { get; set; }
            public string Issue { get; set; }
            public string ProductionIssue { get; set; }
            public string Request { get; set; }
            public string UserNeeds { get; set; }
            public string Release { get; set; }
        }

        public class Help
        {
            public string markdown { get; set; }
        }

        public RESTResult<Option> Results { get; set; } = new RESTResult<Option>();

        public async Task<RESTResult<Option>> GetList(GetOptionsEventArgs args)
        { 
            Int64 startTicks = Log.DOMAIN("Enter(Option)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"build/options?"
                    + "?api-version=6.1-preview.2";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    OptionsRoot resultRoot = JsonConvert.DeserializeObject<OptionsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Option>(resultRoot.value);

                    Results.Count = Results.ResultItems.Count;
                }

            Log.DOMAIN("Exit(Option)", Common.LOG_CATEGORY, startTicks);

            return Results;
            }
        }
    }
}
