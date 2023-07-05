using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProsysMobile.Droid.Helper;
using ProsysMobile.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsApp))]
namespace ProsysMobile.Droid.Helper
{
    public class SettingsApp : ISettings
    {
        public void OpenSettings()
        {
            Intent intent = new Android.Content.Intent(Android.Provider.Settings.ActionDateSettings);
            intent.AddFlags(ActivityFlags.NewTask);
            Android.App.Application.Context.StartActivity(intent);
        }
    }
}