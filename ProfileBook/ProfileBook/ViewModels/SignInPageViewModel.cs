using Prism.Navigation;
using ProfileBook.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInPageViewModel : INotifyPropertyChanged, IConfirmNavigation
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public User User { get; set; }
        private string _login = string.Empty;
        private string _password = string.Empty;
        private ICommand _enterCommand;
        private readonly INavigationService _navigationService;
        public SignInPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        public ICommand EnterCommand => _enterCommand ?? (_enterCommand = new Command(OpenMainListViewPageAsync));


        async void OpenMainListViewPageAsync(object obj)
        {
            await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/MainListView", System.UriKind.Absolute));
        }

        public string Login
        {
            get { return _login; }
            set
            {
                if (_login != value)
                {
                    _login = value;
                    OnPropertyChanged("Login");
                }
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }


        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }
    }
}
