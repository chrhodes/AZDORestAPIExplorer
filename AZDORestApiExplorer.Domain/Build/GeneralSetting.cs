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
        public class GetGeneralSettingsEvent : PubSubEvent<GetGeneralSettingsEventArgs> { }

        public class GetGeneralSettingsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            public Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedGeneralSettingChangedEvent : PubSubEvent<GeneralSetting> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class GeneralSetting

    public class GeneralSettingsRoot
    {
        //public int count { get; set; }
        //public GeneralSetting[] value { get; set; }

        public bool enforceReferencedRepoScopedToken { get; set; }
        public bool statusBadgesArePrivate { get; set; }
        public bool enforceSettableVar { get; set; }
        public bool enforceJobAuthScope { get; set; }
        public bool enforceJobAuthScopeForReleases { get; set; }
        public bool publishPipelineMetadata { get; set; }
    }

    public class GeneralSetting
    {
        public bool enforceReferencedRepoScopedToken { get; set; }
        public bool statusBadgesArePrivate { get; set; }
        public bool enforceSettableVar { get; set; }
        public bool enforceJobAuthScope { get; set; }
        public bool enforceJobAuthScopeForReleases { get; set; }
        public bool publishPipelineMetadata { get; set; }

        public RESTResult<GeneralSetting> Results { get; set; } = new RESTResult<GeneralSetting>();

        public async Task<RESTResult<GeneralSetting>> GetList(GetGeneralSettingsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(GeneralSetting)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                    + $"build/generalsettings"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    GeneralSettingsRoot resultRoot = JsonConvert.DeserializeObject<GeneralSettingsRoot>(outJson);

                    // NOTE(crhodes)
                    // There is only one item coming back.  Hack it into the collection.

                    //Results.ResultItems = new ObservableCollection<Build>(resultRoot.value);

                    Results.ResultItems = new ObservableCollection<GeneralSetting>();

                    Results.ResultItems.Add(this);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(GeneralSetting)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
