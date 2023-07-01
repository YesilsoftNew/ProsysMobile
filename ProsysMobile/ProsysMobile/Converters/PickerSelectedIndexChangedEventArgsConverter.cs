using System;
using System.Globalization;
using Xamarin.Forms;

namespace WiseMobile.Converters
{
    public class PickerSelectedIndexChangedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is EventArgs eventArgs))
                throw new ArgumentException("Expected EventArgs as value", "value");
            
            return eventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}