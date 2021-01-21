using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;

using AZDORestApiExplorer.Core;
using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Core.Events.WorkItemTracking;
using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Domain.WorkItemTracking;
using AZDORestApiExplorer.WorkItemTracking.Presentation.Views;

using DevExpress.XtraPrinting;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Prism.Commands;
using Prism.Events;

using VNC;
using VNC.Core.Services;
using VNC.HttpHelper;

namespace AZDORestApiExplorer.WorkItemTracking.Presentation.ViewModels
{
    public class FieldMainViewModel : HTTPExchangeBase, IFieldMainViewModel
    {

        #region Constructors, Initialization, and Load

        public FieldMainViewModel(
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

            EventAggregator.GetEvent<GetFieldsWITEvent>().Subscribe(GetFields);

            this.Fields.PropertyChanged += PublishSelectionChanged;

            ExportToExcelCommand = new DelegateCommand(ExportToExcelExecute, ExportToExcelCanExecute);

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
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

        #region Commands


        #region GetFields Command

        public DelegateCommand ExportToExcelCommand { get; set; }
        public string ExportToExcelTContent { get; set; } = "Export to Excel";
        public string ExportToExcelToolTip { get; set; } = "Export to Excel ToolTip";

        public void ExportToExcelExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            XlsxExportOptions options = new XlsxExportOptions();
            options.SheetName = "WITFields";
            ((FieldMain)View).gcMainTable.View.ExportToXlsx(@"C:\temp\FieldData.xlsx",options);

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool ExportToExcelCanExecute()
        {
            //if (_collectionMainViewModel.SelectedCollection is null)
            //{
            //    return false;
            //}

            return true;
        }

        #endregion

        #endregion

        private async void GetFields(GetFieldsWITEventArgs args)
        {
            try
            {
                string requestUri;

                using (HttpClient client = new HttpClient())
                {
                    Helpers.InitializeHttpClient(args.Organization, client);

                    if (args.Project is null)
                    {
                        requestUri = $"{args.Organization.Uri}/_apis"
                            + "/wit/fields"
                            + "?api-version=4.1";
                    }
                    else
                    {
                        requestUri = $"{args.Organization.Uri}/{args.Project.id}/_apis"
                            + "/wit/fields"
                            + "?api-version=4.1";
                    }

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
                Log.Error(ex, Common.LOG_APPNAME);
                MessageDialogService.ShowInfoDialog($"Error ({ex})");
            }

            EventAggregator.GetEvent<HttpExchangeEvent>().Publish(RequestResponseExchange);
        }

        private void PublishSelectionChanged(object sender, PropertyChangedEventArgs e)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            EventAggregator.GetEvent<SelectedFieldWITChangedEvent>().Publish(Fields.SelectedItem);

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

    }
}
