using System.Collections.ObjectModel;
using System.Threading.Tasks;

using AZDORestApiExplorer.Core.Events;
using AZDORestApiExplorer.DomainServices;
using AZDORestApiExplorer.Presentation.ViewModels;

using Prism.Events;

using VNC.Core.DomainServices;
using VNC.Core.Mvvm;

namespace AZDORestApiExplorer.Presentation.TYPE.ViewModels
{
    public class TYPEViewModel : ViewModelBase, ITYPEViewModel
    {
        private ICatLookupDataService _dataService;
        private IEventAggregator _eventAggregator;

        public TYPEViewModel(
                ICatLookupDataService TYPELookupDataService,
                IEventAggregator eventAggregator)
        {
            _dataService = TYPELookupDataService;
            _eventAggregator = eventAggregator;
            TYPEs = new ObservableCollection<NavigationItemViewModel>();
        }

        public async Task LoadAsync()
        {
            var lookup = await _dataService.GetTYPELookupAsync();
            TYPEs.Clear();

            foreach (var item in lookup)
            {
                TYPEs.Add(new NavigationItemViewModel(item.Id, item.DisplayMember));
            }
        }

        public ObservableCollection<NavigationItemViewModel> TYPEs { get; }

        public IView View
        {
            get;
            set;
        }

        NavigationItemViewModel _selectedTYPE;

        public NavigationItemViewModel SelectedTYPE
        {
            get { return _selectedTYPE; }
            set
            {
                _selectedTYPE = value;
                OnPropertyChanged();

                if (_selectedTYPE != null)
                {
                    _eventAggregator.GetEvent<OpenCatDetailViewEvent>()
                        .Publish(_selectedTYPE.Id);
                }
            }
        }
    }
}
