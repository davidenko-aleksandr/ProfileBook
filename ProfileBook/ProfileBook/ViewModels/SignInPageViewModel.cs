using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
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
        public SignInPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public ICommand EnterCommand => _enterCommand ?? (_enterCommand = new Command(
                        async () => await OpenMainListViewPageAsync(), 
                        () => false)    //add property ICommand.CanExecute to keep the button deactivated
                        );
        public ICommand ToSignUpPageCommand => _toSignUpPageCommand ?? (_toSignUpPageCommand = new Command(
                        async () => await OpenSignUpPageAsync())
                        );
            
        
        async Task OpenSignUpPageAsync()
        {
            await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/SignUpPageView", System.UriKind.Absolute));
        }

        async Task OpenMainListViewPageAsync()
        {
            await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/MainListView", System.UriKind.Absolute));
            
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

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var getLog = parameters["log"];
            var getPas = parameters["pas"];
            Login = (string)getLog;
            Password = (string)getPas;
        }
    }
}
