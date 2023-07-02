using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using ProsysMobile.Droid.Renderer;
using ProsysMobile.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(ProsysMobile.Droid.Renderer.EntryRenderer))]
namespace ProsysMobile.Droid.Renderer
{
    [Obsolete]
    public class EntryRenderer : Xamarin.Forms.Platform.Android.EntryRenderer 
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {  
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                gd.SetCornerRadius(10);
                gd.SetStroke(2, global::Android.Graphics.Color.Transparent);
                gd.SetPadding(0,0,0,0);
                this.Control.SetBackgroundDrawable(gd);
                 
                //this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
            }
        }
    }
}