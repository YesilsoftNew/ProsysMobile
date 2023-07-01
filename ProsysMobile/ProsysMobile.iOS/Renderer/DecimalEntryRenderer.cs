using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using WiseMobile.iOS.Renderer;
using WiseMobile.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomDecimalEntry), typeof(WiseMobile.iOS.Renderer.DecimalEntryRenderer))]
namespace WiseMobile.iOS.Renderer
{
    public class DecimalEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.CornerRadius = 4;
            }
        }
    }
}