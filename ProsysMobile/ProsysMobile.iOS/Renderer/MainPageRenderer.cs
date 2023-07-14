using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using ProsysMobile.Helper;
using ProsysMobile.iOS.Renderer;
using ProsysMobile.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MainTabbedPage), typeof(MainPageRenderer))]
namespace ProsysMobile.iOS.Renderer
{
   public  class MainPageRenderer : TabbedRenderer
    {
        private MainTabbedPage _page;
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
                _page = (MainTabbedPage)e.NewElement;
            else
                _page = (MainTabbedPage)e.OldElement;

            try
            {
                var tabbarController = (UITabBarController)this.ViewController;

                if (null != tabbarController)
                    tabbarController.ViewControllerSelected += OnTabbarControllerItemSelected;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);

                Console.WriteLine(ex);
            }
        }

        private async void OnTabbarControllerItemSelected(object sender, UITabBarSelectionEventArgs eventArgs)
        {
            if (_page?.CurrentPage?.Navigation != null && _page.CurrentPage.Navigation.NavigationStack.Count > 0)
                await _page.CurrentPage.Navigation.PopToRootAsync();
        }
    }
}