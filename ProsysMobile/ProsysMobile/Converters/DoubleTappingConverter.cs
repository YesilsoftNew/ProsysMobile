using System;
using System.Globalization;
using Xamarin.Forms;

namespace WiseMobile.Converters
{
    public class DoubleTappingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool eventArgs))
                throw new ArgumentException("Expected DateChangedEventArgs as value", "value");

            return !eventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
