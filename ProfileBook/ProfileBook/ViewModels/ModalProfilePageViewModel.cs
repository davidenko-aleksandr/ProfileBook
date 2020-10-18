using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.ViewModels
{
    public class ModalProfilePageViewModel : BindableBase, IConfirmNavigation, INavigatedAware
    {
        private string _profileImage = "";
        private string _nickName = string.Empty;
        private string _name = string.Empty;
        private DateTime _dateTime = DateTime.Now;
        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }
        public ModalProfilePageViewModel() { }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            NickName = (string)parameters["nickName"];
            if (NickName == null) NickName = "";
            Name = (string)parameters["name"];
            if (Name == null) Name = "";
            DateTimePr = (DateTime)parameters["dateTime"];
            if (DateTimePr == null) DateTimePr = DateTime.Now;
            ProfileImage = (string)parameters["profileImage"];
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
    }
}
