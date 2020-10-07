using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInPageViewModel : BindableBase, IConfirmNavigation
    {

        public User User { get; set; }
        private string _login = string.Empty;
        private string _password = string.Empty;
        private ICommand _enterCommand;
        private ICommand _toSignUpPageCommand;
        private readonly INavigationService _navigationService;
        public SignInPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public ICommand EnterCommand => _enterCommand ?? (_enterCommand = new Command(OpenMainListViewPageAsync));
        public ICommand ToSignUpPageCommand => _toSignUpPageCommand ?? (_toSignUpPageCommand = new Command(OpenSignUpPageAsync));


        async void OpenMainListViewPageAsync(object obj)
        {
            await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/MainListView", System.UriKind.Absolute));
        }
        async void OpenSignUpPageAsync(object obj)
        {
            await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/SignUpPageView", System.UriKind.Absolute));
        }
        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }
    }
}
