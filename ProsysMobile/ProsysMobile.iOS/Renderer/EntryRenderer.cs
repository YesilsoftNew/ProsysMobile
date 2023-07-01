using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using WiseMobile.Helper;
using WiseMobile.iOS.Renderer;
using WiseMobile.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(WiseMobile.iOS.Renderer.EntryRenderer))]
namespace WiseMobile.iOS.Renderer
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
                    WiseLogger.Instance.CrashLog(ex);

                    return;
                }
                
            }
        }
    }
}  