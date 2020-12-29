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
    public class CollectionDetailViewModel : DetailViewModelBase, ICollectionDetailViewModel, IInstanceCountVM
    {
        #region Contructors, Initialization, and Load

        public CollectionDetailViewModel(
                //ICollectionDataService CollectionDataService,
                //ILookupDataService LookupDataService,
                IEventAggregator eventAggregator,
                IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            //InstanceCountVM++;

            //_CollectionDataService = CollectionDataService;
            //_LookupDataService = LookupDataService;

            //eventAggregator.GetEvent<AfterCollectionSavedEvent>()
            //    .Subscribe(AfterCollectionSaved);

            //AddPhoneNumberCommand = new DelegateCommand(
            //    OnAddPhoneNumberExecute);

            //RemovePhoneNumberCommand = new DelegateCommand(
            //    OnRemovePhoneNumberExecute, OnRemovePhoneNumberCanExecute);

            //s = new ObservableCollection<LookupItem>();
            //PhoneNumbers = new ObservableCollection<CollectionPhoneNumberWrapper>();

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Enums

        #endregion

        #region Structures

        #endregion

        #region Fields and Properties

        private ICollectionDataService _CollectionDataService;
        //private ILookupDataService _LookupDataService;

        //public ICommand AddPhoneNumberCommand { get; }
        //public ICommand RemovePhoneNumberCommand { get; }

        //private CollectionPhoneNumberWrapper _selectedPhoneNumber;

        //public ObservableCollection<LookupItem> s { get; }
        //public ObservableCollection<CollectionPhoneNumberWrapper> PhoneNumbers { get; }


        private CollectionWrapper _Collection;

        public CollectionWrapper Collection
        {
            get { return _Collection; }
            private set
            {
                _Collection = value;
                OnPropertyChanged();
            }
        }

        //public CollectionPhoneNumberWrapper SelectedPhoneNumber
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
            Int64 startTicks = Log.EVENT_HANDLER("(CollectionDetailViewModel) Enter", Common.LOG_APPNAME);

            //if (args.ViewModelName == nameof(DetailViewModel))
            //{
            //    await LoadsLookupAsync();
            //}

            Log.EVENT_HANDLER("(CollectionDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Public Methods

        public override async Task LoadAsync(int id)
        {
            Int64 startTicks = Log.VIEWMODEL($"(CollectionDetailViewModel) Enter Id:({id})", Common.LOG_APPNAME);

            var item = id > 0
                ? await _CollectionDataService.FindByIdAsync(id)
                : CreateNewCollection();

            Id = item.Id;

            InitializeCollection(item);

            //InitializeCollectionPhoneNumbers(item.PhoneNumbers);

            await LoadsLookupAsync();

            Log.VIEWMODEL("(CollectionDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
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
            Int64 startTicks = Log.VIEWMODEL($"(CollectionDetailViewModel) Enter Id:({Collection.Id})", Common.LOG_APPNAME);

            var result = MessageDialogService.ShowOkCancelDialog(
                "Do you really want to delete the Collection?", "Question");

            if (result == MessageDialogResult.OK)
            {
                _CollectionDataService.Remove(Collection.Model);

                await _CollectionDataService.UpdateAsync();

                PublishAfterDetailDeletedEvent(Collection.Id);
            }

            Log.VIEWMODEL("(CollectionDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        protected override bool OnSaveCanExecute()
        {
            // TODO(crhodes)
            // Check if Collection is valid or has changes
            // This enables and disables the button

            var result = Collection != null
                && !Collection.HasErrors
                && HasChanges;

            return result;

            //return true;
        }

        protected override async void OnSaveExecute()
        {
            Int64 startTicks = Log.VIEWMODEL("(CollectionDetailViewModel) Enter Id:({Collection.Id})", Common.LOG_APPNAME);

            await _CollectionDataService.UpdateAsync();

            //await SaveWithOptimisticConcurrencyAsync(_CollectionDataService.UpdateAsync,
            //  () =>
            //  {
            //      HasChanges = _CollectionDataService.HasChanges();
            //      Id = Collection.Id;
            //      RaiseDetailSavedEvent(Collection.Id, $"{Collection.FieldString}");
            //  });

            HasChanges = false;
            Id = Collection.Id;

            PublishAfterDetailSavedEvent(Collection.Id, Collection.FieldString);

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        //private void OnAddPhoneNumberExecute()
        //{
        //    Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

        //    var newNumber = new CollectionPhoneNumberWrapper(new CollectionPhoneNumber());
        //    newNumber.PropertyChanged += CollectionPhoneNumberWrapper_PropertyChanged;
        //    PhoneNumbers.Add(newNumber);
        //    Collection.Model.PhoneNumbers.Add(newNumber.Model);
        //    newNumber.Number = ""; // Trigger validation :-)

        //    Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private void OnRemovePhoneNumberExecute()
        //{
        //    Int64 startTicks = Log.EVENT_HANDLER("Enter", Common.LOG_APPNAME);

        //    SelectedPhoneNumber.PropertyChanged -= CollectionPhoneNumberWrapper_PropertyChanged;
        //    //_friendRepository.RemovePhoneNumber(SelectedPhoneNumber.Model);
        //    PhoneNumbers.Remove(SelectedPhoneNumber);
        //    SelectedPhoneNumber = null;
        //    HasChanges = _CollectionDataService.HasChanges();
        //    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

        //    Log.EVENT_HANDLER("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private bool OnRemovePhoneNumberCanExecute()
        //{
        //    return SelectedPhoneNumber != null;
        //}

        #endregion

        #region Private Methods

        private Domain.Collection CreateNewCollection()
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            var item = new Domain.Collection();
            item.FieldDate = DateTime.Now;
            item.FieldDate2 = DateTime.Now;
            _CollectionDataService.Add(item);

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

        private void InitializeCollection(Collection item)
        {
            Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

            Collection = new CollectionWrapper(item);

            Collection.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _CollectionDataService.HasChanges();
                }

                if (e.PropertyName == nameof(Collection.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

                if (e.PropertyName == nameof(Collection.FieldString))
                {
                    SetTitle();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            // Little trick to trigger the validation when creating new entries
            if (Collection.Id == 0)
            {
                Collection.FieldString = "";
            }

            SetTitle();

            Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        }

        //private void InitializeCollectionPhoneNumbers(ICollection<CollectionPhoneNumber> phoneNumbers)
        //{
        //    Int64 startTicks = Log.VIEWMODEL("Enter", Common.LOG_APPNAME);

        //    foreach (var wrapper in PhoneNumbers)
        //    {
        //        wrapper.PropertyChanged -= CollectionPhoneNumberWrapper_PropertyChanged;
        //    }

        //    PhoneNumbers.Clear();

        //    foreach (var phoneNumber in phoneNumbers)
        //    {
        //        var wrapper = new CollectionPhoneNumberWrapper(phoneNumber);
        //        PhoneNumbers.Add(wrapper);
        //        wrapper.PropertyChanged += CollectionPhoneNumberWrapper_PropertyChanged;
        //    }

        //    Log.VIEWMODEL("Exit", Common.LOG_APPNAME, startTicks);
        //}

        //private void CollectionPhoneNumberWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    Int64 startTicks = Log.EVENT("Enter", Common.LOG_APPNAME);

        //    if (!HasChanges)
        //    {
        //        HasChanges = _CollectionDataService.HasChanges();
        //    }
        //    if (e.PropertyName == nameof(CollectionPhoneNumberWrapper.HasErrors))
        //    {
        //        ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        //    }

        //    Log.EVENT("Exit", Common.LOG_APPNAME, startTicks);
        //}

        private async Task LoadsLookupAsync()
        {
            Int64 startTicks = Log.VIEWMODEL("(CollectionDetailViewModel) Enter", Common.LOG_APPNAME);

            //s.Clear();

            ////ProgrammingLanguages.Add(new NullLookupItem());
            //s.Add(new NullLookupItem { DisplayMember = " - " });

            //var lookup = await _LookupDataService
            //                    .GetLookupAsync();

            //foreach (var lookupItem in lookup)
            //{
            //    s.Add(lookupItem);
            //}

            Log.VIEWMODEL("(CollectionDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        private void SetTitle()
        {
            Title = $"{Collection.FieldString}";
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
