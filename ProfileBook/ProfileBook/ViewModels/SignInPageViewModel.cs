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
        public User User { get; set; }
        private string _login = string.Empty;
        private string _password = string.Empty;
        private ICommand _enterCommand;
        private ICommand _toSignUpPageCommand;
        private readonly INavigationService _navigationService; 
        private readonly IUserAuthentication _userAuthentication;
        private readonly IPageDialogService _dialogService;
        private readonly IAuthorizationService _authorization;

        //The constructor 
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
                        () => false)    //add property ICommand.CanExecute to keep the button deactivated
                        );
        public ICommand ToSignUpPageCommand => _toSignUpPageCommand ?? (_toSignUpPageCommand = new Command(
                        async () => await OpenSignUpPageAsync())
                        );

        async Task OpenSignUpPageAsync() //Open the user registration page
        {
            _ = await _navigationService.NavigateAsync("SignUpPageView");
        }

        async Task OpenMainListViewPageAsync()
        {
            _userAuthentication.GetUsersFromDB(_login, _password);  //Сheck if there is such a user in the database
            if (_userAuthentication.IsPasswordConfirm())    //Check if the password is correct and open MainList Page
            {
                _authorization.AddUodateUserId(_userAuthentication.GetUserId());
                _authorization.ToWriteLoginId();
                await _navigationService.NavigateAsync(new Uri("http://WWW.ProfileBook/NavigationPage/MainListPageView", UriKind.Absolute));
            }
            else
            {
                _ = _dialogService.DisplayAlertAsync("Error", "Invalid login or password", "Ok");   //if password is incorrect, show a message
                Login = "";         //and clean login and password fields
                Password = "";
            }
                
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
        //This method allows navigation from this page
        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }
        public void OnNavigatedTo(INavigationParameters parameters) //Accept parameters from other pages
        {
            var getLog = parameters["log"];
            var getPas = parameters["pas"];
            Login = (string)getLog;
            Password = (string)getPas;
        }
    }
}
