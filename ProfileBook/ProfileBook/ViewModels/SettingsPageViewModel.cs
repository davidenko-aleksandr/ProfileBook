using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Services.EnumServices;
using ProfileBook.Themes;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsPageViewModel : BindableBase, IConfirmNavigation
    {
        private readonly IProfileSort _profileSort;
        private readonly INavigationService _navigationService;

        private bool _isdate;
        private bool _inickName;
        private bool _isname;

        private ICommand _comaBackCommand;
        private ICommand _bydateCommand;
        private ICommand _byNickNameCommand;
        private ICommand _bynameCommand;
        private ICommand _newIsCheckCommand;

        public bool IsDate
        {
            get { return _isdate; }
            set { SetProperty(ref _isdate, value); }
        }
        public bool IsNickName
        {
            get { return _inickName; }
            set { SetProperty(ref _inickName, value); }
        }
        public bool IsName
        {
            get { return _isname; }
            set { SetProperty(ref _isname, value); }
        }

        public SettingsPageViewModel(IProfileSort profileSort, INavigationService navigationService)
        {
            _profileSort = profileSort;
            if (_profileSort.SaveSelectSort == "By nick name") IsNickName = true;
            if (_profileSort.SaveSelectSort == "By name") IsName = true;
            if (_profileSort.SaveSelectSort == "By date") IsDate = true;
            _navigationService = navigationService;
        }

        public ICommand ComaBackCommand => _comaBackCommand ?? (_comaBackCommand = new Command(
                                            async () => await ComeBack()));

        public ICommand ByDateCommand => _bydateCommand ?? (_bydateCommand = new Command(DateSort));

        public ICommand ByNickNameCommand => _byNickNameCommand ?? (_byNickNameCommand = new Command(NickNameSort));

        public ICommand ByNameCommand => _bynameCommand ?? (_bynameCommand = new Command(NameSort));

        public ICommand NewIsCheckCommand => _newIsCheckCommand ?? (_newIsCheckCommand = new Command(ChangeCurrentTheme));

        async Task ComeBack()
        {
            await _navigationService.NavigateAsync("NavigationPage/MainListPageView");
        }

        private void DateSort()
        {
            _profileSort.SaveSelectSort = "By date";
            IsDate = true;
            ChangeCurrentTheme();
        }

        private void NickNameSort()
        {
            _profileSort.SaveSelectSort = "By nick name";
            IsNickName = true;
        }

        private void NameSort()
        {
            _profileSort.SaveSelectSort = "By name";
            IsName = true;
        }

        private void ChangeCurrentTheme()
        {
            if (App.IsDarkOrLightTheme == false)
            {
                ChangeTheme.TurnOnTheDark();
                App.IsDarkOrLightTheme = true;
            }
            else
            {
                ChangeTheme.TurnOnTheLight();
                App.IsDarkOrLightTheme = false;
            }
        }

        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }
    }
}
