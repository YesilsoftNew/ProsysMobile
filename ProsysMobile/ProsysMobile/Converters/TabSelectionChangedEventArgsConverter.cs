using System;
using System.Globalization;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using static Xamarin.CommunityToolkit.UI.Views.TabView;

namespace WiseMobile.Converters
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