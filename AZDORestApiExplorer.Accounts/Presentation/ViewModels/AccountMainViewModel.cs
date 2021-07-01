using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Core.Events.Accounts;
using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Accounts;
using AZDORestApiExplorer.Presentation.ViewModels;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Services;
using VNC.HttpHelper;

namespace AZDORestApiExplorer.Accounts.Presentation.ViewModels
{
    public class AccountMainViewModel : GridViewModelBase, IAccountMainViewModel
    {

        #region Constructors, Initialization, and Load

        public AccountMainViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService) : base(eventAggregator, dialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetAccountsEvent>().Subscribe(GetAccounts);

            this.Results.PropertyChanged += PublishSelectionChanged;

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<Account> Results { get; set; } = new RESTResult<Account>();

        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private async void GetAccounts(GetAccountsEventArgs args)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Core.Helpers.InitializeHttpClient(args.Organization, client);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"https://app.vssps.visualstudio.com/_apis/"
                        + "accounts"
                        + "?api-version=6.1-preview.1";

                    RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(outJson);

                        AccountsRoot resultRoot = JsonConvert.DeserializeObject<AccountsRoot>(outJson);

                        //Accounts.ResultItems = new ObservableCollection<Domain.Accounts.Account>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        Results.Count = Results.ResultItems.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, Common.LOG_CATEGORY);

                ExceptionDialogService.DisplayExceptionDialog(DialogService, ex);
            }

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(RequestResponseExchange);
        }

        private void PublishSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<SelectedAccountChangedEvent>().Publish(Results.SelectedItem);

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

    }
}
