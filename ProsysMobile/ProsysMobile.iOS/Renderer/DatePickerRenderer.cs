using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using WiseMobile.iOS.Renderer;
using WiseMobile.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(WiseMobile.iOS.Renderer.DatePickerRenderer))]
namespace WiseMobile.iOS.Renderer
{
    public class DatePickerRenderer : Xamarin.Forms.Platform.iOS.DatePickerRenderer
    { 
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                this.Control.LeftView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
                this.Control.RightView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
                this.Control.LeftViewMode = UITextFieldViewMode.Always;
                this.Control.RightViewMode = UITextFieldViewMode.Always;

                this.Control.BorderStyle = UITextBorderStyle.None;
                this.Element.HeightRequest = 44;
            }
        }
    }
}