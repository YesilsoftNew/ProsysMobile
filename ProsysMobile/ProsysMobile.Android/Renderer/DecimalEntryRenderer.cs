using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using ProsysMobile.Droid.Renderer;
using ProsysMobile.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDecimalEntry), typeof(ProsysMobile.Droid.Renderer.DecimalEntryRenderer))]
namespace ProsysMobile.Droid.Renderer
{
    [Obsolete]
    public class DecimalEntryRenderer : EntryRenderer
    {
        private EditText _native = null;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                gd.SetCornerRadius(4);
                gd.SetStroke(2, global::Android.Graphics.Color.Transparent); 
                this.Control.SetBackgroundDrawable(gd);

                this.Control.KeyListener = Android.Text.Method.DigitsKeyListener.GetInstance(string.Format("1234567890{0}", System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                this.Control.InputType = Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagDecimal;
            }
        }
    }
}