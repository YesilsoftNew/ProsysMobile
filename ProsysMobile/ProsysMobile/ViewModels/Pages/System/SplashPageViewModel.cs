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
using ProsysMobile.Services.API.UserMobile;

namespace ProsysMobile.ViewModels.Pages.System
{
    public class SplashPageViewModel : ViewModelBase
    {
        private IDefaultSettingsSQLiteService _defaultSettingsSQLiteService;
        private IUserMobileSQLiteService _userSQLiteService;
        private ISignInService _signInService;

        public SplashPageViewModel(IDefaultSettingsSQLiteService defaultSettingsSQLiteService, IUserMobileSQLiteService userSqLiteService, ISignInService signInService)
        {
            _defaultSettingsSQLiteService = defaultSettingsSQLiteService;
            _userSQLiteService = userSqLiteService;
            _signInService = signInService;
        }

        public override Task InitializeAsync(object navigationData)
        {
            GoToLoginPage();

            return base.InitializeAsync(navigationData);
        }
        async Task GoToLoginPage()
        {
            try
            {
                USERMOBILE user;
                
                await Task.Delay(3000);

                DefaultSettings userDefaultSetting = _defaultSettingsSQLiteService.getSettings("UserId");

                //var aasddfs = _defaultSettingsSQLiteService.getSettingsAll();

                if (!(userDefaultSetting is null))
                {
                    user = _userSQLiteService.GetUser(TOOLS.ToLong(userDefaultSetting.Value));
                    
                    if (user != null)
                    {
                        GlobalSetting.Instance.User = user;
                    
                        DefaultSettings userTokenDefaultSetting = _defaultSettingsSQLiteService.getSettings("UserToken");
                        DefaultSettings userTokenExpiredDateDefaultSetting = _defaultSettingsSQLiteService.getSettings("UserTokenExpiredDate");
                    
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

                            signIn.Email = user.EMAIL;
                            signIn.Password = user.PASSWORD;
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

                                _defaultSettingsSQLiteService.Save(userTokenDefaultSetting);

                                userTokenExpiredDateDefaultSetting.Key = "UserTokenExpiredDate";
                                userTokenExpiredDateDefaultSetting.Value = TOOLS.ToString(GlobalSetting.Instance.JWTTokenExpireDate);

                                _defaultSettingsSQLiteService.Save(userTokenExpiredDateDefaultSetting);
                            }
                            #endregion
                        }
                    
                        await NavigationService.SetMainPageAsync<AppShellViewModel>();
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