using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services;
using ProfileBook.Services.RepositoryService;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignUpPageViewModel : BindableBase, IConfirmNavigation
    {
        public User User { get; set; }
        private string _login = string.Empty;
        private string _password = string.Empty;
        private string _conPassw = string.Empty;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly ICheckPasswordValid _checkPasswordValid;
        private readonly ICheckLoginValid _checkLoginValid;
        private ICommand _signUpCommand;
        private readonly IRepository<User> _repository;
        
        public SignUpPageViewModel(INavigationService navigationService, 
                                    IPageDialogService dialogService, 
                                    ICheckPasswordValid checkPasswordValid, 
                                    ICheckLoginValid checkLoginValid,
                                    IRepository<User> repository)
        {
            _checkPasswordValid = checkPasswordValid;
            _dialogService = dialogService;
            _checkLoginValid = checkLoginValid;
            _navigationService = navigationService;
            _repository = repository;
        }

        public ICommand SignUpCommand => _signUpCommand ?? (_signUpCommand = new Command(
            async () => await SignUpComplete(),
            () => false));    //add property ICommand.CanExecute to keep the button deactivated

        async Task SignUpComplete()
        {                
            if (ChekLoginPasswod() == false)     //If the data is correct, then we return to the previous page
            {
                SaveToDataBase();           //Registering a user
                var parametr = new NavigationParameters     //We send the username and password to the SignIn Page
                {
                    { "log", _login },      
                    { "pas", _password }
                };
                await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/NavigationPage/SignInPageView", System.UriKind.Absolute), parametr);                
            }            
        }
        
        private bool ChekLoginPasswod()
        {
            bool isErrorExist = false;

            if (_checkLoginValid.IsCheckLogin(_login))      //Checking the login for correctness
            {
                _ = _dialogService.DisplayAlertAsync("Incorrect login",
                    "Login must not start with a number, " +
                    "login length must be no less than 4 characters " +
                    "and no more than 16 characters", "ok");

                Login = string.Empty;          //Сlean login entry
                isErrorExist = true;
            }
            if (_checkPasswordValid.IsPasswordValid(_password))     //Checking the password for correctness
            {
                _ = _dialogService.DisplayAlertAsync("Incorrect password",
                    "The password must contain from 8 to 16 characters, " +
                    "among which there must be a capital letter, " +
                    "a small letter, and also a number", "ok");

                Password = string.Empty;      //Сlean password entry
                ConPassw = string.Empty;      //Сlean confirm password entry

                isErrorExist = true;
            }
            if (isErrorExist == false)    //If the login and password are entered correctly, then we check the password confirmation
            {
                if (_password != _conPassw)     //Chek password confirm
                {
                    _ = _dialogService.DisplayAlertAsync("Error",
                    "Password not confirmed", "ok");

                    Password = string.Empty; 
                    ConPassw = string.Empty; 

                    isErrorExist = true;
                }
            }
            if (isErrorExist == false)    //If all the data is entered correctly 
            {
                if (_checkLoginValid.IsCheckLoginDB(_login))    //we check the login for uniqueness in the database
                {
                    _ = _dialogService.DisplayAlertAsync("Error",
                        "This login is already registered", "ok");

                    isErrorExist = true;
                }
            }

            return isErrorExist;
        }
        private void SaveToDataBase()
        {
            User user = new User
            {
                Login = _login,
                Password = _password
            };
            _repository.InsertItem(user);
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
        public string ConPassw
        {
            get { return _conPassw; }
            set { SetProperty(ref _conPassw, value); }
        }

        //This method allows navigation from this page
        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }


    }
}
