using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProsysMobile.Converters
{
    public class SelectedItemChangedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SelectedItemChangedEventArgs eventArgs))
                throw new ArgumentException("Expected SelectedItemChangedEventArgs as value", "value");
            
            return eventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}