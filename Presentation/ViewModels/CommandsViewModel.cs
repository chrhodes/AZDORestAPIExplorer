using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AZDORestApiExplorer.Core.Events;

using Prism.Commands;
using Prism.Events;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class CommandsViewModel : EventViewModelBase
    {

        #region Constructors, Initialization, and Load

        public CommandsViewModel(
            ICollectionMainViewModel collectionMainViewModel,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            _collectionMainViewModel = (CollectionMainViewModel)collectionMainViewModel;

            InitializeViewModel();

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }
        private void InitializeViewModel()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            //SelectedCollection = new CollectionDetails();
            ////var foo = AvailableCollections.First().Key;
            //CB1.SelectedItem = AvailableCollections.First();

            GetProcessesCommand = new DelegateCommand(OnGetProcessesExecute, OnGetProcessesCanExecute);
            GetProjectsCommand = new DelegateCommand(OnGetProjectsExecute, OnGetProjectsCanExecute);

            EventAggregator.GetEvent<SelectedCollectionChangedEvent>().Subscribe(RaiseCollectionChanged);

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void RaiseCollectionChanged()
        {
            GetProjectsCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        CollectionMainViewModel _collectionMainViewModel;

        #endregion

        #region Event Handlers


        #endregion

        #region Public Methods


        #endregion

        #region Protected Methods


        #endregion

        #region Private Methods


        #endregion

        #region GetProcesses Command

        public DelegateCommand GetProcessesCommand { get; set; }
        public string GetProcessesContent { get; set; } = "GetProcesses";
        public string GetProcessesToolTip { get; set; } = "GetProcesses ToolTip";

        // Can get fancy and use Resources
        //public string GetProcessesContent { get; set; } = "ViewName_GetProcessesContent";
        //public string GetProcessesToolTip { get; set; } = "ViewName_GetProcessesContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProcessesContent">GetProcesses</system:String>
        //    <system:String x:Key="ViewName_GetProcessesContentToolTip">GetProcesses ToolTip</system:String>  

        public void OnGetProcessesExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);
            // TODO(crhodes)
            // Do something amazing.
            //Message = "Cool, you called GetProcesses";
            EventAggregator.GetEvent<GetProcessesEvent>().Publish(_collectionMainViewModel.SelectedCollection.Details);

            // Start Cut Four

            // Put this in places that listen for event
            //Common.EventAggregator.GetEvent<GetProcessesEvent>().Subscribe(GetProcesses);

            // End Cut Four
            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetProcessesCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region GetProjects Command

        public DelegateCommand GetProjectsCommand { get; set; }
        public string GetProjectsContent { get; set; } = "GetProjects";
        public string GetProjectsToolTip { get; set; } = "GetProjects ToolTip";

        // Can get fancy and use Resources
        //public string GetProjectsContent { get; set; } = "ViewName_GetProjectsContent";
        //public string GetProjectsToolTip { get; set; } = "ViewName_GetProjectsContentToolTip";

        // Put these in Resource File
        //    <system:String x:Key="ViewName_GetProjectsContent">GetProjects</system:String>
        //    <system:String x:Key="ViewName_GetProjectsContentToolTip">GetProjects ToolTip</system:String>  

        public void OnGetProjectsExecute()
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);
            // TODO(crhodes)
            // Do something amazing.
            //Message = "Cool, you called GetProjects";
            EventAggregator.GetEvent<GetProjectsEvent>().Publish(_collectionMainViewModel.SelectedCollection.Details);

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        public bool OnGetProjectsCanExecute()
        {
            // TODO(crhodes)
            // Add any before button is enabled logic.
            if (_collectionMainViewModel.SelectedCollection is null)
            {
                return false;
            }

            return true;
        }

        #endregion

    }
}
