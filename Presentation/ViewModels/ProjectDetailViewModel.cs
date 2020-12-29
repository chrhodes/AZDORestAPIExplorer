using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using AZDORestApiExplorer.Domain;
using AZDORestApiExplorer.DomainServices;
using AZDORestApiExplorer.Presentation.ModelWrappers;

using Prism.Commands;
using Prism.Events;

using VNC;
using VNC.Core.DomainServices;
using VNC.Core.Events;
using VNC.Core.Mvvm;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class ProjectDetailViewModel : DetailViewModelBase, IProjectDetailViewModel, IInstanceCountVM
    {
        #region Contructors, Initialization, and Load

        public ProjectDetailViewModel(
                //IProjectDataService ProjectDataService,
                //ILookupDataService LookupDataService,
                IEventAggregator eventAggregator,
                IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            //InstanceCountVM++;

            //_ProjectDataService = ProjectDataService;
            //_LookupDataService = LookupDataService;

            //eventAggregator.GetEvent<AfterCollectionSavedEvent>()
            //    .Subscribe(AfterCollectionSaved);

            //AddPhoneNumberCommand = new DelegateCommand(
            //    OnAddPhoneNumberExecute);

            //RemovePhoneNumberCommand = new DelegateCommand(
            //    OnRemovePhoneNumberExecute, OnRemovePhoneNumberCanExecute);

            //s = new ObservableCollection<LookupItem>();
            //PhoneNumbers = new ObservableCollection<ProjectPhoneNumberWrapper>();

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Enums

        #endregion

        #region Structures

        #endregion

        #region Fields and Properties

        private IProjectDataService _ProjectDataService;
        //private ILookupDataService _LookupDataService;

        //public ICommand AddPhoneNumberCommand { get; }
        //public ICommand RemovePhoneNumberCommand { get; }

        //private ProjectPhoneNumberWrapper _selectedPhoneNumber;

        //public ObservableCollection<LookupItem> s { get; }
        //public ObservableCollection<ProjectPhoneNumberWrapper> PhoneNumbers { get; }


        private ProjectWrapper _Project;

        public ProjectWrapper Project
        {
            get { return _Project; }
            private set
            {
                _Project = value;
                OnPropertyChanged();
            }
        }

        //public ProjectPhoneNumberWrapper SelectedPhoneNumber
        //{
        //    get { return _selectedPhoneNumber; }
        //    set
        //    {
        //        _selectedPhoneNumber = value;
        //        OnPropertyChanged();
        //        ((DelegateCommand)RemovePhoneNumberCommand).RaiseCanExecuteChanged();
        //    }
        //}

        #endregion

        #region Event Handlers

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

            await LoadAsync(args.Id);

            Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        }

        private async void AfterCollectionSaved(AfterCollectionSavedEventArgs args)
        {
            Int64 startTicks = Log.EVENT_HANDLER("(ProjectDetailViewModel) Enter", Common.LOG_APPNAME);

            //if (args.ViewModelName == nameof(DetailViewModel))
            //{
            //    await LoadsLookupAsync();
            //}

            Log.EVENT_HANDLER("(ProjectDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Public Methods

        public override async Task LoadAsync(int id)
        {
            Int64 startTicks = Log.VIEWMODEL($"(ProjectDetailViewModel) Enter Id:({id})", Common.LOG_APPNAME);

            var item = id > 0
                ? await _ProjectDataService.FindByIdAsync(id)
                : CreateNewProject();

            //Id = item.Id;

            //InitializeProject(item);

            //InitializeProjectPhoneNumbers(item.PhoneNumbers);

            //await LoadsLookupAsync();

            Log.VIEWMODEL("(ProjectDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Protected Methods

        protected override bool OnDeleteCanExecute()
        {
            // TODO(crhodes)
            // Why do we need this?
            return true;
        }

        protected override async void OnDeleteExecute()
        {
            //Int64 startTicks = Log.VIEWMODEL($"(ProjectDetailViewModel) Enter Id:({Project.Id})", Common.LOG_APPNAME);

            //var result = MessageDialogService.ShowOkCancelDialog(
            //    "Do you really want to delete the Project?", "Question");

            //if (result == MessageDialogResult.OK)
            //{
            //    _ProjectDataService.Remove(Project.Model);

            //    await _ProjectDataService.UpdateAsync();

            //    PublishAfterDetailDeletedEvent(Project.Id);
            //}

            //Log.VIEWMODEL("(ProjectDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        protected override bool OnSaveCanExecute()
        {
            // TODO(crhodes)
            // Check if Project is valid or has changes
            // This enables and disables the button

            var result = Project != null
                && !Project.HasErrors
                && HasChanges;

            return result;

            //return true;
        }

        protected override async void OnSaveExecute()
        {
            Int64 startTicks = Log.VIEWMODEL("(ProjectDetailViewModel) Enter Id:({Project.Id})", Common.LOG_APPNAME);

            await _ProjectDataService.UpdateAsync();

            //await SaveWithOptimisticConcurrencyAsync(_ProjectDataService.UpdateAsync,
            //  () =>
            //  {
            //      HasChanges = _ProjectDataService.HasChanges();
            //      Id = Project.Id;
            //      RaiseDetailSavedEvent(Project.Id, $"{Project.FieldString}");
            //  });

            //HasChanges = false;
            //Id = Project.Id;

            //PublishAfterDetailSavedEvent(Project.Id, Project.FieldString);

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        //private void OnAddPhoneNumberExecute()
        //{
        //    Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

        //    var newNumber = new ProjectPhoneNumberWrapper(new ProjectPhoneNumber());
        //    newNumber.PropertyChanged += ProjectPhoneNumberWrapper_PropertyChanged;
        //    PhoneNumbers.Add(newNumber);
        //    Project.Model.PhoneNumbers.Add(newNumber.Model);
        //    newNumber.Number = ""; // Trigger validation :-)

        //    Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private void OnRemovePhoneNumberExecute()
        //{
        //    Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

        //    SelectedPhoneNumber.PropertyChanged -= ProjectPhoneNumberWrapper_PropertyChanged;
        //    //_friendRepository.RemovePhoneNumber(SelectedPhoneNumber.Model);
        //    PhoneNumbers.Remove(SelectedPhoneNumber);
        //    SelectedPhoneNumber = null;
        //    HasChanges = _ProjectDataService.HasChanges();
        //    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

        //    Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private bool OnRemovePhoneNumberCanExecute()
        //{
        //    return SelectedPhoneNumber != null;
        //}

        #endregion

        #region Private Methods

        private Domain.Project CreateNewProject()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            var item = new Domain.Project();
            //item.FieldDate = DateTime.Now;
            //item.FieldDate2 = DateTime.Now;
            _ProjectDataService.Add(item);

            // TODO(crhodes)
            // Need to figure out how to handle creating new.
            // This tries to push all the way to the database which complains because
            // Haven't set Required Fields (e.g. FieldString)

            // This was our attempt to use our DataService later - but that creates a context and tries to add item which
            // throws exception

            //_dataService.Insert(item);

            // This is what was in Claudius Code (NB>  Add does not call Save Changes in his code
            //_friendRepository.Add(friend);

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);

            return item;
        }

        private void InitializeProject(Project item)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            Project = new ProjectWrapper(item);

            Project.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _ProjectDataService.HasChanges();
                }

                if (e.PropertyName == nameof(Project.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

                if (e.PropertyName == nameof(Project.FieldString))
                {
                    SetTitle();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            // Little trick to trigger the validation when creating new entries
            //if (Project.Id == 0)
            //{
            //    Project.FieldString = "";
            //}

            SetTitle();

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        //private void InitializeProjectPhoneNumbers(ICollection<ProjectPhoneNumber> phoneNumbers)
        //{
        //    Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

        //    foreach (var wrapper in PhoneNumbers)
        //    {
        //        wrapper.PropertyChanged -= ProjectPhoneNumberWrapper_PropertyChanged;
        //    }

        //    PhoneNumbers.Clear();

        //    foreach (var phoneNumber in phoneNumbers)
        //    {
        //        var wrapper = new ProjectPhoneNumberWrapper(phoneNumber);
        //        PhoneNumbers.Add(wrapper);
        //        wrapper.PropertyChanged += ProjectPhoneNumberWrapper_PropertyChanged;
        //    }

        //    Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private void ProjectPhoneNumberWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

        //    if (!HasChanges)
        //    {
        //        HasChanges = _ProjectDataService.HasChanges();
        //    }
        //    if (e.PropertyName == nameof(ProjectPhoneNumberWrapper.HasErrors))
        //    {
        //        ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        //    }

        //    Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private async Task LoadsLookupAsync()
        //{
        //    Int64 startTicks = Log.VIEWMODEL("(ProjectDetailViewModel) Enter", Common.LOG_APPNAME);

        //    s.Clear();

        //    //ProgrammingLanguages.Add(new NullLookupItem());
        //    s.Add(new NullLookupItem { DisplayMember = " - " });

        //    var lookup = await _LookupDataService
        //                        .GetLookupAsync();

        //    foreach (var lookupItem in lookup)
        //    {
        //        s.Add(lookupItem);
        //    }

        //    Log.VIEWMODEL("(ProjectDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        //}

        private void SetTitle()
        {
            Title = $"{Project.FieldString}";
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
