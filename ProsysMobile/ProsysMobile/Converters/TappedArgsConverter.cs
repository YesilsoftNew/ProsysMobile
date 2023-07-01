using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProsysMobile.Converters
{
    public class TappedArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TappedEventArgs eventArgs))
                throw new ArgumentException("Expected TappedEventArgs as value", "value");

            return eventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}