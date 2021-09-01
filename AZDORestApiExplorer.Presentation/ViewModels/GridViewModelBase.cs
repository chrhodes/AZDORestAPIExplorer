using System;

using AZDORestApiExplorer.Core.Events;

using DevExpress.Xpf.Grid;

using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

using VNC;
using VNC.HttpHelper;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class GridViewModelBase : HTTPExchangeViewModelBase
    {
        #region Constructors, Initialization, and Load

        public GridViewModelBase(
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

            ExportGridCommand = new DelegateCommand<GridControl>(ExportGrid, ExportGridCanExecute);
            EventAggregator.GetEvent<SelectedCollectionChangedEvent>().Subscribe(CollectionChanged);

            Log.VIEWMODEL("Exit", Common.LOG_CATEGORY, startTicks);
        }

        #endregion Constructors, Initialization, and Load

        #region Fields and Properties

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

        private string _outputFileNameAndPath;
        public string OutputFileNameAndPath
        {
            get => _outputFileNameAndPath;
            set
            {
                if (_outputFileNameAndPath == value)
                    return;
                _outputFileNameAndPath = value;
                OnPropertyChanged();
            }
        }

        #endregion Fields and Properties

        #region Event Handlers

        public virtual void CollectionChanged(SelectedCollectionChangedEventArgs args)
        {
            OutputFileNameAndPath = $@"C:\temp\{args.Collection.Name}-TYPE";
        }

        #region ExportGrid Command

        public DelegateCommand<GridControl> ExportGridCommand { get; set; }

        public string ExportGridContent { get; set; } = "ExportGrid";
        public string ExportGridToolTip { get; set; } = "ExportGrid ToolTip";

        // Can get fancy and use Resources
        //public string ExportGridContent { get; set; } = "ViewName_ExportGridContent";
        //public string ExportGridToolTip { get; set; } = "ViewName_ExportGridContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_ExportGridContent">ExportGrid</system:String>
        //    <system:String x:Key="ViewName_ExportGridContentToolTip">ExportGrid ToolTip</system:String>

        public void ExportGrid(GridControl gridControl)
        {
            // TODO(crhodes)
            // Do something amazing.
            Message = "Cool, you called ExportGrid";

            var dialogParameters = new DialogParameters();
            dialogParameters.Add("message", $"Message)");
            dialogParameters.Add("title", "Exception");
            dialogParameters.Add("gridcontrol", gridControl);

            // TODO(crhodes)
            // Add some more context to name, e.g. Org, Team Project, ???

            dialogParameters.Add("outputfilenameandpath", OutputFileNameAndPath);

            DialogService.Show("ExportGridDialog", dialogParameters, r =>
            {
            });
        }

        public bool ExportGridCanExecute(GridControl gridControl)
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            return true;
        }

        #endregion ExportGrid Command

        #endregion Event Handlers

        #region Private Methods

        #endregion Private Methods

    }
}