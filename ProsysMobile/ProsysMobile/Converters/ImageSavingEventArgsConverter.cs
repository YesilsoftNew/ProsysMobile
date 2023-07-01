using Syncfusion.SfImageEditor.XForms;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace WiseMobile.Converters
{
    public class ImageSavingEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ImageSavingEventArgs eventArgs))
                throw new ArgumentException("Expected FocusEventArgs as value", "value");

            return eventArgs;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}