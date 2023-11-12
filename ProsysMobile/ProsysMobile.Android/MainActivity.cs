using System.Globalization;
using System.Threading;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Acr.UserDialogs;
using Plugin.FirebasePushNotification;
using Plugin.LocalNotification;
using Plugin.LocalNotification.Platform.Droid;
using Plugin.LocalNotifications;
using Color = Android.Graphics.Color;

namespace ProsysMobile.Droid
{
    [Activity(Label = "Bidi Früchte", Icon = "@drawable/Logo",RoundIcon = "@drawable/Logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            var ci = new CultureInfo("tr-TR")
            {
                DateTimeFormat =
                {
                    DateSeparator = "/",
                    ShortDatePattern = "dd/MM/yyyy"
                },
                NumberFormat =
                {
                    NumberDecimalSeparator = ".",
                    NumberGroupSeparator = ","
                } 
            }; 

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            
            base.OnCreate(savedInstanceState);

            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.Logo;
            
            UserDialogs.Init(this);
            Rg.Plugins.Popup.Popup.Init(this);
            
            RequestedOrientation = ScreenOrientation.Portrait;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window?.SetStatusBarColor(Color.LightGray);
            }
            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            #region Notification 

            FirebasePushNotificationManager.ProcessIntent(this, Intent);

            #endregion
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}