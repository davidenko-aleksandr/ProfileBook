using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using System;

namespace ProfileBook.ViewModels
{
    public class ModalProfilePageViewModel : BindableBase, IConfirmNavigation, INavigatedAware
    {
        private string _profileImage;
        private string _nickName;
        private string _name;
        private DateTime _dateTime;
                
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Profile profile = (Profile)parameters["profile"];
            NickName = profile.NickName;
            if (NickName == null) NickName = "";
            Name = profile.Name;
            if (Name == null) Name = "";
            DateTimePr = profile.DateTimePr;
            if (DateTimePr == null) DateTimePr = DateTime.Now;
            ProfileImage = profile.ProfileImage;
            if (ProfileImage == null) ProfileImage = "pic_profile.png";
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
        public DateTime DateTimePr
        {
            get => _dateTime;
            set { SetProperty(ref _dateTime, value); }
        }
        public void OnNavigatedFrom(INavigationParameters parameters) { }
        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }
    }
}
