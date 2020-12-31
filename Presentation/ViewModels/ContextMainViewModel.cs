using System;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.Presentation.ModelWrappers;

using Prism.Events;

using VNC;
using VNC.Core.Mvvm;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class ContextMainViewModel : EventViewModelBase, IContextMainViewModel, IInstanceCountVM
    {

        #region Constructors, Initialization, and Load

        public ContextMainViewModel(
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

            InstanceCountVM++;

            EventAggregator.GetEvent<SelectedProjectChangedEvent>().Subscribe(ProjectChanged);
            EventAggregator.GetEvent<SelectedTeamChangedEvent>().Subscribe(TeamChanged);
            EventAggregator.GetEvent<SelectedProcessChangedEvent>().Subscribe(ProcessChanged);

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        private void ProjectChanged(Project project)
        {
            Context.SelectedProject = project;
        }

        private void TeamChanged(Team team)
        {
            Context.SelectedTeam = team;
        }

        private void ProcessChanged(Process process)
        {
            Context.SelectedProcess = process;
        }

        #endregion

        #region Enums


        #endregion

        #region Structures


        #endregion

        #region Fields and Properties

        private ContextWrapper _context = new ContextWrapper(new Context());
        
        public ContextWrapper Context
        {
            get => _context;
            set
            {
                if (_context == value)
                    return;
                _context = value;
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
