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
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Resources.Language;
using ProsysMobile.Services.API.UserDevices;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.System
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly ICheckTimeService _checkTimeService;
        private readonly ISaveUserDevicesService _saveUserDevicesService;
        private readonly ISignInService _signInService;
        private readonly IDefaultSettingsSQLiteService _defaultSettingsSqLiteService;

        public LoginPageViewModel(IDefaultSettingsSQLiteService defaultSettingsSqLiteService, ISignInService signInService, ISaveUserDevicesService saveUserDevicesService, ICheckTimeService checkTimeService)
        {
            _defaultSettingsSqLiteService = defaultSettingsSqLiteService;
            _signInService = signInService;
            _saveUserDevicesService = saveUserDevicesService;
            _checkTimeService = checkTimeService;
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (Debugger.IsAttached)
            {
                Email = "test@test.com";
                Password = "test123";
            }

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
                GetUserAuthAsync();
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
                    DialogService.WarningToastMessage(Resource.PleaseCheckYourInternetConnection);
                    
                    DoubleTapping.ResumeTap();

                    isError = true;
                }

                if (string.IsNullOrWhiteSpace(Email))
                {
                    DialogService.WarningToastMessage(Resource.PleaseWriteYourEMailAddress);

                    DoubleTapping.ResumeTap();

                    isError = true;
                }

                if (string.IsNullOrWhiteSpace(Password))
                {
                    DialogService.WarningToastMessage(Resource.PleaseWriteYourPassword);
                   
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

                    var result = await _signInService.SignIn (
                        signIn,
                        enPriorityType.UserInitiated
                    );

                    if (result.ResponseData != null && result.IsSuccess)
                    {
                        var jwt = result.ResponseData.SignIn.Token;
                        var handler = new JwtSecurityTokenHandler();
                        var token = handler.ReadJwtToken(jwt);
                        var expiredDateString = token.Claims.First(claim => claim.Type == "ExpiredDateTime").Value.ToString();

                        GlobalSetting.Instance.JWTToken = result.ResponseData.SignIn.Token;
                        GlobalSetting.Instance.JWTTokenExpireDate = DateTime.ParseExact(expiredDateString, "M/d/yyyy h:m:ss tt", CultureInfo.InvariantCulture);

                        var checkTimeData = await _checkTimeService.CheckTime(
                            userId: result.ResponseData.UserMobile.ID,
                            loginDate: DateTime.Now,
                            priorityType: enPriorityType.UserInitiated
                        );
                        
                        if (checkTimeData?.ResponseData == null || !checkTimeData.IsSuccess)
                        {
                            DialogService.WarningToastMessage(Resource.AnErrorHasOccurred);
                            return;
                        }

                        var notNullCheckTime = checkTimeData.ResponseData;
                        
                        if (result.ResponseData.UserMobile != null)
                        {
                            Database.SQLConnection.Insert(result.ResponseData.UserMobile, "OR REPLACE");
                            GlobalSetting.Instance.User = result.ResponseData.UserMobile;
                        }

                        if (notNullCheckTime.IsContinue)
                        {
                            await NavigationService.SetMainPageAsync<AppShellViewModel>();
                        }
                        else
                        {
                            var navigationModel = new NavigationModel<MaintenancePageViewParamModel>
                            {
                                Model = new MaintenancePageViewParamModel
                                {
                                    CheckTimeResponseModel = notNullCheckTime
                                }
                            };
                            
                            await NavigationService.SetMainPageAsync<MaintenancePageViewModel>(true, navigationModel);
                        }

                        var tokenSettings = new List<DefaultSettings>
                        {
                            new DefaultSettings{Key="UserToken",Value=GlobalSetting.Instance.JWTToken},
                            new DefaultSettings{Key="UserTokenExpiredDate",Value=TOOLS.ToString(GlobalSetting.Instance.JWTTokenExpireDate)},
                            new DefaultSettings{Key="UserId",Value=GlobalSetting.Instance.User.ID.ToString()}
                        };

                        _defaultSettingsSqLiteService.SaveAll(tokenSettings);
                        
                        _saveUserDevicesService.SaveUserDevices(TOOLS.GetUserDevices(GlobalSetting.Instance?.User?.ID ?? -1), enPriorityType.UserInitiated);
                    }
                    else
                    {
                        DialogService.WarningToastMessage(Resource.YourEmailOrPasswordIsIncorrect);
                    }    
                }
                
            }
            catch (Exception ex)
            {
                DialogService.WarningToastMessage(Resource.AnErrorHasOccurred);

                ProsysLogger.Instance.CrashLog(ex);
            }
            
            IsBusy = false;
            DoubleTapping.ResumeTap();
        }
    }
}