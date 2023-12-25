
using System.Collections.Generic;
using Android.Content;
using Android.Webkit;
using ProsysMobile.Droid.Renderer;
using ProsysMobile.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using WebView = Xamarin.Forms.WebView;

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

            Control.Settings.JavaScriptEnabled = true;
            
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {customWebView.Token}" }
            };
            
            Control.SetWebChromeClient(new CustomWebChromeClient());

            Control.LoadUrl(customWebView.Uri, headers);
        }
        
        public class CustomWebChromeClient : WebChromeClient
        {
            // Eğer kamera veya dosya seçimi için bir dialog açılmasını istiyorsanız,
            // bu methodları override edebilirsiniz.

            public override void OnPermissionRequest(PermissionRequest request)
            {
                // Bu metod, web içeriği kamera, mikrofon gibi kaynaklara erişmek istediğinde çağrılır.
                // İzinleri burada kontrol edebilir ve onaylayabilirsiniz.
                request.Grant(request.GetResources());
            }

            // Diğer ihtiyaç duyulan override metodları buraya ekleyebilirsiniz.
            // Örneğin, kamera veya dosya yükleme için diyalog yönetimi gibi.
        }
    }
}