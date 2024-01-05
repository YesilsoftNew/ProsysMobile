using Foundation;
using ProsysMobile.iOS.Renderer;
using ProsysMobile.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace ProsysMobile.iOS.Renderer
{
    public class CustomWebViewRenderer : WkWebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (!(e.NewElement is CustomWebView customWebView)) return;

            var url = new NSUrl(customWebView.Uri);
            var request = new NSMutableUrlRequest(url)
            {
                Headers = NSDictionary.FromObjectAndKey(
                    new NSString($"Bearer {customWebView.Token}"), 
                    new NSString("Authorization"))
            };
            
            LoadRequest(request);
        }
        
    }
}