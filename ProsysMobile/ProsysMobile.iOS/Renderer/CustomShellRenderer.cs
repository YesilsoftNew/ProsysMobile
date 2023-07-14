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

        public void SetAppearance(UITabBarController controller, ShellAppearance appearance)
        {
            UITabBar myTabBar = controller.TabBar;

            if (myTabBar.Items != null)
            {
                foreach (UITabBarItem item in myTabBar.Items)
                {
                    item.Title = null;
                    item.ImageInsets = new UIEdgeInsets(10, 0, 0, 0);
                }
                //The same logic if you have itemThree, itemFour....
            }
        }
        public void UpdateLayout(UITabBarController controller)
        {

        }
    }
}
