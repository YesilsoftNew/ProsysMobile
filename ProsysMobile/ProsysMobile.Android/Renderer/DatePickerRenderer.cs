using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using WiseMobile.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android; 

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(WiseMobile.Droid.Renderer.DatePickerRenderer))]
namespace WiseMobile.Droid.Renderer
{
    [Obsolete]
    public class DatePickerRenderer : Xamarin.Forms.Platform.Android.DatePickerRenderer
    { 
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackgroundDrawable(gd);
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);

                Control.SetPadding(0, 0, 0, 0);
            }
        }
    }
}