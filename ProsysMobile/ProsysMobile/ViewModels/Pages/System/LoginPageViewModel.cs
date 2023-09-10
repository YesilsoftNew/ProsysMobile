using ProsysMobile.Helper;
using ProsysMobile.Helper.SQLite;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.API.UserMobile;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.FirebasePushNotification;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.API.UserDevices;
using ProsysMobile.ViewModels.Pages.Order;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.System
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly ISaveUserDevicesService _saveUserDevicesService;
        private ISignInService _signInService;
        private IDefaultSettingsSQLiteService _defaultSettingsSqLiteService;

        public LoginPageViewModel(IDefaultSettingsSQLiteService defaultSettingsSqLiteService, ISignInService signInService, ISaveUserDevicesService saveUserDevicesService)
        {
            _defaultSettingsSqLiteService = defaultSettingsSqLiteService;
            _signInService = signInService;
            _saveUserDevicesService = saveUserDevicesService;
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (Debugger.IsAttached)
            {
                Email = "test@test.com";
                Password = "Test.1193";
            }
            
            var result = _saveUserDevicesService.SaveUserDevices(new UserDevices
            {
                UserId = 2,
                LastLoginUserId = 2,
                LastLoginDateTime = DateTime.Now,
                Manufacturer = DeviceInfo.Manufacturer,
                Model = DeviceInfo.Model,
                Name = DeviceInfo.Name,
                Version = DeviceInfo.VersionString,
                Platform = DeviceInfo.Platform.ToString(),
                AppVersion = VersionTracking.CurrentVersion,
                PushToken = CrossFirebasePushNotification.Current?.Token ?? "",
                Timezone = "null",
                RecordDate = DateTime.Now
            }, enPriorityType.UserInitiated);

            return base.InitializeAsync(navigationData);
        }

        #region Propertys
        private string _email;
        public string Email { get => _email; set { _email = value; PropertyChanged(() => Email); } }

        private string _password;
        public string Password { get => _password; set { _password = value; PropertyChanged(() => Password); } }
        #endregion

        #region Commands
        public ICommand LoginClickCommand => new Command(async () =>
        {
            try
            {
                await GetUserAuthAsync();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });

        public ICommand RegisterClickCommand => new Command(async () =>
        {
            try
            {
                NavigationService.NavigateToBackdropAsync<CreateAccountPageViewModel>();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });
        #endregion

        private async Task GetUserAuthAsync()
        {
            try
            {
                IsBusy = true;
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                var isError = false;
                
                if (!GlobalSetting.Instance.IsConnectedInternet)
                {
                    DialogService.WarningToastMessage("Lütfen internet bağlantınızı kontrol ediniz!");
                    
                    DoubleTapping.ResumeTap();

                    isError = true;
                }

                if (string.IsNullOrWhiteSpace(Email))
                {
                    DialogService.WarningToastMessage("Lütfen email adresinizi yazınız!");

                    DoubleTapping.ResumeTap();

                    isError = true;
                }

                if (string.IsNullOrWhiteSpace(Password))
                {
                    DialogService.WarningToastMessage("Lütfen şifrenizi yazınız!");
                   
                    DoubleTapping.ResumeTap();

                    isError = true;
                }

                if (!isError)
                {
                    var signIn = new SignIn();

                    signIn.Email = Email;
                    signIn.Password = Password;
                    signIn.DeviceGuid = Guid.NewGuid();
                    signIn.Token = string.Empty;

                    var result = await _signInService.SignIn(
                        signIn,
                        Models.CommonModels.Enums.enPriorityType.UserInitiated
                    );

                    if (result.ResponseData != null && result.IsSuccess)
                    {
                        var jwt = result.ResponseData.SignIn.Token;
                        var handler = new JwtSecurityTokenHandler();
                        var token = handler.ReadJwtToken(jwt);
                        var expiredDateString = token.Claims.First(claim => claim.Type == "ExpiredDateTime").Value.ToString();
                        
                        GlobalSetting.Instance.JWTToken = result.ResponseData.SignIn.Token;
                        GlobalSetting.Instance.JWTTokenExpireDate = DateTime.ParseExact(expiredDateString, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                        if (result.ResponseData.UserMobile != null)
                        {
                            Database.SQLConnection.Insert(result.ResponseData.UserMobile, "OR REPLACE");
                            GlobalSetting.Instance.User = result.ResponseData.UserMobile;
                        }

                        await NavigationService.SetMainPageAsync<AppShellViewModel>();

                        var tokenSettings = new List<DefaultSettings>()
                        {
                            new DefaultSettings{Key="UserToken",Value=GlobalSetting.Instance.JWTToken},
                            new DefaultSettings{Key="UserTokenExpiredDate",Value=TOOLS.ToString(GlobalSetting.Instance.JWTTokenExpireDate)},
                            new DefaultSettings{Key="UserId",Value=GlobalSetting.Instance.User.ID.ToString()}
                        };

                        _defaultSettingsSqLiteService.SaveAll(tokenSettings);
                    }
                    else
                    {
                        DialogService.WarningToastMessage("Email veya şifreniz hatalı!");
                    }    
                }
                
            }
            catch (Exception ex)
            {
                DialogService.WarningToastMessage("Bir hata oluştu!");

                ProsysLogger.Instance.CrashLog(ex);
            }
            
            IsBusy = false;
            DoubleTapping.ResumeTap();
        }
    }
}