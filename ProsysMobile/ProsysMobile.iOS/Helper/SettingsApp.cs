using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using WiseMobile.Helper;
using WiseMobile.iOS.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(SettingsApp))]
namespace WiseMobile.iOS.Helper
{
    public class SettingsApp : ISettings
    {
        public void OpenSettings()
        {
            UIApplication.SharedApplication.OpenUrl(new NSUrl(UIApplication.OpenSettingsUrlString));
        }
    }
}