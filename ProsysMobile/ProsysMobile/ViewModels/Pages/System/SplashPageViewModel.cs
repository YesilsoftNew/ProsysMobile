using ProsysMobile.Helper;
using ProsysMobile.Helper.ApiClient;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Base;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Plugin.FirebasePushNotification;
using Plugin.LocalNotification;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Services.API.UserDevices;
using ProsysMobile.Services.API.UserMobile;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.System
{
    public class SplashPageViewModel : ViewModelBase
    {
        private readonly ISaveUserDevicesService _saveUserDevicesService;
        private readonly IDefaultSettingsSQLiteService _defaultSettingsSqLiteService;
        private readonly IUserMobileSQLiteService _userSqLiteService;
        private readonly ISignInService _signInService;
        
        private int count = 0;

        public SplashPageViewModel(IDefaultSettingsSQLiteService defaultSettingsSQLiteService, IUserMobileSQLiteService userSqLiteService, ISignInService signInService, ISaveUserDevicesService saveUserDevicesService)
        {
            _defaultSettingsSqLiteService = defaultSettingsSQLiteService;
            _userSqLiteService = userSqLiteService;
            _signInService = signInService;
            _saveUserDevicesService = saveUserDevicesService;
        }

        public override Task InitializeAsync(object navigationData)
        {
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

                            LocalNotificationCenter.Current.Show(notification);
                        }
                    }
                    else
                    {
                        count++;
                    }
                }
                catch (Exception)
                {
                    DoubleTapping.ResumeTap();
                }

            };
            
            GoToLoginPage();

            return base.InitializeAsync(navigationData);
        }
        async Task GoToLoginPage()
        {
            try
            {
                await Task.Delay(3000);

                var userDefaultSetting = _defaultSettingsSqLiteService.getSettings("UserId");
                
                if (!(userDefaultSetting is null))
                {
                    var user = _userSqLiteService.GetUser(TOOLS.ToLong(userDefaultSetting.Value));

                    if (user != null)
                    {
                        GlobalSetting.Instance.User = user;
                    
                        DefaultSettings userTokenDefaultSetting = _defaultSettingsSqLiteService.getSettings("UserToken");
                        DefaultSettings userTokenExpiredDateDefaultSetting = _defaultSettingsSqLiteService.getSettings("UserTokenExpiredDate");
                    
                        if (userTokenDefaultSetting != null)
                        {
                            GlobalSetting.Instance.JWTToken = userTokenDefaultSetting.Value;
                            GlobalSetting.Instance.JWTTokenExpireDate = Convert.ToDateTime(userTokenExpiredDateDefaultSetting.Value);
                        }
                        else
                        {
                            userTokenDefaultSetting = new DefaultSettings();
                            userTokenExpiredDateDefaultSetting = new DefaultSettings();
                        }
                    
                        if (string.IsNullOrWhiteSpace(GlobalSetting.Instance.JWTToken) || DateTime.Now >= GlobalSetting.Instance.JWTTokenExpireDate)
                        {
                            #region JWTToken
                            
                            SignIn signIn = new SignIn();

                            signIn.Email = user.Email;
                            signIn.Password = user.Password;
                            signIn.DeviceGuid = Guid.NewGuid();
                            signIn.Token = string.Empty;
                        
                            var result = await _signInService.SignIn(signIn, Models.CommonModels.Enums.enPriorityType.UserInitiated);
                        
                            if (result.ResponseData != null && result.IsSuccess)
                            {
                                var jwt = result.ResponseData.SignIn.Token;
                                var handler = new JwtSecurityTokenHandler();
                                var token = handler.ReadJwtToken(jwt);
                                var expiredDateString = token.Claims.First(claim => claim.Type == "ExpiredDateTime").Value;
                                
                                GlobalSetting.Instance.JWTToken = result.ResponseData.SignIn.Token;
                                GlobalSetting.Instance.JWTTokenExpireDate = DateTime.ParseExact(expiredDateString, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                                
                                userTokenDefaultSetting.Key = "UserToken";
                                userTokenDefaultSetting.Value = GlobalSetting.Instance.JWTToken;

                                _defaultSettingsSqLiteService.Save(userTokenDefaultSetting);

                                userTokenExpiredDateDefaultSetting.Key = "UserTokenExpiredDate";
                                userTokenExpiredDateDefaultSetting.Value = TOOLS.ToString(GlobalSetting.Instance.JWTTokenExpireDate);

                                _defaultSettingsSqLiteService.Save(userTokenExpiredDateDefaultSetting);
                            }
                            #endregion
                        }

                        await NavigationService.SetMainPageAsync<AppShellViewModel>();
                        
                        _saveUserDevicesService.SaveUserDevices(TOOLS.GetUserDevices(GlobalSetting.Instance?.User?.ID ?? -1), enPriorityType.UserInitiated);
                    }
                    else
                        await NavigationService.SetMainPageAsync<LoginPageViewModel>();
                }
                else
                    await NavigationService.SetMainPageAsync<LoginPageViewModel>();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

    }
}