﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Core.Events.WorkItemTrackingProcess;
using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.WorkItemTrackingProcess;
using AZDORestApiExplorer.Presentation.ViewModels;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Services;
using VNC.HttpHelper;

namespace AZDORestApiExplorer.WorkItemTrackingProcess.Presentation.ViewModels
{
    public class FieldMainViewModel : GridViewModelBase, IFieldMainViewModel, IInstanceCountVM
    {

        #region Constructors, Initialization, and Load

        public FieldMainViewModel(
            IEventAggregator eventAggregator,
            IDialogService dialogService) : base(eventAggregator, dialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_CATEGORY);

            // TODO(crhodes)
            // Save constructor parameters here

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_CATEGORY, startTicks);
        }

        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_CATEGORY);

            InstanceCountVM++;


            EventAggregator.GetEvent<GetFieldsWITPEvent>().Subscribe(GetFields);

            this.Fields.PropertyChanged += PublishSelectionChanged;

            // TODO(crhodes)
            //

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        public RESTResult<Field> Fields { get; set; } = new RESTResult<Field>();


        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods

        private async void GetFields(GetFieldsWITPEventArgs args)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Helpers.InitializeHttpClient(args.Organization, client);

                    // TODO(crhodes)
                    // Update Uri  Use args for parameters.
                    var requestUri = $"{args.Organization.Uri}/_apis/work/processes/{args.Process.id}"
                        + $"/workItemTypes/{args.WorkItemType.referenceName}/fields?api-version=6.1-preview.1";

                    RequestResponseInfo exchange = InitializeExchange(client, requestUri);

                    using (HttpResponseMessage response = await client.GetAsync(requestUri))
                    {
                        RecordExchangeResponse(response, exchange);

                        response.EnsureSuccessStatusCode();

                        string outJson = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(outJson);

                        FieldsRoot resultRoot = JsonConvert.DeserializeObject<FieldsRoot>(outJson);

                        Fields.ResultItems = new ObservableCollection<Field>(resultRoot.value);

                        IEnumerable<string> continuationHeaders = default;

                        bool hasContinuationToken = response.Headers.TryGetValues("x-ms-continuationtoken", out continuationHeaders);

                        Fields.Count = Fields.ResultItems.Count;
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

            EventAggregator.GetEvent<SelectedFieldChangedEvent>().Publish(Fields.SelectedItem);

            Log.EVENT("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion

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
