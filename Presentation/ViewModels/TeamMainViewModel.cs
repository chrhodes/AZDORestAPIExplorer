using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain;

using Newtonsoft.Json;

using Prism.Events;

using VNC;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class TeamMainViewModel : HTTPExchangeBase, ITeamMainViewModel
    {
        #region Constructors, Initialization, and Load

        public TeamMainViewModel(
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<GetTeamsEvent>().Subscribe(GetTeams);

            this.Teams.PropertyChanged += PublishSelectedTeamChanged;

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void PublishSelectedTeamChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<SelectedTeamChangedEvent>().Publish(Teams.SelectedItem);

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<Domain.Team> Teams { get; set; } = new RESTResult<Domain.Team>();

        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods

        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private async void GetTeams(CollectionDetails collection)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Helpers.InitializeHttpClient(collection, client);

                    var requestUri = $"{collection.Uri}/_apis/teams?api-version=6.1-preview.3";

                    RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        TeamsRoot resultRoot = JsonConvert.DeserializeObject<TeamsRoot>(outJson);

                        Teams.ResultItems = new ObservableCollection<Domain.Team>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        while (hasContinuationToken)
                        {
                            RequestResponseInfo exchange2 = new RequestResponseInfo();

                            string continueToken = continuationHeaders.First();

                            string requestUri2 = $"{collection.Uri}/_apis/projects?api-version=6.1-preview.4&continuationToken={continueToken}";

                            exchange2.Uri = requestUri2;
                            exchange2.RequestHeadersX.AddRange(client.DefaultRequestHeaders);

                            using (HttpResponseMessage response2 = await client.GetAsync(requestUri2))
                            {
                                exchange2.Response = response2;
                                exchange2.ResponseHeadersX.AddRange(response2.Headers);

                                RequestResponseExchange.Add(exchange2);

                                response2.EnsureSuccessStatusCode();
                                string outJson2 = await response2.Content.ReadAsStringAsync();

                                TeamsRoot resultRootC = JsonConvert.DeserializeObject<TeamsRoot>(outJson2);
                                var resultArray2C = resultRootC.value;

                                Teams.ResultItems.AddRange(resultArray2C);

                                hasContinuationToken = response2.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);
                            }
                        }

                        Teams.Count = Teams.ResultItems.Count();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, Common.LOG_APPNAME);
                MessageDialogService.ShowInfoDialog($"Error ({ex})");
            }

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(RequestResponseExchange);
        }

        #endregion
    }
}
