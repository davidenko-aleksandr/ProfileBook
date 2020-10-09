using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using System.Text.RegularExpressions;
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
        private ICommand _toSignInCommand;
        private ICommand _signUpCommand;


        public ICommand ToSignInCommand => _toSignInCommand ?? (_toSignInCommand = new Command(ComeBackToSignViewPage));
        public ICommand SignUpCommand => _signUpCommand ?? (_signUpCommand = new Command(
            async () => await ToSignUp(), 
            () => false)    //add property ICommand.CanExecute to keep the button deactivated
            );
  

        public SignUpPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        async void ComeBackToSignViewPage(object obj)
        {
            await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/SignInPageView", System.UriKind.Absolute));
        }
        async Task ToSignUp()
        {
            //If the data is correct, then we return to the previous page
            if (ChekLoginPasswod() == false)
            {
                await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/SignInPageView", System.UriKind.Absolute));
            }
            
        }
        //essage display in case of incorrect data of registration fields
        private bool ChekLoginPasswod()
        {
            bool result = false;
            if (IsLoginValid())
            {
                _ = Application.Current.MainPage.DisplayAlert("Incorrect login",
                    "Login must not start with a number, " +
                    "login length must be no less than 4 characters " +
                    "and no more than 16 characters", "ok");
                Login = "";         //Сlean login entry
                result = true;
            }
            if (IsPasswordValid())
            {
                _ = Application.Current.MainPage.DisplayAlert("Incorrect password",
                    "Password must contain an uppercase letter", "ok");
                Password = "";      //Сlean password entry
                ConPassw = "";      //Сlean confirm password entry
                result = true;
            }
            if (result == false)
            {
                if (_password != _conPassw) //Chek password confirm
                {
                    _ = Application.Current.MainPage.DisplayAlert("Error",
                    "Password not confirmed", "ok");
                    Password = "";
                    ConPassw = "";
                    result = true;
                }
            }
            return result;
        }

        //Checking the login for correctness
        private bool IsLoginValid()
        {
            string pattern = @"^\d\w*";
            if (_login.Length < 4 || 
                _login.Length > 16 ||
                Regex.IsMatch(_login, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            else return false;
        }
        //Checking the password for correctness
        private bool IsPasswordValid()
        {
            bool isCapitalLetter = Regex.IsMatch(_password, "[A-Z]{1}");
            bool isSmallLatter = Regex.IsMatch(_password, "[a-z]{1}");
            bool isNumber = Regex.IsMatch(_password, @"\d\w*");
            bool result;
            if (isCapitalLetter == true)
                result = false;
            else result = true;
            if (result == false)
            {
                if (isSmallLatter) 
                    result = false;
                else result = true;
            }
            if (result == false)
            {
                if (isNumber) 
                    result = false;
                else result = true;
            }
            return result; 
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
