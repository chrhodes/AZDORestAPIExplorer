using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

using AZDORestApiExplorer.Core.Events;
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

using static AZDORestApiExplorer.Domain.Git.PullRequestProperties;

namespace AZDORestApiExplorer.Git.Presentation.ViewModels
{
    public class PullRequestMainViewModel : GridViewModelBase, IInstanceCountVM
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
        public RESTResult<PullRequestCommitsRoot.Commit> ResultsCommits { get; set; } = new RESTResult<PullRequestCommitsRoot.Commit>();

        private PullRequestCommitsRoot.Commit _selectedCommit;

        public PullRequestCommitsRoot.Commit SelectedCommit
        {
            get => _selectedCommit;
            set
            {
                if (_selectedCommit == value)
                    return;

                // HACK(crhodes)
                // Until we figure out what to do with different class definitions of a (likely) common thing.
                // new up a Domain.Git commit and pass it along

                Domain.Git.Commit commit = new Domain.Git.Commit();

                commit.author = value.author;
                commit.comment = value.comment;
                commit.committer = value.committer;
                commit.commitId = value.commitId;
                commit.url = value.url;

                EventAggregator.GetEvent<SelectedCommitChangedEvent>().Publish(commit);
                //_selectedCommit = value;
                //OnPropertyChanged();
            }
        }

        public RESTResult<PullRequestIterations.Value> ResultsIterations { get; set; } = new RESTResult<PullRequestIterations.Value>();
        public RESTResult<PullRequest> ResultsLabels { get; set; } = new RESTResult<PullRequest>();
        public RESTResult<PullRequestProperties> ResultsProperties { get; set; } = new RESTResult<PullRequestProperties>();
        public RESTResult<ReviewersRoot.Value> ResultsReviewers { get; set; } = new RESTResult<ReviewersRoot.Value>();
        public RESTResult<PullRequest> ResultsStatuses { get; set; } = new RESTResult<PullRequest>();
        public RESTResult<PullRequest> ResultsThreads { get; set; } = new RESTResult<PullRequest>();
        public RESTResult<WorkItemsRoot.Value> ResultsWorkItems { get; set; } = new RESTResult<WorkItemsRoot.Value>();

        #endregion Fields and Properties

        #region Event Handlers

        #endregion Event Handlers

        #region Private Methods

        private async void GetPullRequests(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter(PullRequest)", Common.LOG_CATEGORY);

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

            Log.VIEWMODEL("Exit(PullRequest)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestAttachments(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter(PullRequesAttachments)", Common.LOG_CATEGORY);

            OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ResultsAttachments.InitializeHttpClient(client, args.Organization.PAT);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + $"git/repositories/{args.Repository.id}/pullrequests"
                        + $"/{args.PullRequest.pullRequestId}/attachments"
                        + "?api-version=6.1-preview.1";

                    var exchange = Results.InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        ResultsAttachments.RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        //ReviewersRoot resultRoot = JsonConvert.DeserializeObject<ReviewersRoot>(outJson);

                        //ResultsAttachments.ResultItems = new ObservableCollection<ReviewersRoot.Value>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        ResultsAttachments.Count = ResultsAttachments.ResultItems.Count;
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

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(ResultsAttachments.RequestResponseExchange);

            Log.VIEWMODEL("Exit(PullRequesAttachments)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestCommits(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter(PullRequestCommits)", Common.LOG_CATEGORY);

            OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ResultsCommits.InitializeHttpClient(client, args.Organization.PAT);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + $"git/repositories/{args.Repository.id}/pullrequests"
                        + $"/{args.PullRequest.pullRequestId}/commits"
                        + "?api-version=6.1-preview.1";

                    var exchange = Results.InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        ResultsCommits.RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        PullRequestCommitsRoot resultRoot = JsonConvert.DeserializeObject<PullRequestCommitsRoot>(outJson);

                        ResultsCommits.ResultItems = new ObservableCollection<PullRequestCommitsRoot.Commit>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        ResultsCommits.Count = ResultsCommits.ResultItems.Count;
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

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(ResultsCommits.RequestResponseExchange);

            Log.VIEWMODEL("Exit(PullRequestCommits)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestIterations(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter(PullRequestIterations)", Common.LOG_CATEGORY);

            OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ResultsIterations.InitializeHttpClient(client, args.Organization.PAT);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + $"git/repositories/{args.Repository.id}/pullrequests"
                        + $"/{args.PullRequest.pullRequestId}/iterations"
                        + "?api-version=6.1-preview.1";

                    var exchange = Results.InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        ResultsIterations.RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        PullRequestIterations resultRoot = JsonConvert.DeserializeObject<PullRequestIterations>(outJson);

                        ResultsIterations.ResultItems = new ObservableCollection<PullRequestIterations.Value>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        ResultsIterations.Count = ResultsIterations.ResultItems.Count;
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

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(ResultsIterations.RequestResponseExchange);

            Log.VIEWMODEL("Exit(PullRequestIterations)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestLabels(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter(PullRequestWorkLabels)", Common.LOG_CATEGORY);

            OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ResultsLabels.InitializeHttpClient(client, args.Organization.PAT);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + $"git/repositories/{args.Repository.id}/pullrequests"
                        + $"/{args.PullRequest.pullRequestId}/labels"
                        + "?api-version=6.1-preview.1";

                    var exchange = Results.InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        ResultsLabels.RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        //ReviewersRoot resultRoot = JsonConvert.DeserializeObject<ReviewersRoot>(outJson);

                        //ResultsLabels.ResultItems = new ObservableCollection<ReviewersRoot.Value>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        ResultsLabels.Count = ResultsLabels.ResultItems.Count;
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

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(ResultsLabels.RequestResponseExchange);

            Log.VIEWMODEL("Exit(PullRequestWorkLabels)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestProperties(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter(PullRequestProperties)", Common.LOG_CATEGORY);

            OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ResultsProperties.InitializeHttpClient(client, args.Organization.PAT);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + $"git/repositories/{args.Repository.id}/pullrequests"
                        + $"/{args.PullRequest.pullRequestId}/properties"
                        + "?api-version=6.1-preview.1";

                    var exchange = Results.InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        ResultsProperties.RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        //PullRequestProperties resultRoot = JsonConvert.DeserializeObject<PullRequestProperties>(outJson);

                        // NOTE(crhodes)
                        // There is only one item coming back.  Hack it into the collection.

                        //ResultsProperties.ResultItems = new ObservableCollection<PullRequestProperties.Value>(resultRoot.value);

                        PullRequestProperties result = JsonConvert.DeserializeObject<PullRequestProperties>(outJson);

                        ResultsProperties.ResultItems = new ObservableCollection<PullRequestProperties>();

                        ResultsProperties.ResultItems.Add(result);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        ResultsProperties.Count = ResultsProperties.ResultItems.Count;
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

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(ResultsProperties.RequestResponseExchange);

            Log.VIEWMODEL("Exit(PullRequestProperties)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestReviewers(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter(PullRequestReviewers)", Common.LOG_CATEGORY);

            OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ResultsReviewers.InitializeHttpClient(client, args.Organization.PAT);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + $"git/repositories/{args.Repository.id}/pullrequests"
                        + $"/{args.PullRequest.pullRequestId}/reviewers"
                        + "?api-version=6.1-preview.1";

                    var exchange = Results.InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        ResultsReviewers.RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        ReviewersRoot resultRoot = JsonConvert.DeserializeObject<ReviewersRoot>(outJson);

                        ResultsReviewers.ResultItems = new ObservableCollection<ReviewersRoot.Value>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        ResultsReviewers.Count = ResultsReviewers.ResultItems.Count;
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

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(ResultsReviewers.RequestResponseExchange);

            Log.VIEWMODEL("Exit(PullRequestReviewers)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestStatuses(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter(PullRequestStatuses)", Common.LOG_CATEGORY);

            OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ResultsStatuses.InitializeHttpClient(client, args.Organization.PAT);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + $"git/repositories/{args.Repository.id}/pullrequests"
                        + $"/{args.PullRequest.pullRequestId}/statuses"
                        + "?api-version=6.1-preview.1";

                    var exchange = Results.InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        ResultsStatuses.RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        //ReviewersRoot resultRoot = JsonConvert.DeserializeObject<ReviewersRoot>(outJson);

                        //ResultsStatuses.ResultItems = new ObservableCollection<ReviewersRoot.Value>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        ResultsStatuses.Count = ResultsStatuses.ResultItems.Count;
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

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(ResultsStatuses.RequestResponseExchange);

            Log.VIEWMODEL("Exit(PullRequestStatuses)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestThreads(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter(PullRequestThreads)", Common.LOG_CATEGORY);

            OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ResultsThreads.InitializeHttpClient(client, args.Organization.PAT);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + $"git/repositories/{args.Repository.id}/pullrequests"
                        + $"/{args.PullRequest.pullRequestId}/threads"
                        + "?api-version=6.1-preview.1";

                    var exchange = Results.InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        ResultsThreads.RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        //ReviewersRoot resultRoot = JsonConvert.DeserializeObject<ReviewersRoot>(outJson);

                        //ResultsThreads.ResultItems = new ObservableCollection<ReviewersRoot.Value>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        ResultsThreads.Count = ResultsThreads.ResultItems.Count;
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

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(ResultsThreads.RequestResponseExchange);

            Log.VIEWMODEL("Exit(PullRequestThreads)", Common.LOG_CATEGORY, startTicks);
        }

        private async void GetPullRequestWorkItems(GetPullRequestsEventArgs args)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter(PullRequestWorkItems)", Common.LOG_CATEGORY);

            OutputFileNameAndPath = $@"C:\temp\{args.Project.name}-{args.Repository.name}-PullRequests";

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    ResultsWorkItems.InitializeHttpClient(client, args.Organization.PAT);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis/"
                        + $"git/repositories/{args.Repository.id}/pullrequests"
                        + $"/{args.PullRequest.pullRequestId}/workitems"
                        + "?api-version=6.1-preview.1";

                    var exchange = Results.InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        ResultsWorkItems.RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        WorkItemsRoot resultRoot = JsonConvert.DeserializeObject<WorkItemsRoot>(outJson);

                        ResultsWorkItems.ResultItems = new ObservableCollection<WorkItemsRoot.Value>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        ResultsWorkItems.Count = ResultsWorkItems.ResultItems.Count;
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

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(ResultsWorkItems.RequestResponseExchange);

            Log.VIEWMODEL("Exit(PullRequestWorkItems)", Common.LOG_CATEGORY, startTicks);
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