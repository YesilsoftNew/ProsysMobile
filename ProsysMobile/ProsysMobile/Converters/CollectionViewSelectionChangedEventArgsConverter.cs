using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProsysMobile.Converters
{
    public class CollectionViewSelectionChangedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SelectionChangedEventArgs eventArgs))
                throw new ArgumentException("Expected SelectionChangedEventArgs as value", "value");

            return eventArgs.CurrentSelection;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
