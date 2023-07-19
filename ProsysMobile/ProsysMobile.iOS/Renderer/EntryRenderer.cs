using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using ProsysMobile.Helper;
using ProsysMobile.iOS.Renderer;
using ProsysMobile.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(ProsysMobile.iOS.Renderer.EntryRenderer))]
namespace ProsysMobile.iOS.Renderer
{
    public class EntryRenderer : Xamarin.Forms.Platform.iOS.EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                try
                {
                    this.Control.LeftView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
                    this.Control.RightView = new UIView(new CGRect(0, 0, 8, this.Control.Frame.Height));
                    this.Control.LeftViewMode = UITextFieldViewMode.Always;
                    this.Control.RightViewMode = UITextFieldViewMode.Always;

                    this.Control.BorderStyle = UITextBorderStyle.None;
                    this.Element.HeightRequest = 44;
                }
                catch (Exception ex)
                {
                    ProsysLogger.Instance.CrashLog(ex);

                    return;
                }
                
            }
        }
    }
}