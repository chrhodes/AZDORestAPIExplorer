using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;

using AZDORestApiExplorer.Domain.Accounts.Events;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Net;

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
            Int64 startTicks = Log.DOMAIN("Enter(Account)", Common.LOG_CATEGORY);

            using (HttpClient client = new HttpClient())
            {
                Results.InitializeHttpClient(client, args.Organization.PAT);

                // TODO(crhodes)
                // Update Uri  
                // Use using accounts is documented but does not work
                //https://docs.microsoft.com/en-us/rest/api/azure/devops/account/accounts/list?view=azure-devops-rest-4.1#examples
                // Need to include ownerId or memberId

                var requestUri = $"https://app.vssps.visualstudio.com/_apis/"
                    + "accounts"
                    + "?api-version=6.1-preview.1";

                var exchange = Results.InitializeExchange(client, requestUri);

                using (HttpResponseMessage response = await client.GetAsync(requestUri))
                {
                    Results.RecordExchangeResponse(response, exchange);

                    response.EnsureSuccessStatusCode();

                    string outJson = await response.Content.ReadAsStringAsync();

                    AccountsRoot resultRoot = JsonConvert.DeserializeObject<AccountsRoot>(outJson);

                    Results.ResultItems = new ObservableCollection<Domain.Accounts.Account>(resultRoot.value);

                    IEnumerable<string> continuationHeaders = default;

                    bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                    Results.Count = Results.ResultItems.Count;
                }

                Log.DOMAIN("Exit(Account)", Common.LOG_CATEGORY, startTicks);

                return Results;
            }
        }
    }
}
