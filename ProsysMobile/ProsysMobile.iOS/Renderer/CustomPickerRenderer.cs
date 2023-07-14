using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using ProsysMobile.iOS.Renderer;
using ProsysMobile.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace ProsysMobile.iOS.Renderer
{
    public class CustomPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            this.Control.LeftView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
            this.Control.RightView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
            this.Control.LeftViewMode = UITextFieldViewMode.Always;
            this.Control.RightViewMode = UITextFieldViewMode.Always;

            this.Control.BorderStyle = UITextBorderStyle.None;
            this.Element.HeightRequest = 44;
        } 
    }
}