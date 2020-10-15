using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
        private ICommand _actionSheetCommand;


        public AddEditProfileViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public ICommand SaveProfileCommand => _saveProfileCommand ?? (_saveProfileCommand = new Command(
                        async () => await SaveProfileAsync())
                        );
        public ICommand ActionSheetCommand => _actionSheetCommand ?? (_actionSheetCommand = new Command(OpenActionSheet));

        private void OpenActionSheet()
        {
            var dialogAlert = UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                            .SetTitle("Adding a photo")
                            .Add("Use camera", ToMakePhoto, "camera.png")
                            .Add("Download from gallery", GetPhotoFromGallery, "gallery.png")
                        );
        }

        async void GetPhotoFromGallery() 
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                ProfileImage = photo.Path;
            }

        }
        async void ToMakePhoto()
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "drawable",
                    Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg"
                });

                if (file == null)
                    return;
                ProfileImage = file.Path;
                
            }
        }
        async Task SaveProfileAsync()
        {
            if(String.IsNullOrEmpty(_nickName) || String.IsNullOrEmpty(_name))
            {
                UserDialogs.Instance.Alert("Fields \"Name\" and \"Nickname\" must be filled", "Exception", "ok");
            }
            if(DescriptionLength())
            {
                _ = UserDialogs.Instance.Alert("The \"Description\" field is too large, it must be no more than 120 characters", "Error", "Ok");
            }
            else
            {
                SaveProfileToDB();

                await _navigationService.NavigateAsync(new System.Uri("http://www.ProfileBook/NavigationPage/MainListView", System.UriKind.Absolute));
            }
           
        }
        public void ShowActionSheet() => _ = UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                            .SetTitle("Choose Type")
                            .Add("Default", null, "camera.png")
                            .Add("E-Mail", null, "gallery.png")
                        );
  

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
        private bool DescriptionLength()
        {
            if (Description.Length > 120)
            {
                
                return true;
            }
            else return false;
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
