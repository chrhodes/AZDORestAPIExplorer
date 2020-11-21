using System;
using System.Threading.Tasks;
using System.Windows.Input;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.DomainServices;

using Prism.Commands;
using Prism.Events;

using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    class CatDetailViewModel : ViewModelBase, ITYPEDetailViewModel
    {
        private ICatDataService _dataService;
        private IEventAggregator _eventAggregator;

        public CatDetailViewModel(
                ICatDataService dataService,
                IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<OpenCatDetailViewEvent>()
                .Subscribe(OnOpenTYPEDetailView);

            SaveCommand = new DelegateCommand(
                OnSaveExecute, OnSaveCanExecute);
        }

        private void OnOpenTYPEDetailView(AfterCatSavedEventArgs obj)
        {
            throw new NotImplementedException();
        }

        private async void OnOpenTYPEDetailView(int typeId)
        {
            await LoadAsync(typeId);
        }

        public async Task LoadAsync(int id)
        {
            Type = await _dataService.FindByIdAsync(id);
        }

        private AZDORestApiExplorer.Domain.Cat _type;

        public AZDORestApiExplorer.Domain.Cat Type
        {
            get { return _type; }
            private set
            {
                _type = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        async void OnSaveExecute()
        {
            await _dataService.UpdateAsync(Type);

            // Tell the Customer that we have updated something
            _eventAggregator.GetEvent<AfterCatSavedEvent>()
                .Publish(new AfterCatSavedEventArgs
                {
                    Id = Type.Id,
                    DisplayMember = Type.FieldString
                    //DisplayMember = $"{Type.FieldString} {Customer.LastName}"
                });

        }

        bool OnSaveCanExecute()
        {
            // TODO(crhodes)
            // Check if Customer is valid
            return true;
        }
    }
}
