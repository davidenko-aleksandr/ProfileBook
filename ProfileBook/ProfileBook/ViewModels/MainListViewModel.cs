using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.AuthorizationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListViewModel : BindableBase, IConfirmNavigation, INotifyPropertyChanged
    {
        private Profile _profile; 
        private string _lableText = string.Empty;
        private readonly INavigationService _navigationService;
        private ICommand _exitCommand;
        private ICommand _addProfileCommand;
        private ICommand _deleteProfileCommand;
        private readonly IAuthorizationService _authorization;
        public ObservableCollection<Profile> ProfileCollection { get; set; }

        public MainListViewModel(INavigationService navigationService, IAuthorizationService authorization)
        {
            _navigationService = navigationService;
            _authorization = authorization;
            InitTable();
        }

        public void InitTable()
        {
            
            ProfileCollection = new ObservableCollection<Profile>(App.DatabaseProfile.GetItems().Where(p => p.User_Id==App.UserLogin));
            LableText = ProfileCollection.Count == 0 ? "No profiles added" : "";

        }
        public ICommand ExitCommand => _exitCommand ?? (_exitCommand = new Command(
                        async () => await ExitFromProfileAsync())
                        );
        public ICommand AddProfileCommand => _addProfileCommand ?? (_addProfileCommand = new Command(
                        async () => await AddProfileAsync()) 
                        );
        public ICommand DeleteProfileCommand => _deleteProfileCommand ?? (_deleteProfileCommand = new Command(
                        async (Object obj) => await DeleteProfile(obj))
                        );

        async Task DeleteProfile(object obj)
        {
            _profile = obj as Profile;
            int profileId;
            if (_profile != null)
            {
                profileId = _profile.Id;
                App.DatabaseProfile.DeleteItem(profileId);
                ProfileCollection = new ObservableCollection<Profile>(App.DatabaseProfile.GetItems().Where(p => p.User_Id == App.UserLogin));
            }
            else throw new ArgumentException("Fuck!");
            await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/NavigationPage/MainListView", System.UriKind.Absolute));
        }

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
        public string LableText
        { 
            get { return _lableText; }
            set { SetProperty(ref _lableText, value); }
        }
        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }

       
    }
}
