using System.Collections.Generic;
using Xamarin.Forms;

namespace ProfileBook.Themes
{
    public static class ChangeTheme 
    {
        private static readonly ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;

        public static void TurnOnTheDark()
        {      
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(new DarkTheme());
            }            
        }

        public static void TurnOnTheLight()
        {
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(new LightTheme());
            }
        }
    }
}
