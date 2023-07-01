using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using WiseMobile.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(WiseMobile.iOS.Renderer.FrameRenderer))]
namespace WiseMobile.iOS.Renderer
{
    public class FrameRenderer : Xamarin.Forms.Platform.iOS.FrameRenderer
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            // Update shadow to match better material design standards of elevation
            Layer.ShadowRadius = 2.0f;
            Layer.ShadowColor = UIColor.Gray.CGColor;
            Layer.ShadowOffset = new CGSize(2, 2);
            Layer.ShadowOpacity = 0.80f;
            Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            Layer.MasksToBounds = false;
        }
    }
}