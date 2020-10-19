using Acr.UserDialogs;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.AuthorizationServices;
using ProfileBook.Services.RepositoryService;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainListPageViewModel : BindableBase, IConfirmNavigation, INotifyPropertyChanged
    {
        private Profile _profile; 
        private string _lableText = string.Empty;
        private readonly INavigationService _navigationService;
        private ICommand _exitCommand;
        private ICommand _addProfileCommand;
        private ICommand _deleteProfileCommand;
        private ICommand _editProfileCommand;
        private ICommand _openSettingPageCommand;
        private ICommand _itemOpenCommand;
        private readonly IRepository<Profile> _repository;
        private readonly IAuthorizationService _authorization;
        public ObservableCollection<Profile> ProfileCollection { get; set; }

        public MainListPageViewModel(INavigationService navigationService,
                                    IAuthorizationService authorization,
                                    IRepository<Profile> repository)
        {
            _navigationService = navigationService;
            _authorization = authorization;
            _repository = repository;
            InitTable();
        }

        public void InitTable()
        {            
            ProfileCollection = new ObservableCollection<Profile>(_repository.GetAllItems().Where(p => p.User_Id==App.UserLogin));
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
        public ICommand EditProfileCommand => _editProfileCommand ?? (_editProfileCommand = new Command(
                        async (Object obj) => await EditProfile(obj))
                        );
        public ICommand OpenSettingPageCommand => _openSettingPageCommand ?? (_openSettingPageCommand = new Command(
                        async () => await OpenSettingPage())
                        );
        public ICommand ItemOpenCommand => _itemOpenCommand ?? (_itemOpenCommand = new Command(
                        async (Object obj) => await OpenItem(obj))
                        );

        async Task OpenItem(object obj)
        {
            _profile = obj as Profile;
            var parametr = new NavigationParameters
            {
                {"profile", _profile }
            };
            await _navigationService.NavigateAsync(new Uri("ModalProfilePageView", UriKind.Relative), parametr); 
        }

        async Task EditProfile(object obj)
        {
            _profile = obj as Profile;
            var parametr = new NavigationParameters
            {
                {"profile", _profile }
            };
            await _navigationService.NavigateAsync(new Uri("AddEditProfileView", UriKind.Relative), parametr);            
        }
        async Task DeleteProfile(object obj)
        {
            _profile = obj as Profile;
            int profileId;
            var result = await UserDialogs.Instance.ConfirmAsync(new ConfirmConfig
            {
                Message = "Do you confirm deletion?",
                OkText = "Delete",
                CancelText = "Cancel"
            }
            );
            if (_profile != null && result == true)
            {
                profileId = _profile.Id;
                _repository.DeleteItem(profileId);
                ProfileCollection = new ObservableCollection<Profile>(_repository.GetAllItems().Where(p => p.User_Id == App.UserLogin));
            }
            await _navigationService.NavigateAsync(new Uri("http://WWW.ProfileBook/NavigationPage/MainListPageView", UriKind.Absolute));
        }

        async Task AddProfileAsync()
        {
            await _navigationService.NavigateAsync("AddEditProfileView");
        }

        async Task ExitFromProfileAsync()
        {
            _authorization.AddUodateUserId(0);
            _authorization.ToWriteLoginId();
            await _navigationService.NavigateAsync(new Uri("http://WWW.ProfileBook/NavigationPage/SignInPageView", UriKind.Absolute));
        }
        async Task OpenSettingPage()
        {
            await _navigationService.NavigateAsync("SettingsPageView");
        }
        public string LableText //This label is displayed when there is no profile list
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
