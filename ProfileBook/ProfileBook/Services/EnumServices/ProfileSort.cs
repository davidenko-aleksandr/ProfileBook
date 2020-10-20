using Plugin.Settings;
using Plugin.Settings.Abstractions;
using ProfileBook.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.EnumServices
{
    public  class ProfileSort : IProfileSort
    {
        private readonly string _sortName = "By date";

        public string SaveSelectSort
        {
            get => AppSettings.GetValueOrDefault(nameof(SaveSelectSort), _sortName);
            set => AppSettings.AddOrUpdateValue(nameof(SaveSelectSort), value);
        }

        private ISettings AppSettings => CrossSettings.Current;


    }
}
