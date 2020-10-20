using Acr.UserDialogs;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsPageViewModel : BindableBase, IConfirmNavigation
    {
        private string _sortName = "By date";
        private ICommand _selrctSortCommand;
        private  ISettings AppSettings => CrossSettings.Current;
        public SettingsPageViewModel()
        {
            SortName = SaveSelectSort;
        }
        public  string SaveSelectSort // Saving data to understand whether the user is authorized or not
        {
            get => AppSettings.GetValueOrDefault(nameof(SaveSelectSort), _sortName); 
            set => AppSettings.AddOrUpdateValue(nameof(SaveSelectSort), value);
        }
        public ICommand SelectSortCommand => _selrctSortCommand ?? (_selrctSortCommand = new Command(SelectSort));

        private void SelectSort()
        {
            var dialogAlert = UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                .SetTitle("Select sorting")
                .Add("By Date", null)
                .Add("By Name", null)
                .Add("By nick name", TextLable)
                  );
        }
        private void TextLable()
        {
            SaveSelectSort = "By nick name";

        }
        public string SortName
        {
            get => _sortName;
            set { SetProperty(ref _sortName, value); }
        }
        public bool CanNavigate(INavigationParameters parameters)
        {
            return true;
        }
    }
}
