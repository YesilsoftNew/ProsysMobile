using Foundation;
using UIKit;

namespace ProsysMobile.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            //Backdrop.Initializer.Init();
            //Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();

            Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }
    }
}
