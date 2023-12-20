
using System.Collections.Generic;
using Android.Content;
using ProsysMobile.Droid.Renderer;
using ProsysMobile.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace ProsysMobile.Droid.Renderer
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        public CustomWebViewRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (!(e.NewElement is CustomWebView customWebView)) return;
            
            Control.Settings.SetSupportZoom(false);
            Control.Settings.BuiltInZoomControls = false;
            Control.Settings.DisplayZoomControls = false;
            
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {customWebView.Token}" }
            };
            
            Control.LoadUrl(customWebView.Uri, headers);
        }
    }
}