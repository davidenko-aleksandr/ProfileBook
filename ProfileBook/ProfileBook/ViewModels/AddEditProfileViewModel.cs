using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class AddEditProfileViewModel : BindableBase, IConfirmNavigation,  INavigatedAware
    {
        Profile _profile;
        private int _user_id = App.UserLogin;
        private int _profile_id;
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
                if (ProfileImage == null)
                    ProfileImage = "pic_profile.png";
                
            }
        }
        //Get a new photo from camera 
        async void ToMakePhoto()
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "drawable",
                    Name = $"{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.jpg"
                });

                if (file == null)
                    return;
                ProfileImage = file.Path;
                if (ProfileImage == null)
                    ProfileImage = "pic_profile.png";

            }
        }
        async Task SaveProfileAsync()
        {
            bool result = false;
            if(String.IsNullOrEmpty(_nickName) || String.IsNullOrEmpty(_name))
            {
                UserDialogs.Instance.Alert("Fields \"Name\" and \"Nickname\" must be filled", "Error", "ok");
                result = true;
            }
            if(result == false)
            {
                if (Description.Length > 120)
                {
                    UserDialogs.Instance.Alert("The \"Description\" field is too large, it must be no more than 120 characters", "Error", "Ok");
                    result = true;
                }
            }            
            if(result == false)
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
                Id = Profile_id,
                ProfileImage = ProfileImage,
                NickName = NickName,
                Name = Name,
                Description = Description,
                DateTimePr = DateTimePr
            };
            App.DatabaseProfile.SaveItem(_profile);
        }
        //Get data from MainListPage
        public void OnNavigatedTo(INavigationParameters parameters)
        {

            NickName = (string)parameters["nickName"];
            if (NickName == null) NickName = "";
            Name = (string)parameters["name"];
            if (Name == null) Name = "";
            Description = (string)parameters["description"];
            if (Description == null) Description = "";
            DateTimePr = (DateTime)parameters["dateTime"];
            Profile_id = (int)parameters["profileId"];
            ProfileImage = (string)parameters["profileImage"];
            if (ProfileImage == null) ProfileImage = "pic_profile.png";
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
        public int User_Id
        {
            get => _user_id;
            set { SetProperty(ref _user_id, value); }
        }
        public int Profile_id
        {
            get => _profile_id;
            set { SetProperty(ref _profile_id, value); }
        }
        public string ProfileImage
        {
            get => _profileImage;
            set { SetProperty(ref _profileImage, value); }
        }
        public string NickName
        {
            get => _nickName;
            set { SetProperty(ref _nickName, value); }
        }
        public string Name
        {
            get => _name;
            set { SetProperty(ref _name, value); }
        }
        public string Description
        {
            get => _description;
            set { SetProperty(ref _description, value); }
        }
        public DateTime DateTimePr
        {
            get => _dateTime;
            set { SetProperty(ref _dateTime, value); }
        }

        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }





    }
}
