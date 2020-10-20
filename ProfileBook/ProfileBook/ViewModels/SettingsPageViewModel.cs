using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Services.EnumServices;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsPageViewModel : BindableBase, IConfirmNavigation
    {
        private string _sortName = "By date";
        private ICommand _selrctSortCommand;
        private ICommand _comaBackCommand;
        private readonly IProfileSort _profileSort;
        private readonly INavigationService _navigationService;

        public SettingsPageViewModel(IProfileSort profileSort, INavigationService navigationService)
        {
            _profileSort = profileSort;
            SortName = _profileSort.SaveSelectSort;
            _navigationService = navigationService;
        }

        public ICommand SelectSortCommand => _selrctSortCommand ?? (_selrctSortCommand = new Command(SelectSort));
        public ICommand ComaBackCommand => _comaBackCommand ?? (_comaBackCommand = new Command(
                                            async () => await ComeBack())
                                            );

        async Task ComeBack()
        {
            await _navigationService.NavigateAsync(new Uri("http://WWW.ProfileBook/NavigationPage/MainListPageView", UriKind.Absolute));
        }

        private void SelectSort()
        {
            var dialogAlert = UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                .SetTitle("Select sorting")
                .Add("By Date", DateSort)
                .Add("By Name", NameSort)
                .Add("By nick name", NickNameSort)
                  );
        }
        private void NickNameSort()
        {
            SortName = "By nick name";
            _profileSort.SaveSelectSort = "By nick name";
        }
        private void NameSort()
        {
            SortName = "By name";
            _profileSort.SaveSelectSort = "By name";
        }
        private void DateSort()
        {
            SortName = "By date";
            _profileSort.SaveSelectSort = "By date";
        }
        public string SortName
        {
            get => _sortName;
            set { SetProperty(ref _sortName, value); }
        }
        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }
    }
}
