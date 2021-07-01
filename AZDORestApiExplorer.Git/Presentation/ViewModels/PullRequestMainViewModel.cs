using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Core.Events.Git;
using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Git;
using AZDORestApiExplorer.Presentation.ViewModels;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.HttpHelper;

namespace AZDORestApiExplorer.Git.Presentation.ViewModels
{
    public class PullRequestMainViewModel : GridViewModelBase, IPullRequestMainViewModel
    {
        #region Constructors, Initialization, and Load

        public PullRequestMainViewModel(
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

            EventAggregator.GetEvent<GetPullRequestsEvent>().Subscribe(GetPullRequests);

            this.PullRequests.PropertyChanged += PublishSelectionChanged;

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion Constructors, Initialization, and Load

        #region Fields and Properties
        
        public RESTResult<PullRequest> PullRequests { get; set; } = new RESTResult<PullRequest>();

        #endregion Fields and Properties

        #region Event Handlers

        #endregion Event Handlers

        #region Private Methods

        private async void GetPullRequests(GetPullRequestsEventArgs args)
        {
            OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Helpers.InitializeHttpClient(args.Organization, client);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all"
                        + "&api-version=6.1-preview.1";

                    RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(outJson);

                        PullRequestsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestsRoot>(outJson);

                        PullRequests.ResultItems = new ObservableCollection<PullRequest>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        PullRequests.Count = PullRequests.ResultItems.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, Common.LOG_CATEGORY);

                var dialogParameters = new DialogParameters();
                dialogParameters.Add("message", $"Error ({ex})");
                dialogParameters.Add("title", "Exception");

                DialogService.Show("NotificationDialog", dialogParameters, r =>
                {
                });
            }

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(RequestResponseExchange);
        }

        private void PublishSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<SelectedPullRequestChangedEvent>().Publish(PullRequests.SelectedItem);

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion Private Methods
    }
}