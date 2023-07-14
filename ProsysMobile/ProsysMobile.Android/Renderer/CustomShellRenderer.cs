using Android.Content;
using AndroidX.AppCompat.Widget;
using Google.Android.Material.BottomNavigation;
using ProsysMobile.Droid.Renderer;
using ProsysMobile.Renderer;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRendererAttribute(typeof(CustomShell), typeof(CustomShellRenderer))]
namespace ProsysMobile.Droid.Renderer
{
    public class CustomShellRenderer : ShellRenderer
    {
        public CustomShellRenderer(Context context) : base(context)
        {

        }

        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new MarginedTabBarAppearance();
        }
    }

    public class ToolbarAppearance : IShellToolbarAppearanceTracker
    {

        public void Dispose()
        {

        }

        public void ResetAppearance(Toolbar toolbar, IShellToolbarTracker toolbarTracker)
        {
            throw new NotImplementedException();
        }

        public void SetAppearance(Toolbar toolbar, IShellToolbarTracker toolbarTracker, ShellAppearance appearance)
        {
            throw new NotImplementedException();
        }
    }

    public class MarginedTabBarAppearance : IShellBottomNavViewAppearanceTracker
    {
        public void Dispose()
        {
        }

        public void ResetAppearance(BottomNavigationView bottomView)
        {
        }

        public void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            bottomView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityUnlabeled;
        }
    }

}