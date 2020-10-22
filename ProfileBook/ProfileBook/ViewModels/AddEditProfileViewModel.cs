using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.RepositoryService;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class AddEditProfileViewModel : BindableBase, IConfirmNavigation,  INavigatedAware
    {
        private readonly INavigationService _navigationService;
        private readonly IRepository<Profile> _repository;

        private int _user_id = App.UserLogin;
        private int _profile_id;
        private string _profileImage = "pic_profile.png";
        private string _nickName = string.Empty;
        private string _name = string.Empty;
        private string _description = string.Empty;
        private DateTime _dateTime = DateTime.Now;
        private bool _isSaveOrUpdate;
        private Profile _profile;
        
        private ICommand _saveProfileCommand;
        private ICommand _actionSheetCommand;

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

        public AddEditProfileViewModel(INavigationService navigationService,
                                        IRepository<Profile> repository)
        {
            _navigationService = navigationService;
            _repository = repository;
        }

        public ICommand SaveProfileCommand => _saveProfileCommand ?? (_saveProfileCommand = new Command(
                        async () => await SaveProfileAsync()));

        public ICommand ActionSheetCommand => _actionSheetCommand ?? (_actionSheetCommand = new Command(OpenActionSheet));

        private void OpenActionSheet()
        {
             UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                            .SetCancel("Cancel")
                            .SetTitle("Adding a photo")
                            .Add("Use camera", ToMakePhotoAsync, "camera.png")
                            .Add("Download from gallery", GetPhotoFromGalleryAsync, "gallery.png")
                            );
        }

        private async void GetPhotoFromGalleryAsync() 
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                ProfileImage = photo.Path;
                if (ProfileImage == null)
                {
                    ProfileImage = "pic_profile.png";
                }
            }
        }

        private async void ToMakePhotoAsync()
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "drawable",
                    Name = $"{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.jpg"
                });

                if (file != null)
                {
                    ProfileImage = file.Path;
                }

                if (ProfileImage == null)
                {
                    ProfileImage = "pic_profile.png";
                }
            }
        }

        private async Task SaveProfileAsync()
        {
            bool isErrorExist = false;

            if(String.IsNullOrEmpty(_nickName) || String.IsNullOrEmpty(_name))
            {
                UserDialogs.Instance.Alert("Fields \"Name\" and \"Nickname\" must be filled", "Error", "ok");
                isErrorExist = true;
            }
            if(isErrorExist == false)
            {
                if (Description.Length > 120)
                {
                    UserDialogs.Instance.Alert("The \"Description\" field is too large, it must be no more than 120 characters", "Error", "Ok");
                    isErrorExist = true;
                }
            }            
            if(isErrorExist == false)
            {
                SaveProfileToDB();
                await _navigationService.NavigateAsync(new Uri("http://WWW.ProfileBook/NavigationPage/MainListPageView", UriKind.Absolute));
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

            if (_isSaveOrUpdate) 
            {
                _repository.UpdateItem(_profile); 
                _isSaveOrUpdate = false;
            }
            else
            {
                _repository.InsertItem(_profile); 
            }
        }
        
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Profile profile = (Profile)parameters["profile"];

            Profile_id = profile.Id;

            NickName = profile.NickName;
            if (NickName == null)
            {
                NickName = string.Empty;
            }

            DateTimePr = profile.DateTimePr;
            if (DateTimePr == null)
            {
                DateTimePr = DateTime.Now;
            }

            ProfileImage = profile.ProfileImage;
            if (ProfileImage == null)
            {
                ProfileImage = "pic_profile.png";
            }

            Description = profile.Description;
            if (Description == null)
            {
                Description = string.Empty;
            }
            
            Name = profile.Name;
            if (Name == null)
            {
                Name = string.Empty;
            }
            else
            {
                _isSaveOrUpdate = true; 
            }
        }       

        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }
    }
}
