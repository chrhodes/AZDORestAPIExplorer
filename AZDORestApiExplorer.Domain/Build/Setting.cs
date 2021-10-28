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
        public class GetSettingsEvent : PubSubEvent<GetSettingsEventArgs> { }

        public class GetSettingsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedSettingChangedEvent : PubSubEvent<Setting> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Setting

    public class SettingsRoot
    {
        //public int count { get; set; }
        //public Setting[] value { get; set; }

        public Defaultretentionpolicy defaultRetentionPolicy { get; set; }
        public Maximumretentionpolicy maximumRetentionPolicy { get; set; }
        public int daysToKeepDeletedBuildsBeforeDestroy { get; set; }

        public class Defaultretentionpolicy
        {
            public string[] branches { get; set; }
            public object[] artifacts { get; set; }
            public string[] artifactTypesToDelete { get; set; }
            public int daysToKeep { get; set; }
            public int minimumToKeep { get; set; }
            public bool deleteBuildRecord { get; set; }
            public bool deleteTestResults { get; set; }
        }

        public class Maximumretentionpolicy
        {
            public string[] branches { get; set; }
            public object[] artifacts { get; set; }
            public string[] artifactTypesToDelete { get; set; }
            public int daysToKeep { get; set; }
            public int minimumToKeep { get; set; }
            public bool deleteBuildRecord { get; set; }
            public bool deleteTestResults { get; set; }
        }
    }

    public class Setting
    {
        public Defaultretentionpolicy defaultRetentionPolicy { get; set; }
        public Maximumretentionpolicy maximumRetentionPolicy { get; set; }
        public int daysToKeepDeletedBuildsBeforeDestroy { get; set; }

        public class Defaultretentionpolicy
        {
            public string[] branches { get; set; }
            public object[] artifacts { get; set; }
            public string[] artifactTypesToDelete { get; set; }
            public int daysToKeep { get; set; }
            public int minimumToKeep { get; set; }
            public bool deleteBuildRecord { get; set; }
            public bool deleteTestResults { get; set; }
        }

        public class Maximumretentionpolicy
        {
            public string[] branches { get; set; }
            public object[] artifacts { get; set; }
            public string[] artifactTypesToDelete { get; set; }
            public int daysToKeep { get; set; }
            public int minimumToKeep { get; set; }
            public bool deleteBuildRecord { get; set; }
            public bool deleteTestResults { get; set; }
        }

        public RESTResult<Setting> Results { get; set; } = new RESTResult<Setting>();

        public async Task<RESTResult<Setting>> GetList(GetSettingsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(Setting)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"build/settings?"
                    + "api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    //SettingsRoot resultRoot = JsonConvert.DeserializeObject<SettingsRoot>(outJson);

                    // NOTE(crhodes)
                    // There is only one item coming back.  Hack it into the collection.

                    //Results.ResultItems = new ObservableCollection<Build>(resultRoot.value);

                    Setting result = JsonConvert.DeserializeObject<Setting>(outJson);

                    Results.ResultItems = new ObservableCollection<Setting>();

                    Results.ResultItems.Add(result);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(Setting)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
