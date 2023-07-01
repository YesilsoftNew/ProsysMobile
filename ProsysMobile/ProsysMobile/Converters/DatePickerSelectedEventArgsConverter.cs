using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProsysMobile.Converters
{
    public class DatePickerSelectedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DateChangedEventArgs eventArgs))
                throw new ArgumentException("Expected DateChangedEventArgs as value", "value");

            return eventArgs.NewDate;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
