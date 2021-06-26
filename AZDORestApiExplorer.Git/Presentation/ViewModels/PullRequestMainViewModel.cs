using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Windows.Input;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Core.Events.Git;
using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.Git;

using DevExpress.Xpf.Grid;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Services;
using VNC.HttpHelper;

namespace AZDORestApiExplorer.Git.Presentation.ViewModels
{
    public class PullRequestMainViewModel : HTTPExchangeBase, IPullRequestMainViewModel
    {

        #region Constructors, Initialization, and Load

        IDialogService _dialogService;

        public PullRequestMainViewModel(
            IDialogService dialogService,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            _dialogService = dialogService;

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<GetPullRequestsEvent>().Subscribe(GetPullRequests);

            this.PullRequests.PropertyChanged += PublishSelectionChanged;

            ShowCommand = new DelegateCommand<GridControl>(Show);
            ShowDialogCommand = new DelegateCommand<GridControl>(ShowDialog);

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<PullRequest> PullRequests { get; set; } = new RESTResult<PullRequest>();

        public ICommand ShowCommand { get; private set; }
        public ICommand ShowDialogCommand { get; private set; }

        private string _message = "Initial Message";

        public string Message
        {
            get => _message;
            set
            {
                if (_message == value)
                    return;
                _message = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private async void GetPullRequests(GetPullRequestsEventArgs args)
        {
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
                MessageDialogService.ShowInfoDialog($"Error ({ex})");
            }

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(RequestResponseExchange);
        }

        private void PublishSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_CATEGORY);

            EventAggregator.GetEvent<SelectedPullRequestChangedEvent>().Publish(PullRequests.SelectedItem);

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        private void Show(GridControl gridControl)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            var message = "Show";
            //using the dialog service as-is
            _dialogService.Show("NotificationDialog", new DialogParameters($"message={message}"), r =>
            {
                if (r.Result == ButtonResult.None)
                    Message = "Result is None";
                else if (r.Result == ButtonResult.OK)
                    Message = "Result is OK";
                else if (r.Result == ButtonResult.Cancel)
                    Message = "Result is Cancel";
                else
                    Message = "I Don't know what you did!?";
            });

            gridControl.View.ExportToXlsx(@"C:\temp\PullRequestExport.xlsx");

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void ShowDialog(GridControl gridControl)
        {
            Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_CATEGORY);

            var message = "ShowDialog";
            //using the dialog service as-is
            _dialogService.ShowDialog("NotificationDialog", new DialogParameters($"message={message}"), r =>
            {
                if (r.Result == ButtonResult.None)
                    Message = "Result is None";
                else if (r.Result == ButtonResult.OK)
                    Message = "Result is OK";
                else if (r.Result == ButtonResult.Cancel)
                    Message = "Result is Cancel";
                else
                    Message = "I Don't know what you did!?";
            });

            gridControl.View.ExportToXlsx(@"C:\temp\PullRequestExport.xlsx");

            Log.EVENT_HANDLER("Exit", Common.LOG_CATEGORY, startTicks);
        }

    }
}
