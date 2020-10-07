using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProfileBook.Converters
{
    //This class is needed to be used in a trigger 
    //and change the activity property of the button
    public class MultiTriggerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value > 0)     // length > 0 ?
                return true;        // some data has been entered
            else
                return false;       // input is empty
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
