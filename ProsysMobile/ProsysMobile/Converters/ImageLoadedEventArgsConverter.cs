using Syncfusion.SfImageEditor.XForms;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProsysMobile.Converters
{
    public class ImageLoadedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ImageLoadedEventArgs eventArgs))
                throw new ArgumentException("Expected FocusEventArgs as value", "value");

            return eventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}