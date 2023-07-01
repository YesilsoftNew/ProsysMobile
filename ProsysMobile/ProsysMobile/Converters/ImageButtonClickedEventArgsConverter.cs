using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProsysMobile.Converters
{
    public class ImageButtonClickedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ClickedEventArgs eventArgs))
                throw new ArgumentException("Expected System.EventArgs as value", "value");

            return eventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
