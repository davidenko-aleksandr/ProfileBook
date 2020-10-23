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
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly ICheckPasswordValid _checkPasswordValid;
        private readonly ICheckLoginValid _checkLoginValid;
        private readonly IRepository<User> _repository;

        private string _login = string.Empty;
        private string _password = string.Empty;
        private string _conPassw = string.Empty;

        private ICommand _signUpCommand;
        public User User { get; set; }

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
            async () => await SignUpCompleteAsync(),
            () => false));   

        private async Task SignUpCompleteAsync()
        {                
            if (await ChekLoginPasswodAsync() == false)     
            {
                SaveToDataBase();   
                
                var parametr = new NavigationParameters     
                {
                    { "log", _login },      
                    { "pas", _password }
                };
                await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/NavigationPage/SignInPageView", System.UriKind.Absolute), parametr);                
            }            
        }
        
        private async Task<bool> ChekLoginPasswodAsync()
        {
            bool isErrorExist = false;

            if (_checkLoginValid.IsCheckLogin(_login))      
            {
                await _dialogService.DisplayAlertAsync("Incorrect login",
                    "Login must not start with a number, " +
                    "login length must be no less than 4 characters " +
                    "and no more than 16 characters", "ok");

                Login = string.Empty;        
                isErrorExist = true;
            }
            if (_checkPasswordValid.IsPasswordValid(_password))    
            {
                await _dialogService.DisplayAlertAsync("Incorrect password",
                    "The password must contain from 8 to 16 characters, " +
                    "among which there must be a capital letter, " +
                    "a small letter, and also a number", "ok");

                Password = string.Empty;      
                ConPassw = string.Empty;   

                isErrorExist = true;
            }
            if (isErrorExist == false)    
            {
                if (_password != _conPassw)   
                {
                    await _dialogService.DisplayAlertAsync("Error",
                    "Password not confirmed", "ok");

                    Password = string.Empty; 
                    ConPassw = string.Empty; 

                    isErrorExist = true;
                }
            }
            if (isErrorExist == false)   
            {
                if (_checkLoginValid.IsCheckLoginDB(_login)) 
                {
                    await _dialogService.DisplayAlertAsync("Error",
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

        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }
    }
}
