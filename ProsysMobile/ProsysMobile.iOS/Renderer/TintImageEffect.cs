using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using WiseMobile.Helper;
using WiseMobile.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FormsTintImageEffect = WiseMobile.Renderer.TintImageEffect;

// Img croplama için kapattım burayı burası kapalı olduğu için çalışmaz suanda
//[assembly: ResolutionGroupName(WiseDynamicMobile.Renderer.TintImageEffect.GroupName)]
[assembly: ExportEffect(typeof(TintImageEffect), WiseMobile.Renderer.TintImageEffect.Name)]
namespace WiseMobile.iOS.Renderer
{
    public class TintImageEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (FormsTintImageEffect)Element.Effects.FirstOrDefault(e => e is FormsTintImageEffect);

                if (effect == null || !(Control is UIImageView image))
                    return;

                image.Image = image.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                image.TintColor = effect.TintColor.ToUIColor();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);

                System.Diagnostics.Debug.WriteLine($"An error occurred when setting the {typeof(TintImageEffect)} effect: {ex.Message}\n{ex.StackTrace}");
            }
        }

        protected override void OnDetached() { }
    }
}