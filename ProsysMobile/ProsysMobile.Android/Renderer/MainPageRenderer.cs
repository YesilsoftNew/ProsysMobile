using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using ProsysMobile.Droid.Renderer;
using ProsysMobile.Renderer;
using Xamarin.Forms.Platform.Android;
using Google.Android.Material.Tabs;

[assembly: ExportRenderer(typeof(MainTabbedPage), typeof(MainPageRenderer))]
namespace ProsysMobile.Droid.Renderer
{
    [Obsolete]
    public class MainPageRenderer : TabbedPageRenderer, TabLayout.IOnTabSelectedListener
    {
        private MainTabbedPage _page;
        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
                _page = (MainTabbedPage)e.NewElement;
            else
                _page = (MainTabbedPage)e.OldElement;

        }

        async void TabLayout.IOnTabSelectedListener.OnTabReselected(TabLayout.Tab tab)
        {
            await _page.CurrentPage.Navigation.PopToRootAsync();
        }
    }
}