using Xamarin.Forms;

namespace ProsysMobile.Renderer
{
    public class CustomWebView : WebView
    {
        public static readonly BindableProperty TokenProperty = 
            BindableProperty.Create(nameof(Token), typeof(string), typeof(CustomWebView), default(string));

        public string Token
        {
            get => (string)GetValue(TokenProperty);
            set => SetValue(TokenProperty, value);
        }

        public static readonly BindableProperty UriProperty =
            BindableProperty.Create(nameof(Uri), typeof(string), typeof(CustomWebView), default(string));

        public string Uri
        {
            get => (string)GetValue(UriProperty);
            set => SetValue(UriProperty, value);
        }
    }
}