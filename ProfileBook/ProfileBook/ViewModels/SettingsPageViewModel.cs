using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Services.EnumServices;
using ProfileBook.Themes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsPageViewModel : BindableBase, IConfirmNavigation
    {
        private ICommand _comaBackCommand;
        private ICommand _bydateCommand;
        private ICommand _byNickNameCommand;
        private ICommand _bynameCommand;
        private ICommand _newIsCheckCommand;
        private readonly IProfileSort _profileSort;
        private readonly INavigationService _navigationService;
        private bool _isdate;
        private bool _inickName;
        private bool _isname;
        private bool _isCheckedDark;

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
        public ICommand NewIsCheckCommand => _newIsCheckCommand ?? (_newIsCheckCommand = new Command(TestMethod));

        private void TestMethod(object obj)
        {
            NameSort();
        }

        async Task ComeBack()
        {
            await _navigationService.NavigateAsync(new Uri("http://WWW.ProfileBook/NavigationPage/MainListPageView", UriKind.Absolute));
        }

        private void DateSort()
        {
            _profileSort.SaveSelectSort = "By date";
            IsDate = true;
            TurnOnDarkThema();
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
        private void TurnOnDarkThema()
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(new DarkTheme());
            }
        }

        public bool IsCheckedDark
        {
            get { return _isCheckedDark; }
            set { SetProperty(ref _isCheckedDark, value); }
        }
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
        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }
    }
}
