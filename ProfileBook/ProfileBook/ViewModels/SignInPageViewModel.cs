using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services.AuthenticationServices;
using ProfileBook.Services.AuthorizationServices;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInPageViewModel : BindableBase, IConfirmNavigation, INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IUserAuthentication _userAuthentication;
        private readonly IPageDialogService _dialogService;
        private readonly IAuthorizationService _authorization;
        
        private string _login = string.Empty;
        private string _password = string.Empty;

        private ICommand _enterCommand;
        private ICommand _toSignUpPageCommand;

        public User User { get; set; }


        public SignInPageViewModel(INavigationService navigationService,
                                    IUserAuthentication userAuthentication,
                                    IPageDialogService dialogService,
                                    IAuthorizationService authorization)
        {
            _navigationService = navigationService;
            _userAuthentication = userAuthentication;
            _dialogService = dialogService;
            _authorization = authorization;
        }

        public ICommand EnterCommand => _enterCommand ?? (_enterCommand = new Command(
                        async () => await OpenMainListViewPageAsync(), 
                        () => false));    

        public ICommand ToSignUpPageCommand => _toSignUpPageCommand ?? (_toSignUpPageCommand = new Command(
                        async () => await OpenSignUpPageAsync()));

        async Task OpenSignUpPageAsync()
        {
             await _navigationService.NavigateAsync("SignUpPageView");
        }

        async Task OpenMainListViewPageAsync()
        {
            _userAuthentication.GetUsersFromDB(_login, _password);

            if (_userAuthentication.IsPasswordConfirm())  
            {
                _authorization.AddUodateUserId(_userAuthentication.GetUserId());
                _authorization.ToWriteLoginId();
                await _navigationService.NavigateAsync(new Uri("http://WWW.ProfileBook/NavigationPage/MainListPageView", UriKind.Absolute));
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Error", "Invalid login or password", "Ok"); 
                Login = string.Empty;         
                Password = string.Empty;
            }                
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Login = (string)parameters["log"];
            Password = (string)parameters["pas"];            
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

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }
    }
}
