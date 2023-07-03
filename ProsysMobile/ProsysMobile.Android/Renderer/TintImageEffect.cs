using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProsysMobile.Droid.Renderer;
using ProsysMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FormsTintImageEffect = ProsysMobile.Renderer.TintImageEffect;

// Img croplama için kapattım burayı burası kapalı olduğu için çalışmaz suanda
//[assembly: ResolutionGroupName(WiseDynamicMobile.Renderer.TintImageEffect.GroupName)]
[assembly: ExportEffect(typeof(TintImageEffect), ProsysMobile.Renderer.TintImageEffect.Name)]
namespace ProsysMobile.Droid.Renderer
{
    public class TintImageEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (FormsTintImageEffect)Element.Effects.FirstOrDefault(e => e is FormsTintImageEffect);

                if (effect == null || !(Control is ImageView image))
                    return;

                var filter = new PorterDuffColorFilter(effect.TintColor.ToAndroid(), PorterDuff.Mode.SrcIn);
                image.SetColorFilter(filter);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);

                System.Diagnostics.Debug.WriteLine(
                    $"An error occurred when setting the {typeof(TintImageEffect)} effect: {ex.Message}\n{ex.StackTrace}");
            }
        }

        protected override void OnDetached() { }
    }
}