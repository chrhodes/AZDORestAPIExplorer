using System;
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
using VNC.Core.Events;
using VNC.Core.Mvvm;
using VNC.Core.Services;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class FoodDetailViewModel : DetailViewModelBase, IFoodDetailViewModel, IInstanceCountVM
    {
        #region Contructors, Initialization, and Load

        public FoodDetailViewModel(
                IFoodDataService FoodDataService,
                IEventAggregator eventAggregator,
                IMessageDialogService messageDialogService) : base(eventAggregator, messageDialogService)
        {
            Int64 startTicks = Log.CONSTRUCTOR("Enter", Common.LOG_APPNAME);

            InstanceCountVM++;

            _FoodDataService = FoodDataService;

            Title = "Foods";
            Foods = new ObservableCollection<FoodWrapper>();

            AddCommand = new DelegateCommand(AddExecute);
            RemoveCommand = new DelegateCommand(RemoveExecute, RemoveCanExecute);

            Log.CONSTRUCTOR("Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Enums

        #endregion

        #region Structures

        #endregion

        #region Fields and Properties

        private IFoodDataService _FoodDataService;

        public ICommand AddCommand { get; }

        public ICommand RemoveCommand { get; }

        public ObservableCollection<FoodWrapper> Foods { get; }

        private FoodWrapper _selectedFood;

        public FoodWrapper SelectedFood
        {
            get { return _selectedFood; }
            set
            {
                _selectedFood = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Event Handlers

        // private async void OpenDetailView(OpenDetailViewEventArgs args)
        // {
        // Int64 startTicks = Log.EVENT("(FoodDetailViewModel) Enter", Common.LOG_APPNAME);

        // await LoadAsync(args.Id);

        // Log.EVENT("(FoodDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        // }

        #endregion

        #region Public Methods

        public override async Task LoadAsync(int id)
        {
            Int64 startTicks = Log.VIEWMODEL("(FoodDetailViewModel) Enter Id:({id})", Common.LOG_APPNAME);

            Id = id;

            foreach (var wrapper in Foods)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }

            Foods.Clear();

            var items = await _FoodDataService.AllAsync();

            foreach (var model in items)
            {
                var wrapper = new FoodWrapper(model);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
                Foods.Add(wrapper);
            }

            Log.VIEWMODEL("(FoodDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        void Wrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _FoodDataService.HasChanges();
            }

            if (e.PropertyName == nameof(FoodWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Protected Methods

        protected override bool DeleteCanExecute()
        {
            // TODO(crhodes)
            // Why do we need this?
            return true;
        }

        protected override async void DeleteExecute()
        {
            Int64 startTicks = Log.VIEWMODEL($"(FoodDetailViewModel) Enter Id:({SelectedFood.Id})", Common.LOG_APPNAME);

            Log.VIEWMODEL("(FoodDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        protected override bool SaveCanExecute()
        {
            return HasChanges && Foods.All(p => !p.HasErrors);
        }

        protected override async void SaveExecute()
        {
            Int64 startTicks = Log.VIEWMODEL($"(FoodDetailViewModel) Enter Id:({SelectedFood.Id})", Common.LOG_APPNAME);

            try
            {
                await _FoodDataService.UpdateAsync();

                HasChanges = _FoodDataService.HasChanges();

                PublishAfterCollectionSavedEvent();
            }
            catch (Exception ex)
            {
                // while (ex.InnerException != null)
                // {
                // ex = ex.InnerException;
                // }

                MessageDialogService.ShowInfoDialog(
                    "Error while saving the Foods, " +
                    "the data will be reloaded.  Details: " + ex);
                await LoadAsync(Id);
            }

            Log.VIEWMODEL("(FoodDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        #endregion

        #region Private Methods

        void AddExecute()
        {
            Int64 startTicks = Log.VIEWMODEL("(FoodDetailViewModel) Enter", Common.LOG_APPNAME);

            var wrapper = new FoodWrapper(new Domain.Food());
            wrapper.PropertyChanged += Wrapper_PropertyChanged;

            _FoodDataService.Add(wrapper.Model);
            Foods.Add(wrapper);

            wrapper.Name = "";  // Trigger the validation

            Log.VIEWMODEL("(FoodDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        private async void RemoveExecute()
        {
            Int64 startTicks = Log.VIEWMODEL("(FoodDetailViewModel) Enter", Common.LOG_APPNAME);

            var isReferenced =
                await _FoodDataService.IsReferencedByCatAsync(SelectedFood.Id);

            if (isReferenced)
            {
                MessageDialogService.ShowInfoDialog(
                    $"The Cat ({SelectedFood.Name})" +
                    " can't be removed;  It is referenced by at least one Cat");
                return;
            }

            SelectedFood.PropertyChanged -= Wrapper_PropertyChanged;
            _FoodDataService.Remove(SelectedFood.Model);
            Foods.Remove(SelectedFood);
            SelectedFood = null;
            HasChanges = _FoodDataService.HasChanges();

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            Log.VIEWMODEL("(FoodDetailViewModel) Exit", Common.LOG_APPNAME, startTicks);
        }

        bool RemoveCanExecute()
        {
            return SelectedFood != null;
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
