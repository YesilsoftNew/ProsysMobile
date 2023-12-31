﻿using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Resources.Language;
using ProsysMobile.Services.API.UserDevices;
using ProsysMobile.Services.API.UserMobile;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.System
{
    public class SplashPageViewModel : ViewModelBase
    {
        private readonly ICheckTimeService _checkTimeService;
        private readonly ISaveUserDevicesService _saveUserDevicesService;
        private readonly IDefaultSettingsSQLiteService _defaultSettingsSqLiteService;
        private readonly IUserMobileSQLiteService _userSqLiteService;
        private readonly ISignInService _signInService;

        public SplashPageViewModel(IDefaultSettingsSQLiteService defaultSettingsSqLiteService, IUserMobileSQLiteService userSqLiteService, ISignInService signInService, ISaveUserDevicesService saveUserDevicesService, ICheckTimeService checkTimeService)
        {
            _defaultSettingsSqLiteService = defaultSettingsSqLiteService;
            _userSqLiteService = userSqLiteService;
            _signInService = signInService;
            _saveUserDevicesService = saveUserDevicesService;
            _checkTimeService = checkTimeService;
        }
        
        #region Propertys
        
        private string _versionText;
        public string VersionText { get => _versionText; set { _versionText = value; PropertyChanged(() => VersionText); } }

        #endregion

        public override async Task InitializeAsync(object navigationData)
        {
            VersionText = "v " + Xamarin.Essentials.VersionTracking.CurrentBuild + $" ({Xamarin.Essentials.VersionTracking.CurrentVersion})";
            await GoToLoginPage();
        }

        private async Task GoToLoginPage()
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
                            
                            var dateString = userTokenExpiredDateDefaultSetting.Value;
        
                            var fullParts = dateString.Split(' ', '/');
                            var timeParts = fullParts[3].Split(' ', ':');

                            var day = int.Parse(fullParts[0]);
                            var month = int.Parse(fullParts[1]);
                            var year = int.Parse(fullParts[2]);
                            var hour = int.Parse(timeParts[0]);
                            var minute = int.Parse(timeParts[1]);
                            var second = int.Parse(timeParts[2]);

                            var formatedDate = new DateTime(year, month, day, hour, minute, second);
                            
                            GlobalSetting.Instance.JWTTokenExpireDate = formatedDate;
                        }
                        else
                        {
                            userTokenDefaultSetting = new DefaultSettings();
                            userTokenExpiredDateDefaultSetting = new DefaultSettings();
                        }

                        var dateNow = DateTime.Now;
                    
                        if (string.IsNullOrWhiteSpace(GlobalSetting.Instance.JWTToken) || dateNow >= GlobalSetting.Instance.JWTTokenExpireDate)
                        {
                            #region JWTToken
                            
                            SignIn signIn = new SignIn();

                            signIn.Email = user.Email;
                            signIn.Password = user.Password;
                            signIn.DeviceGuid = Guid.NewGuid();
                            signIn.Token = string.Empty;
                        
                            var result = await _signInService.SignIn(signIn, enPriorityType.UserInitiated);
                        
                            if (result.ResponseData != null && result.IsSuccess)
                            {
                                var jwt = result.ResponseData.SignIn.Token;
                                var handler = new JwtSecurityTokenHandler();
                                var token = handler.ReadJwtToken(jwt);
                                var expiredDateString = token.Claims.First(claim => claim.Type == "ExpiredDateTime").Value;
                                
                                GlobalSetting.Instance.JWTToken = result.ResponseData.SignIn.Token;
                                GlobalSetting.Instance.JWTTokenExpireDate = DateTime.ParseExact(expiredDateString, "M/d/yyyy h:m:ss tt", CultureInfo.InvariantCulture);
                                
                                userTokenDefaultSetting.Key = "UserToken";
                                userTokenDefaultSetting.Value = GlobalSetting.Instance.JWTToken;

                                _defaultSettingsSqLiteService.Save(userTokenDefaultSetting);

                                userTokenExpiredDateDefaultSetting.Key = "UserTokenExpiredDate";
                                userTokenExpiredDateDefaultSetting.Value = TOOLS.ToString(GlobalSetting.Instance.JWTTokenExpireDate);

                                _defaultSettingsSqLiteService.Save(userTokenExpiredDateDefaultSetting);
                            }
                            #endregion
                        }
                        
                        var checkTimeData = await _checkTimeService.CheckTime(
                            userId: GlobalSetting.Instance.User.ID,
                            loginDate: DateTime.Now,
                            priorityType: enPriorityType.UserInitiated
                        );
                        
                        if (checkTimeData?.ResponseData == null || !checkTimeData.IsSuccess)
                        {
                            ProsysLogger.Instance.CrashLog(new Exception("CheckTime method --yasin"), new Dictionary<string, string>
                            {
                                { "Custom_DateTimeNow", dateNow.ToString("dd/MM/yyyy HH:mm:ss") },
                                { "Custom_Token", GlobalSetting.Instance.JWTToken },
                                { "Custom_TimeZoneInfo", TimeZoneInfo.Local.ToString() }
                            });
                            DialogService.WarningToastMessage(Resource.AnErrorHasOccurred);
                            return;
                        }

                        var notNullCheckTime = checkTimeData.ResponseData;
                        
                        if (Debugger.IsAttached || notNullCheckTime.IsContinue)
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