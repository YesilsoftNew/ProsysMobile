using System;
using System.Globalization;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace ProsysMobile.Converters
{
    public class TabSelectionChangedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is TabSelectionChangedEventArgs eventArgs))
                throw new ArgumentException("Expected TabSelectionChangedEventArgs as value", "value");
            
            return eventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}