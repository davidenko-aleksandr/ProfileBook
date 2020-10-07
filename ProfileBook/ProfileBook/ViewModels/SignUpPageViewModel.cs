using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
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
        public ICommand ToSignInCommand => _toSignInCommand ?? (_toSignInCommand = new Command(ComeBackToSignViewPage));

        public SignUpPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        async void ComeBackToSignViewPage(object obj)
        {
            await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/SignInPageView", System.UriKind.Absolute));
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
        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }
    }
}
