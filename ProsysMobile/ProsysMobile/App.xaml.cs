using ProsysMobile.Helper;
using ProsysMobile.Helper.SQLite;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Base;
using ProsysMobile.ViewModels.Pages.System;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.IdentityModel.Tokens;
using Plugin.FirebasePushNotification;
using ProsysMobile.Endpoints.UserDevices;
using ProsysMobile.Handler;
using ProsysMobile.Services.API.UserDevices;
using Xamarin.Essentials;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;
using Plugin.LocalNotification;
using Plugin.Multilingual;
using ProsysMobile.Resources.Language;
using ProsysMobile.Services.Dialog;

[assembly: ExportFont("poppins_black.ttf", Alias = "poppins_black")]
[assembly: ExportFont("poppins_black_italic.ttf", Alias = "poppins_black_italic")]
[assembly: ExportFont("poppins_bold.ttf", Alias = "poppins_bold")]
[assembly: ExportFont("poppins_bold_italic.ttf", Alias = "poppins_bold_italic")]
[assembly: ExportFont("poppins_extra_bold.ttf", Alias = "poppins_extra_bold")]
[assembly: ExportFont("poppins_extra_bold_italic.ttf", Alias = "poppins_extra_bold_italic")]
[assembly: ExportFont("poppins_extra_light.ttf", Alias = "poppins_extra_light")]
[assembly: ExportFont("poppins_extra_light_italic.ttf", Alias = "poppins_extra_light_italic")]
[assembly: ExportFont("poppins_italic.ttf", Alias = "poppins_italic")]
[assembly: ExportFont("poppins_light.ttf", Alias = "poppins_light")]
[assembly: ExportFont("poppins_light_italic.ttf", Alias = "poppins_light_italic")]
[assembly: ExportFont("poppins_medium.ttf", Alias = "poppins_medium")]
[assembly: ExportFont("poppins_medium_italic.ttf", Alias = "poppins_medium_italic")]
[assembly: ExportFont("poppins_regular.ttf", Alias = "poppins_regular")]
[assembly: ExportFont("poppins_semi_bold.ttf", Alias = "poppins_semi_bold")]
[assembly: ExportFont("poppins_semi_bold_italic.ttf", Alias = "poppins_semi_bold_italic")]
[assembly: ExportFont("poppins_thin.ttf", Alias = "poppins_thin")]
[assembly: ExportFont("poppins_thin_italic.ttf", Alias = "poppins_thin_italic")]

namespace ProsysMobile
{
    public partial class App
    { 
        public static string Prosys_Api = "http://yas.yesilsoft.net";   // dev test
        public App()
        {
            InitializeComponent();

            GlobalSetting.Instance.PagesPath = "Pages";
            GlobalSetting.Instance.ViewsPath = "Views";
            GlobalSetting.Instance.ViewModelPath = "ViewModels";
            GlobalSetting.Instance.BaseEndpoint = Prosys_Api;

            ViewModelLocator.Init<SplashPageViewModel>();

            GlobalSetting.Instance.IsConnectedInternet = Connectivity.NetworkAccess == NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        
        private static void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs e)
        {
            GlobalSetting.Instance.IsConnectedInternet = e.NetworkAccess == NetworkAccess.Internet;
        }

        protected override void OnStart()
        {
            #region Appcenter
            AppCenter.Start("android=3124f4df-dc29-422f-b729-37d848c9eb5b;" +
                            "uwp={Your UWP App secret here};" +
                            "ios=16447944-c577-4d52-a9c8-079fd36f482c;",
                            typeof(Analytics), typeof(Crashes));
            #endregion

            #region SQLite
            SQLiteDBSync syn = new SQLiteDBSync();
            syn.SQLiteCreateTable();

            try
            {
                if (!Directory.Exists(GlobalSetting.Instance.ImageFolder))
                    Directory.CreateDirectory(GlobalSetting.Instance.ImageFolder);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            #endregion

            #region Default Settings
            try
            {
                var defaultSettingsSqLiteService = new DefaultSettingsSQLiteService();

                #region Language

                var defaultSettingsLanguage = defaultSettingsSqLiteService.getSettings(DefaultSettingsKey.Language);

                if (defaultSettingsLanguage != null)
                    GlobalSetting.Instance.AppLanguage = defaultSettingsLanguage.Value;
                else
                {
                    var deviceCultureInfo = CrossMultilingual.Current.DeviceCultureInfo.Name;
                
                    if (deviceCultureInfo == "tr-TR" || deviceCultureInfo == "en-EN" || deviceCultureInfo == "de-DE")
                        GlobalSetting.Instance.AppLanguage = deviceCultureInfo;
                    else
                        GlobalSetting.Instance.AppLanguage = "de-DE";
                }

                if (defaultSettingsLanguage is null)
                {
                    defaultSettingsLanguage = new DefaultSettings
                    {
                        Key = DefaultSettingsKey.Language,
                        Value = GlobalSetting.Instance.AppLanguage
                    };

                    defaultSettingsSqLiteService.Save(defaultSettingsLanguage);
                }
                
                TOOLS.SetCulture();

                #endregion

                #region DeviceGuid

                var defaultSettingsDeviceGuid = defaultSettingsSqLiteService.getSettings(DefaultSettingsKey.DeviceGuid);

                if (defaultSettingsDeviceGuid != null)
                    GlobalSetting.Instance.DeviceGuid = defaultSettingsDeviceGuid.Value;

                if (defaultSettingsDeviceGuid is null)
                {
                    var newDeviceGuid = Guid.NewGuid();
                    
                    defaultSettingsDeviceGuid = new DefaultSettings
                    {
                        Key = DefaultSettingsKey.DeviceGuid,
                        Value = newDeviceGuid.ToString()
                    };

                    defaultSettingsSqLiteService.Save(defaultSettingsDeviceGuid);
                    
                    GlobalSetting.Instance.DeviceGuid = defaultSettingsDeviceGuid.Value;
                }
                
                #endregion

            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            #endregion

            #region Notification

            CrossFirebasePushNotification.Current.Subscribe("general");
            CrossFirebasePushNotification.Current.OnTokenRefresh +=  (source, args) =>
            {
                GlobalSetting.Instance.FirebaseNotificationToken = args?.Token ?? "";
            };
            
            var random = new Random();

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                try
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        if (p.Data.ContainsKey("body") && p.Data.ContainsKey("title"))
                        {
                            var notification = new NotificationRequest
                            {
                                BadgeNumber = 1,
                                Description = p.Data["body"].ToString(),
                                Title = p.Data["title"].ToString(),
                                NotificationId = random.Next(1, int.MaxValue)
                            };

                            NotificationCenter.Current.Show(notification);
                        }
                    }
                    else
                    {
                        var notification = new NotificationRequest
                        {
                            BadgeNumber = 1,
                            Description = "",
                            Title = "",
                            NotificationId = random.Next(1, int.MaxValue)
                        };

                        NotificationCenter.Current.Show(notification);   
                    }
                }
                catch (Exception)
                {
                    DoubleTapping.ResumeTap();
                }

            };

            #endregion
        }

        protected override void OnSleep()
        {
            Console.WriteLine("test");
        }

        protected override void OnResume()
        {
        }
    }
}
