using System;
using CoreAnimation;
using CoreGraphics;
using ProsysMobile.Helper;
using ProsysMobile.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRendererAttribute(typeof(CustomShell), typeof(ProsysMobile.iOS.Renderer.CustomShellRenderer))]

namespace ProsysMobile.iOS.Renderer
{
    public class CustomShellRenderer : ShellRenderer
    {
        protected override IShellSectionRenderer CreateShellSectionRenderer(ShellSection shellSection)
        {
            var renderer = base.CreateShellSectionRenderer(shellSection);
            if (renderer != null)
            {

            }
            return renderer;
        }

        protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
        {
            return new CustomTabbarAppearance();
        }
    }

    public class CustomTabbarAppearance : IShellTabBarAppearanceTracker
    {
        public void Dispose()
        {

        }

        public void ResetAppearance(UITabBarController controller)
        {

        }

        private bool hasSetAppearance = false;
        
        public void SetAppearance(UITabBarController controller, ShellAppearance appearance)
        {
           
        }
        public void UpdateLayout(UITabBarController controller)
        {

        }
        
        
    }
    
    public static class ColorExtensions
    {
        public static UIColor FromHexString(string hex)
        {
            try
            {
                hex = hex.TrimStart('#');

                if (hex.Length == 6)
                {
                    int red = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                    int green = int.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
                    int blue = int.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

                    return UIColor.FromRGB(red, green, blue);
                }

                throw new ArgumentException("Invalid hex color code", nameof(hex));
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                
                throw new ArgumentException("Invalid hex color code", nameof(hex));
            }
        }
    }

}
