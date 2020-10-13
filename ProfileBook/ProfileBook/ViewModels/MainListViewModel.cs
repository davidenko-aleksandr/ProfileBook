using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.AuthorizationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListViewModel : BindableBase, IConfirmNavigation
    {
        private readonly INavigationService _navigationService;
        private ICommand _exitCommand;
        private ICommand _addProfileCommand;
        private readonly IAuthorizationService _authorization;


        public MainListViewModel(INavigationService navigationService, IAuthorizationService authorization)
        {
            _navigationService = navigationService;
            _authorization = authorization;
        }
        public ObservableCollection<Profile> ProfileCollection { get; set; }
        public void InitTable()
        {
            ProfileCollection = new ObservableCollection<Profile>(App.DatabaseProfile.GetItems());
        }
        public ICommand ExitCommand => _exitCommand ?? (_exitCommand = new Command(
                        async () => await ExitFromProfileAsync())
                        );
        public ICommand AddProfileCommand => _addProfileCommand ?? (_addProfileCommand = new Command(
                        async () => await AddProfileAsync()) 
                        );

        async Task AddProfileAsync()
        {
            await _navigationService.NavigateAsync("AddEditProfileView");
        }

        async Task ExitFromProfileAsync()
        {
            _authorization.AddUodateUserId(-1);
            _authorization.ToWriteLoginId();
            await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/SignInPageView", System.UriKind.Absolute));
        }

        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }

       
    }
}
