using Plugin.Multilingual;
using ProsysMobile.Helper;
using ProsysMobile.Helper.SQLite;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Base;
using ProsysMobile.ViewModels.Pages.System;
using System;
using System.IO;
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

            GlobalSetting.Instance.IsConnectedInternet = Connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            
            CrossFirebasePushNotification.Current.OnTokenRefresh +=  (source, args) =>
            {
                GlobalSetting.Instance.FirebaseNotificationToken = args?.Token ?? "";
            };
        }
        
        private void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs e)
        {
            GlobalSetting.Instance.IsConnectedInternet = e.NetworkAccess == NetworkAccess.Internet ? true : false;
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

            #region Language
            try
            {
                DefaultSettingsSQLiteService _defaultSettingsSQLiteService = new DefaultSettingsSQLiteService();
                DefaultSettings defaultSettings = _defaultSettingsSQLiteService.getSettings("Language");

                string DeviceCultureInfo = CrossMultilingual.Current.DeviceCultureInfo.Name;
                DeviceCultureInfo = DeviceCultureInfo.Substring(0, DeviceCultureInfo.IndexOf("-"));

                if (DeviceCultureInfo == "tr" || DeviceCultureInfo == "en" || DeviceCultureInfo == "de")
                    GlobalSetting.Instance.AppLanguage = DeviceCultureInfo;
                else
                    GlobalSetting.Instance.AppLanguage = "de";

                if (defaultSettings != null)
                    GlobalSetting.Instance.AppLanguage = defaultSettings.Value;

                if (defaultSettings is null)
                {
                    defaultSettings = new DefaultSettings();
                    defaultSettings.Key = "Language";
                    defaultSettings.Value = GlobalSetting.Instance.AppLanguage;

                    _defaultSettingsSQLiteService.Save(defaultSettings);
                }

                TOOLS.setCulture();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            #endregion

            Random random = new Random();

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

                            LocalNotificationCenter.Current.Show(notification);
                        }
                    }
                    else
                    {
                        if (p.Data.ContainsKey("aps.alert.title") && p.Data.ContainsKey("aps.alert.body"))
                        {
                            var notification = new NotificationRequest
                            {
                                BadgeNumber = 1,
                                Description = p.Data["aps.alert.body"].ToString(),
                                Title = p.Data["aps.alert.title"].ToString(),
                                NotificationId = random.Next(1, int.MaxValue)
                            };

                            LocalNotificationCenter.Current.Show(notification);
                        }
                    }
                }
                catch (Exception)
                {
                    DoubleTapping.ResumeTap();
                }

            };
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
