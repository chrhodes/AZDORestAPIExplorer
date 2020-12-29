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
    public class ProcessDetailViewModel : DetailViewModelBase, IProcessDetailViewModel, IInstanceCountVM
    {
        #region Contructors, Initialization, and Load

        public ProcessDetailViewModel(
                //IProcessDataService ProcessDataService,
                //ILookupDataService LookupDataService,
                IEventAggregator eventAggregator,
                IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            //InstanceCountVM++;

            //_ProcessDataService = ProcessDataService;
            //_LookupDataService = LookupDataService;

            //eventAggregator.GetEvent<AfterCollectionSavedEvent>()
            //    .Subscribe(AfterCollectionSaved);

            //AddPhoneNumberCommand = new DelegateCommand(
            //    OnAddPhoneNumberExecute);

            //RemovePhoneNumberCommand = new DelegateCommand(
            //    OnRemovePhoneNumberExecute, OnRemovePhoneNumberCanExecute);

            //s = new ObservableCollection<LookupItem>();
            //PhoneNumbers = new ObservableCollection<ProcessPhoneNumberWrapper>();

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Enums

        #endregion

        #region Structures

        #endregion

        #region Fields and Properties

        private IProcessDataService _ProcessDataService;
        //private ILookupDataService _LookupDataService;

        //public ICommand AddPhoneNumberCommand { get; }
        //public ICommand RemovePhoneNumberCommand { get; }

        //private ProcessPhoneNumberWrapper _selectedPhoneNumber;

        //public ObservableCollection<LookupItem> s { get; }
        //public ObservableCollection<ProcessPhoneNumberWrapper> PhoneNumbers { get; }


        private ProcessWrapper _Process;

        public ProcessWrapper Process
        {
            get { return _Process; }
            private set
            {
                _Process = value;
                OnPropertyChanged();
            }
        }

        //public ProcessPhoneNumberWrapper SelectedPhoneNumber
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
            Int64 startTicks = Log.EVENT_HANDLER("(ProcessDetailViewModel) Enter", Common.LOG_APPNAME);

            //if (args.ViewModelName == nameof(DetailViewModel))
            //{
            //    await LoadsLookupAsync();
            //}

            Log.EVENT_HANDLER("(ProcessDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Public Methods

        public override async Task LoadAsync(int id)
        {
            Int64 startTicks = Log.VIEWMODEL($"(ProcessDetailViewModel) Enter Id:({id})", Common.LOG_APPNAME);

            //var item = id > 0
            //    ? await _ProcessDataService.FindByIdAsync(id)
            //    : CreateNewProcess();

            //Id = item.Id;

            //InitializeProcess(item);

            //InitializeProcessPhoneNumbers(item.PhoneNumbers);

            //await LoadsLookupAsync();

            Log.VIEWMODEL("(ProcessDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
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
            //Int64 startTicks = Log.VIEWMODEL($"(ProcessDetailViewModel) Enter Id:({Process.Id})", Common.LOG_APPNAME);

            //var result = MessageDialogService.ShowOkCancelDialog(
            //    "Do you really want to delete the Process?", "Question");

            //if (result == MessageDialogResult.OK)
            //{
            //    _ProcessDataService.Remove(Process.Model);

            //    await _ProcessDataService.UpdateAsync();

            //    PublishAfterDetailDeletedEvent(Process.Id);
            //}

            //Log.VIEWMODEL("(ProcessDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        protected override bool OnSaveCanExecute()
        {
            // TODO(crhodes)
            // Check if Process is valid or has changes
            // This enables and disables the button

            var result = Process != null
                && !Process.HasErrors
                && HasChanges;

            return result;

            //return true;
        }

        protected override async void OnSaveExecute()
        {
            Int64 startTicks = Log.VIEWMODEL("(ProcessDetailViewModel) Enter Id:({Process.Id})", Common.LOG_APPNAME);

            await _ProcessDataService.UpdateAsync();

            //await SaveWithOptimisticConcurrencyAsync(_ProcessDataService.UpdateAsync,
            //  () =>
            //  {
            //      HasChanges = _ProcessDataService.HasChanges();
            //      Id = Process.Id;
            //      RaiseDetailSavedEvent(Process.Id, $"{Process.FieldString}");
            //  });

            //HasChanges = false;
            //Id = Process.Id;

            //PublishAfterDetailSavedEvent(Process.Id, Process.FieldString);

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        //private void OnAddPhoneNumberExecute()
        //{
        //    Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

        //    var newNumber = new ProcessPhoneNumberWrapper(new ProcessPhoneNumber());
        //    newNumber.PropertyChanged += ProcessPhoneNumberWrapper_PropertyChanged;
        //    PhoneNumbers.Add(newNumber);
        //    Process.Model.PhoneNumbers.Add(newNumber.Model);
        //    newNumber.Number = ""; // Trigger validation :-)

        //    Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private void OnRemovePhoneNumberExecute()
        //{
        //    Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

        //    SelectedPhoneNumber.PropertyChanged -= ProcessPhoneNumberWrapper_PropertyChanged;
        //    //_friendRepository.RemovePhoneNumber(SelectedPhoneNumber.Model);
        //    PhoneNumbers.Remove(SelectedPhoneNumber);
        //    SelectedPhoneNumber = null;
        //    HasChanges = _ProcessDataService.HasChanges();
        //    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

        //    Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private bool OnRemovePhoneNumberCanExecute()
        //{
        //    return SelectedPhoneNumber != null;
        //}

        #endregion

        #region Private Methods

        private Domain.Process CreateNewProcess()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            var item = new Domain.Process();
            //item.FieldDate = DateTime.Now;
            //item.FieldDate2 = DateTime.Now;
            //_ProcessDataService.Add(item);

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

        private void InitializeProcess(Process item)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            Process = new ProcessWrapper(item);

            Process.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _ProcessDataService.HasChanges();
                }

                if (e.PropertyName == nameof(Process.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

                if (e.PropertyName == nameof(Process.FieldString))
                {
                    SetTitle();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            // Little trick to trigger the validation when creating new entries
            //if (Process.Id == 0)
            //{
            //    Process.FieldString = "";
            //}

            SetTitle();

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        //private void InitializeProcessPhoneNumbers(ICollection<ProcessPhoneNumber> phoneNumbers)
        //{
        //    Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

        //    foreach (var wrapper in PhoneNumbers)
        //    {
        //        wrapper.PropertyChanged -= ProcessPhoneNumberWrapper_PropertyChanged;
        //    }

        //    PhoneNumbers.Clear();

        //    foreach (var phoneNumber in phoneNumbers)
        //    {
        //        var wrapper = new ProcessPhoneNumberWrapper(phoneNumber);
        //        PhoneNumbers.Add(wrapper);
        //        wrapper.PropertyChanged += ProcessPhoneNumberWrapper_PropertyChanged;
        //    }

        //    Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private void ProcessPhoneNumberWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

        //    if (!HasChanges)
        //    {
        //        HasChanges = _ProcessDataService.HasChanges();
        //    }
        //    if (e.PropertyName == nameof(ProcessPhoneNumberWrapper.HasErrors))
        //    {
        //        ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        //    }

        //    Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private async Task LoadsLookupAsync()
        //{
        //    Int64 startTicks = Log.VIEWMODEL("(ProcessDetailViewModel) Enter", Common.LOG_APPNAME);

        //    s.Clear();

        //    //ProgrammingLanguages.Add(new NullLookupItem());
        //    s.Add(new NullLookupItem { DisplayMember = " - " });

        //    var lookup = await _LookupDataService
        //                        .GetLookupAsync();

        //    foreach (var lookupItem in lookup)
        //    {
        //        s.Add(lookupItem);
        //    }

        //    Log.VIEWMODEL("(ProcessDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        //}

        private void SetTitle()
        {
            Title = $"{Process.FieldString}";
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
