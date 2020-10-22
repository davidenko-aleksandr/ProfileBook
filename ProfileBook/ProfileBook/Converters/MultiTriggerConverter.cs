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
            return (int)value > 0;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
