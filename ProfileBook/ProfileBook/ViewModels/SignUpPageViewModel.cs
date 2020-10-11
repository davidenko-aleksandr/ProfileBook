using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ProfileBook.Models;
using ProfileBook.Services;
using System.Linq;
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
        private ICommand _toSignInCommand;
        private ICommand _signUpCommand;

        // THE CONSTRUCTOR
        public SignUpPageViewModel(INavigationService navigationService, 
            IPageDialogService dialogService, 
            ICheckPasswordValid checkPasswordValid, 
            ICheckLoginValid checkLoginValid
            )
        {
            _checkPasswordValid = checkPasswordValid;
            _dialogService = dialogService;
            _checkLoginValid = checkLoginValid;
            _navigationService = navigationService;
        }

        public ICommand ToSignInCommand => _toSignInCommand ?? (_toSignInCommand = new Command(ComeBackToSignViewPage));
        public ICommand SignUpCommand => _signUpCommand ?? (_signUpCommand = new Command(
            async () => await ToSignUp(),
            () => false)    //add property ICommand.CanExecute to keep the button deactivated
            );

        async void ComeBackToSignViewPage(object obj)
        {
            await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/SignInPageView", System.UriKind.Absolute));
        }
        async Task ToSignUp()
        {
            var parametr = new NavigationParameters
            {
                { "log", _login },
                { "pas", _password }
            };
            //If the data is correct, then we return to the previous page
            if (ChekLoginPasswod() == false)
            {
                SaveToDataBase();
                await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/SignInPageView", System.UriKind.Absolute), parametr);
                
            }
            
        }
        //essage display in case of incorrect data of registration fields
        private bool ChekLoginPasswod()
        {
            bool result = false;
            if (_checkLoginValid.IsCheckLogin(_login))      //Checking the login for correctness
            {
                _dialogService.DisplayAlertAsync("Incorrect login",
                    "Login must not start with a number, " +
                    "login length must be no less than 4 characters " +
                    "and no more than 16 characters", "ok");
                Login = "";         //Сlean login entry
                result = true;
            }
            if (_checkPasswordValid.IsPasswordValid(_password))     //Checking the password for correctness
            {
                _dialogService.DisplayAlertAsync("Incorrect password",
                    "Password must contain an uppercase letter", "ok");
                Password = "";      //Сlean password entry
                ConPassw = "";      //Сlean confirm password entry
                result = true;
            }
            if (result == false)    //If the login and password are entered correctly, then we check the password confirmation
            {
                if (_password != _conPassw)     //Chek password confirm
                {
                    _dialogService.DisplayAlertAsync("Error",
                    "Password not confirmed", "ok");
                    Password = "";
                    ConPassw = "";
                    result = true;
                }
            }
            if (result == false)    //If all the data is entered correctly, we check the login for uniqueness in the database
            {
                if (_checkLoginValid.IsCheckLoginDB(_login))
                {
                    _dialogService.DisplayAlertAsync("Ups",
                        "This login is already registered", "ok");
                    result = true;
                }
            }
            return result;
        }

        //private bool IsCheckLoginDB(string login)
        //{
        //    var lg = App.Database.GetItems().FirstOrDefault(user => user.Login == login);
        //    if (lg != null)
        //        return true;
        //    else return false;
        //}
        private void SaveToDataBase()
        {
            User user = new User
            {
                Login = _login,
                Password = _password
            };
            _ = App.Database.SaveItem(user);
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
