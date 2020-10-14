using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class AddEditProfileViewModel : BindableBase, IConfirmNavigation
    {
        Profile _profile;
        private int _user_id = App.UserLogin;
        private string _profileImage = "pic_profile.png";
        private string _nickName = string.Empty;
        private string _name = string.Empty;
        private string _description = string.Empty;
        private DateTime _dateTime = DateTime.Now;
        private readonly INavigationService _navigationService;
        private ICommand _saveProfileCommand;


        public AddEditProfileViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand SaveProfileCommand => _saveProfileCommand ?? (_saveProfileCommand = new Command(
                        async () => await SaveProfileAsync())
                        );

        async Task SaveProfileAsync()
        {
            if(String.IsNullOrEmpty(_nickName) || String.IsNullOrEmpty(_name))
            {
                NickName = "Fuck you";
                Name = "Write something";
            }
            else
            {
                SaveProfileToDB();

                await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/NavigationPage/MainListView", System.UriKind.Absolute));
            }
           
        }

        private void SaveProfileToDB()
        {
            _profile = new Profile
            {
                User_Id = User_Id,
                ProfileImage = ProfileImage,
                NickName = NickName,
                Name = Name,
                Description = Description,
                DateTime = DateTime
            };
            App.DatabaseProfile.SaveItem(_profile);
        }
        public int User_Id
        {
            get { return _user_id; }
            set 
            {
                SetProperty(ref _user_id, value);
            }
        }
        public string ProfileImage
        {
            get { return _profileImage; }
            set 
            {
                
                SetProperty(ref _profileImage, value);
            }
        }
        public string NickName
        {
            get { return _nickName; }
            set { SetProperty(ref _nickName, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        public DateTime DateTime
        {
            get { return _dateTime; }
            set { SetProperty(ref _dateTime, value); }
        }

        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }
    }
}
