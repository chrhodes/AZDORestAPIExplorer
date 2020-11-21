using Prism.Mvvm;

namespace AZDORestApiExplorer.Presentation.ViewModels
{
    public class NavigationItemViewModel : BindableBase
    {
        string _displayMember;

        public NavigationItemViewModel(int id, string displayMember)
        {
            Id = id;
            DisplayMember = displayMember;
        }

        public int Id { get; set; }

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                if (_displayMember == value)
                    return;
                _displayMember = value;
                RaisePropertyChanged();
            }
        }
    }
}

