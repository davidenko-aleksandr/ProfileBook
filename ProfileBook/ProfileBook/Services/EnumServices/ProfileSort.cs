using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace ProfileBook.Services.EnumServices
{
    public  class ProfileSort : IProfileSort
    {
        private readonly string _sortName = "By date";

        //Save the name of the selected sort locally
        public string SaveSelectSort
        {
            get => AppSettings.GetValueOrDefault(nameof(SaveSelectSort), _sortName);
            set => AppSettings.AddOrUpdateValue(nameof(SaveSelectSort), value);
        }

        private ISettings AppSettings => CrossSettings.Current;


    }
}
