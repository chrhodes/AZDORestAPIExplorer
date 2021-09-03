using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Git;
using AZDORestApiExplorer.Domain.Git.Events;
using AZDORestApiExplorer.Presentation.ViewModels;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Net;

namespace AZDORestApiExplorer.Git.Presentation.ViewModels
{
    public class PullRequestMainViewModel : GridViewModelBase, IPullRequestMainViewModel, IInstanceCountVM
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

            EventAggregator.GetEvent<GetPullRequestAttachmentsEvent>().Subscribe(GetPullRequestAttachments);
            EventAggregator.GetEvent<GetPullRequestCommitsEvent>().Subscribe(GetPullRequestCommits);
            EventAggregator.GetEvent<GetPullRequestIterationsEvent>().Subscribe(GetPullRequestIterations);
            EventAggregator.GetEvent<GetPullRequestLabelsEvent>().Subscribe(GetPullRequestLabels);
            EventAggregator.GetEvent<GetPullRequestPropertiesEvent>().Subscribe(GetPullRequestProperties);
            EventAggregator.GetEvent<GetPullRequestReviewersEvent>().Subscribe(GetPullRequestReviewers);
            EventAggregator.GetEvent<GetPullRequestStatusesEvent>().Subscribe(GetPullRequestStatuses);
            EventAggregator.GetEvent<GetPullRequestThreadsEvent>().Subscribe(GetPullRequestThreads);
            EventAggregator.GetEvent<GetPullRequestWorkItemsEvent>().Subscribe(GetPullRequestWorkItems);

            this.Results.PropertyChanged += PublishSelectionChanged;

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion Constructors, Initialization, and Load

        #region Fields and Properties
        
        public RESTResult<PullRequest> Results { get; set; } = new RESTResult<PullRequest>();

        public RESTResult<PullRequest> ResultsAttachments { get; set; } = new RESTResult<PullRequest>();
        public RESTResult<PullRequest> ResultsCommits { get; set; } = new RESTResult<PullRequest>();
        public RESTResult<PullRequest> ResultsIterations { get; set; } = new RESTResult<PullRequest>();
        public RESTResult<PullRequest> ResultsLabels { get; set; } = new RESTResult<PullRequest>();
        public RESTResult<PullRequest> ResultsProperties { get; set; } = new RESTResult<PullRequest>();
        public RESTResult<PullRequest> ResultsReviewers { get; set; } = new RESTResult<PullRequest>();
        public RESTResult<PullRequest> ResultsStatuses { get; set; } = new RESTResult<PullRequest>();
        public RESTResult<PullRequest> ResultsThreads { get; set; } = new RESTResult<PullRequest>();
        public RESTResult<PullRequest> ResultsWorkItems { get; set; } = new RESTResult<PullRequest>();

        #endregion Fields and Properties

        #region Event Handlers

        #endregion Event Handlers

        #region Private Methods

        private async void GetPullRequests(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequest)", Common.LOG_CATEGORY);

            OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Results.InitializeHttpClient(client, args.Organization.PAT);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all"
                        + "&api-version=6.1-preview.1";

                    var exchange = Results.InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        Results.RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(outJson);

                        PullRequestsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestsRoot>(outJson);

                        Results.ResultItems = new ObservableCollection<PullRequest>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        Results.Count = Results.ResultItems.Count;
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

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(Results.RequestResponseExchange);

            Log.DOMAIN("Exit(PullRequest)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestAttachments(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestAttachments)", Common.LOG_CATEGORY);

            //OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            //try
            //{
            //    using (HttpClient client = new HttpClient())
            //    {
            //        ResultsAttachments.InitializeHttpClient(client, args.Organization.PAT);

            //        // TODO(crhodes)
            //        // Update Uri  Use args for parameters.
            //        var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
            //            + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all"
            //            + "&api-version=6.1-preview.1";

            //        var exchange = Results.InitializeExchange(client, requestUri);

            //        using (HttpResponseMessage response = await client.GetAsync(requestUri))
            //        {
            //            ResultsAttachments.RecordExchangeResponse(response, exchange);

            //            response.EnsureSuccessStatusCode();

            //            string outJson = await response.Content.ReadAsStringAsync();

            //            JObject o = JObject.Parse(outJson);

            //            PullRequestsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestsRoot>(outJson);

            //            ResultsAttachments.ResultItems = new ObservableCollection<PullRequest>(resultRoot.value);

            //            IEnumerable<string> continuationHeaders = default;

            //            bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

            //            ResultsAttachments.Count = Results.ResultItems.Count;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, Common.LOG_CATEGORY);

            //    var dialogParameters = new DialogParameters();
            //    dialogParameters.Add("message", $"Error ({ex})");
            //    dialogParameters.Add("title", "Exception");

            //    DialogService.Show("NotificationDialog", dialogParameters, r =>
            //    {
            //    });
            //}

            //EventAggregator.GetEvent<HttpExchangeEvent>().Publish(Results.RequestResponseExchange);

            Log.DOMAIN("Exit(PullRequestAttachments)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestCommits(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestCommits)", Common.LOG_CATEGORY);

            //OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            //try
            //{
            //    using (HttpClient client = new HttpClient())
            //    {
            //        ResultsCommits.InitializeHttpClient(client, args.Organization.PAT);

            //        // TODO(crhodes)
            //        // Update Uri  Use args for parameters.
            //        var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
            //            + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all"
            //            + "&api-version=6.1-preview.1";

            //        var exchange = Results.InitializeExchange(client, requestUri);

            //        using (HttpResponseMessage response = await client.GetAsync(requestUri))
            //        {
            //            ResultsCommits.RecordExchangeResponse(response, exchange);

            //            response.EnsureSuccessStatusCode();

            //            string outJson = await response.Content.ReadAsStringAsync();

            //            JObject o = JObject.Parse(outJson);

            //            PullRequestsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestsRoot>(outJson);

            //            ResultsCommits.ResultItems = new ObservableCollection<PullRequest>(resultRoot.value);

            //            IEnumerable<string> continuationHeaders = default;

            //            bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

            //            ResultsCommits.Count = Results.ResultItems.Count;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, Common.LOG_CATEGORY);

            //    var dialogParameters = new DialogParameters();
            //    dialogParameters.Add("message", $"Error ({ex})");
            //    dialogParameters.Add("title", "Exception");

            //    DialogService.Show("NotificationDialog", dialogParameters, r =>
            //    {
            //    });
            //}

            //EventAggregator.GetEvent<HttpExchangeEvent>().Publish(Results.RequestResponseExchange);

            Log.DOMAIN("Exit(PullRequestCommits)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestIterations(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestIterations)", Common.LOG_CATEGORY);

            //OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            //try
            //{
            //    using (HttpClient client = new HttpClient())
            //    {
            //        ResultsIterations.InitializeHttpClient(client, args.Organization.PAT);

            //        // TODO(crhodes)
            //        // Update Uri  Use args for parameters.
            //        var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
            //            + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all"
            //            + "&api-version=6.1-preview.1";

            //        var exchange = Results.InitializeExchange(client, requestUri);

            //        using (HttpResponseMessage response = await client.GetAsync(requestUri))
            //        {
            //            ResultsIterations.RecordExchangeResponse(response, exchange);

            //            response.EnsureSuccessStatusCode();

            //            string outJson = await response.Content.ReadAsStringAsync();

            //            JObject o = JObject.Parse(outJson);

            //            PullRequestsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestsRoot>(outJson);

            //            ResultsIterations.ResultItems = new ObservableCollection<PullRequest>(resultRoot.value);

            //            IEnumerable<string> continuationHeaders = default;

            //            bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

            //            ResultsIterations.Count = Results.ResultItems.Count;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, Common.LOG_CATEGORY);

            //    var dialogParameters = new DialogParameters();
            //    dialogParameters.Add("message", $"Error ({ex})");
            //    dialogParameters.Add("title", "Exception");

            //    DialogService.Show("NotificationDialog", dialogParameters, r =>
            //    {
            //    });
            //}

            //EventAggregator.GetEvent<HttpExchangeEvent>().Publish(Results.RequestResponseExchange);

            Log.DOMAIN("Exit(PullRequestIterations)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestLabels(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestLabels)", Common.LOG_CATEGORY);

            //OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            //try
            //{
            //    using (HttpClient client = new HttpClient())
            //    {
            //        ResultsLabels.InitializeHttpClient(client, args.Organization.PAT);

            //        // TODO(crhodes)
            //        // Update Uri  Use args for parameters.
            //        var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
            //            + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all"
            //            + "&api-version=6.1-preview.1";

            //        var exchange = Results.InitializeExchange(client, requestUri);

            //        using (HttpResponseMessage response = await client.GetAsync(requestUri))
            //        {
            //            ResultsLabels.RecordExchangeResponse(response, exchange);

            //            response.EnsureSuccessStatusCode();

            //            string outJson = await response.Content.ReadAsStringAsync();

            //            JObject o = JObject.Parse(outJson);

            //            PullRequestsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestsRoot>(outJson);

            //            ResultsLabels.ResultItems = new ObservableCollection<PullRequest>(resultRoot.value);

            //            IEnumerable<string> continuationHeaders = default;

            //            bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

            //            ResultsLabels.Count = Results.ResultItems.Count;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, Common.LOG_CATEGORY);

            //    var dialogParameters = new DialogParameters();
            //    dialogParameters.Add("message", $"Error ({ex})");
            //    dialogParameters.Add("title", "Exception");

            //    DialogService.Show("NotificationDialog", dialogParameters, r =>
            //    {
            //    });
            //}

            //EventAggregator.GetEvent<HttpExchangeEvent>().Publish(Results.RequestResponseExchange);

            Log.DOMAIN("Exit(PullRequestLabels)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestProperties(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestProperties)", Common.LOG_CATEGORY);

            //OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            //try
            //{
            //    using (HttpClient client = new HttpClient())
            //    {
            //        ResultsProperties.InitializeHttpClient(client, args.Organization.PAT);

            //        // TODO(crhodes)
            //        // Update Uri  Use args for parameters.
            //        var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
            //            + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all"
            //            + "&api-version=6.1-preview.1";

            //        var exchange = Results.InitializeExchange(client, requestUri);

            //        using (HttpResponseMessage response = await client.GetAsync(requestUri))
            //        {
            //            ResultsProperties.RecordExchangeResponse(response, exchange);

            //            response.EnsureSuccessStatusCode();

            //            string outJson = await response.Content.ReadAsStringAsync();

            //            JObject o = JObject.Parse(outJson);

            //            PullRequestsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestsRoot>(outJson);

            //            ResultsProperties.ResultItems = new ObservableCollection<PullRequest>(resultRoot.value);

            //            IEnumerable<string> continuationHeaders = default;

            //            bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

            //            ResultsProperties.Count = Results.ResultItems.Count;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, Common.LOG_CATEGORY);

            //    var dialogParameters = new DialogParameters();
            //    dialogParameters.Add("message", $"Error ({ex})");
            //    dialogParameters.Add("title", "Exception");

            //    DialogService.Show("NotificationDialog", dialogParameters, r =>
            //    {
            //    });
            //}

            //EventAggregator.GetEvent<HttpExchangeEvent>().Publish(Results.RequestResponseExchange);

            Log.DOMAIN("Exit(PullRequestProperties)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestReviewers(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestReviewers)", Common.LOG_CATEGORY);

            //OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            //try
            //{
            //    using (HttpClient client = new HttpClient())
            //    {
            //        ResultsReviewers.InitializeHttpClient(client, args.Organization.PAT);

            //        // TODO(crhodes)
            //        // Update Uri  Use args for parameters.
            //        var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
            //            + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all"
            //            + "&api-version=6.1-preview.1";

            //        var exchange = Results.InitializeExchange(client, requestUri);

            //        using (HttpResponseMessage response = await client.GetAsync(requestUri))
            //        {
            //            Results.RecordExchangeResponse(response, exchange);

            //            response.EnsureSuccessStatusCode();

            //            string outJson = await response.Content.ReadAsStringAsync();

            //            JObject o = JObject.Parse(outJson);

            //            PullRequestsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestsRoot>(outJson);

            //            ResultsReviewers.ResultItems = new ObservableCollection<PullRequest>(resultRoot.value);

            //            IEnumerable<string> continuationHeaders = default;

            //            bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

            //            ResultsReviewers.Count = Results.ResultItems.Count;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, Common.LOG_CATEGORY);

            //    var dialogParameters = new DialogParameters();
            //    dialogParameters.Add("message", $"Error ({ex})");
            //    dialogParameters.Add("title", "Exception");

            //    DialogService.Show("NotificationDialog", dialogParameters, r =>
            //    {
            //    });
            //}

            //EventAggregator.GetEvent<HttpExchangeEvent>().Publish(Results.RequestResponseExchange);

            Log.DOMAIN("Exit(PullRequestReviewers)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestStatuses(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestStatuses)", Common.LOG_CATEGORY);

            //OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            //try
            //{
            //    using (HttpClient client = new HttpClient())
            //    {
            //        ResultsStatuses.InitializeHttpClient(client, args.Organization.PAT);

            //        // TODO(crhodes)
            //        // Update Uri  Use args for parameters.
            //        var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
            //            + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all"
            //            + "&api-version=6.1-preview.1";

            //        var exchange = Results.InitializeExchange(client, requestUri);

            //        using (HttpResponseMessage response = await client.GetAsync(requestUri))
            //        {
            //            ResultsStatuses.RecordExchangeResponse(response, exchange);

            //            response.EnsureSuccessStatusCode();

            //            string outJson = await response.Content.ReadAsStringAsync();

            //            JObject o = JObject.Parse(outJson);

            //            PullRequestsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestsRoot>(outJson);

            //            ResultsStatuses.ResultItems = new ObservableCollection<PullRequest>(resultRoot.value);

            //            IEnumerable<string> continuationHeaders = default;

            //            bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

            //            ResultsStatuses.Count = Results.ResultItems.Count;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, Common.LOG_CATEGORY);

            //    var dialogParameters = new DialogParameters();
            //    dialogParameters.Add("message", $"Error ({ex})");
            //    dialogParameters.Add("title", "Exception");

            //    DialogService.Show("NotificationDialog", dialogParameters, r =>
            //    {
            //    });
            //}

            //EventAggregator.GetEvent<HttpExchangeEvent>().Publish(Results.RequestResponseExchange);

            Log.DOMAIN("Exit(PullRequestStatuses)", Common.LOG_CATEGORY, startTicks);
        }
        
        private async void GetPullRequestThreads(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestThreads)", Common.LOG_CATEGORY);

            //OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            //try
            //{
            //    using (HttpClient client = new HttpClient())
            //    {
            //        ResultsThreads.InitializeHttpClient(client, args.Organization.PAT);

            //        // TODO(crhodes)
            //        // Update Uri  Use args for parameters.
            //        var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
            //            + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all"
            //            + "&api-version=6.1-preview.1";

            //        var exchange = Results.InitializeExchange(client, requestUri);

            //        using (HttpResponseMessage response = await client.GetAsync(requestUri))
            //        {
            //            ResultsThreads.RecordExchangeResponse(response, exchange);

            //            response.EnsureSuccessStatusCode();

            //            string outJson = await response.Content.ReadAsStringAsync();

            //            JObject o = JObject.Parse(outJson);

            //            PullRequestsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestsRoot>(outJson);

            //            ResultsThreads.ResultItems = new ObservableCollection<PullRequest>(resultRoot.value);

            //            IEnumerable<string> continuationHeaders = default;

            //            bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

            //            ResultsThreads.Count = Results.ResultItems.Count;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, Common.LOG_CATEGORY);

            //    var dialogParameters = new DialogParameters();
            //    dialogParameters.Add("message", $"Error ({ex})");
            //    dialogParameters.Add("title", "Exception");

            //    DialogService.Show("NotificationDialog", dialogParameters, r =>
            //    {
            //    });
            //}

            //EventAggregator.GetEvent<HttpExchangeEvent>().Publish(Results.RequestResponseExchange);

            Log.DOMAIN("Exit(PullRequestThreads)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestWorkItems(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.DOMAIN("Enter(PullRequestWorkItems)", Common.LOG_CATEGORY);

            OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            //try
            //{
            //    using (HttpClient client = new HttpClient())
            //    {
            //        ResultsWorkItems.InitializeHttpClient(client, args.Organization.PAT);

            //        // TODO(crhodes)
            //        // Update Uri  Use args for parameters.
            //        var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
            //            + $"git/repositories/{args.Repository.id}/pullrequests?searchCriteria.status=all"
            //            + "&api-version=6.1-preview.1";

            //        var exchange = Results.InitializeExchange(client, requestUri);

            //        using (HttpResponseMessage response = await client.GetAsync(requestUri))
            //        {
            //            ResultsWorkItems.RecordExchangeResponse(response, exchange);

            //            response.EnsureSuccessStatusCode();

            //            string outJson = await response.Content.ReadAsStringAsync();

            //            JObject o = JObject.Parse(outJson);

            //            PullRequestsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestsRoot>(outJson);

            //            ResultsWorkItems.ResultItems = new ObservableCollection<PullRequest>(resultRoot.value);

            //            IEnumerable<string> continuationHeaders = default;

            //            bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

            //            ResultsWorkItems.Count = Results.ResultItems.Count;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.Error(ex, Common.LOG_CATEGORY);

            //    var dialogParameters = new DialogParameters();
            //    dialogParameters.Add("message", $"Error ({ex})");
            //    dialogParameters.Add("title", "Exception");

            //    DialogService.Show("NotificationDialog", dialogParameters, r =>
            //    {
            //    });
            //}

            //EventAggregator.GetEvent<HttpExchangeEvent>().Publish(Results.RequestResponseExchange);

            Log.DOMAIN("Exit(PullRequestWorkItems)", Common.LOG_CATEGORY, startTicks);
        }

        private void PublishSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<SelectedPullRequestChangedEvent>().Publish(Results.SelectedItem);

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion Private Methods

        #region IInstanceCount

        private static int _instanceCountVM;

        public int InstanceCountVM
        {
            get => _instanceCountVM;
            set => _instanceCountVM = value;
        }

        #endregion
    }
}