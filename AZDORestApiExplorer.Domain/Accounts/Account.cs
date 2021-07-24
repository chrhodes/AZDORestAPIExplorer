using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Accounts.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC.Core.DomainServices;

namespace AZDORestApiExplorer.Domain.Accounts
{
    namespace Events
    {
        public class GetAccountsEvent : PubSubEvent<GetAccountsEventArgs> { }

        public class GetAccountsEventArgs
        {
            public Organization Organization;

            // public Process Process;

            // public WorkItemType WorkItemType;
        }

        public class SelectedAccountChangedEvent : PubSubEvent<Account> { }

    }

    public class AccountsRoot
    {
        public int count { get; set; }
        public Account[] value { get; set; }
    }

    public class Account
    {

        public RESTResult<Account> Results { get; set; } = new RESTResult<Account>();

        public async Task<RESTResult<Account>> GetList(GetAccountsEventArgs args)
        {
            using (HttpClient client = new HttpClient())
            {
                InitializeHttpClient(args.Organization, client);

                // TODO(crhodes)
                // Update Uri  
                // Use using accounts is documented but does not work
                //https://docs.microsoft.com/en-us/rest/api/azure/devops/account/accounts/list?view=azure-devops-rest-4.1#examples
                // Need to include ownerId or memberId

                var requestUri = $"https://app.vssps.visualstudio.com/_apis/"
                    + "accounts"
                    + "?api-version=6.1-preview.1";

                //RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    //RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    AccountsRoot resultRoot = JsonConvert.DeserializeObject<AccountsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Domain.Accounts.Account>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
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
