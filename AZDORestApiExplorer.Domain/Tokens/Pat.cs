using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using AZDORestApiExplorer.Domain.Tokens.Events;

using Microsoft.Identity.Client;
using Newtonsoft.Json;

using Prism.Events;

using VNC.Core.Net;

namespace AZDORestApiExplorer.Domain.Tokens
{
    namespace Events
    {
        public class GetPatsEvent : PubSubEvent<GetPatsEventArgs> { }

        public class GetPatsEventArgs
        {
            public Organization Organization;

            // public Domain.Core.Process Process;

            // public Domain.Core.Project Project;

            // public Domain.Core.Team Team;

            // public WorkItemType WorkItemType;
        }

        public class SelectedPatChangedEvent : PubSubEvent<Pat> { }
    }

    // TODO(crhodes)
    // PasteSpecial from Json result text

    // Nest any additional classes inside class Pat

    public class PatsRoot
    {
        public int count { get; set; }
        public Pat[] value { get; set; }
    }

    public class Pat
    {
        public RESTResult<Pat> Results { get; set; } = new RESTResult<Pat>();

        public async Task<RESTResult<Pat>> GetList(GetPatsEventArgs args)
        {
            // HACK(crhodes)
            // Hard code this here until we get it working then pass it in with GetPatsEventArgs
            var tenantId = "94c3e67c-9e2d-4800-a6b7-635d97882165"; // AKA DirectoryID
            var clientId = "3412b8ff-fb04-4120-baa0-8189d32b497f"; // AKA ApplicationID

            string[] scopes = new string[] { "user.read" };

            using (HttpClient client = new HttpClient())
            {
                // HACK(crhodes)
                // Do in line for now, then move to RESTResult

                //Results.InitializeHttpClient(client, args.Organization.PAT);
                //Results.InitializeHttpClient2(client, tenantId, clientId);

                // NOTE(crhodes)
                // This displays the signon but fails with no redirect
                //IPublicClientApplication publicClientApp = PublicClientApplicationBuilder.Create(clientId)
                //    .WithDefaultRedirectUri()
                //    .WithAuthority(AzureCloudInstance.AzurePublic, tenantId)
                //    .Build();

                // NOTE(crhodes)
                // This is from Demo App
                // NB. .WithWindowsBroker() does not exist

                IPublicClientApplication publicClientApp = PublicClientApplicationBuilder.Create(clientId)
                    .WithAuthority(AzureCloudInstance.AzurePublic, tenantId)
                    .WithDefaultRedirectUri()
                    .WithExperimentalFeatures()
                    //.WithWindowsBroker(true)
                    .WithBroker(true)
                    .Build();

                // NOTE(crhodes)
                // This throws exception and does not display signon

                //IPublicClientApplication publicClientApp = PublicClientApplicationBuilder.Create(clientId)
                //    .WithAuthority(AzureCloudInstance.AzurePublic, tenantId)
                //    .Build();

                var accounts = await publicClientApp.GetAccountsAsync();
                var firstAccount = accounts.FirstOrDefault();
                AuthenticationResult authResult = null;

                try
                {
                    authResult = await publicClientApp.AcquireTokenSilent(scopes, firstAccount)
                        .ExecuteAsync();
                }
                catch (MsalUiRequiredException ex)
                {
                    // A MsalUiRequiredException happened on AcquireTokenSilent.
                    // This indicates you need to call AcquireTokenInteractive to acquire a token
                    System.Diagnostics.Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");

                    try
                    {
                        //authResult = await publicClientApp.AcquireTokenInteractive(scopes)
                        //    .WithPrompt(Prompt.SelectAccount).ExecuteAsync();
                        authResult = await publicClientApp.AcquireTokenInteractive(scopes)
                            .WithAccount(accounts.FirstOrDefault())
                            .WithPrompt(Prompt.SelectAccount)
                            .ExecuteAsync();
                    }
                    catch (MsalException msalex)
                    {
                        //ResultText.Text = $"Error Acquiring Token:{System.Environment.NewLine}{msalex}";
                    }
                }
                catch (Exception ex)
                {
                    //ResultText.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                    return Results;
                }


                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                //string base64PAT = Convert.ToBase64String(
                //        ASCIIEncoding.ASCII.GetBytes(
                //            string.Format("{0}:{1}", "", personalAccessToken)));

                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64PAT);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);


                // TODO(crhodes)
                // Update Uri  Use args for parameters.
                var requestUri = $"{args.Organization.VsSpsUri}/_apis/"
                    + $"tokens/pats"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    PatsRoot resultRoot = JsonConvert.DeserializeObject<PatsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Pat>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }

                return Results;
            }
        }
    }
}
