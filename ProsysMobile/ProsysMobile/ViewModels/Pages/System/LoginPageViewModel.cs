﻿using ProsysMobile.Helper;
using ProsysMobile.Helper.SQLite;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.API.UserMobile;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.ViewModels.Pages.Order;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.System
{
    public class LoginPageViewModel : ViewModelBase
    {
        private ISignInService _signInService;
        private IDefaultSettingsSQLiteService _defaultSettingsSqLiteService;

        public LoginPageViewModel(IDefaultSettingsSQLiteService defaultSettingsSqLiteService, ISignInService signInService)
        {
            _defaultSettingsSqLiteService = defaultSettingsSqLiteService;
            _signInService = signInService;
        }

        public override Task InitializeAsync(object navigationData)
        {
            if (Debugger.IsAttached)
            {
                Email = "test@test.com";
                Password = "Test.1193";
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
                var navigationModel = new NavigationModel<int>
                {
                    Model = 1
                };
                
                NavigationService.NavigateToBackdropAsync<OrderDetailPageViewModel>(navigationModel);
                
                return;
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
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;


                if (!GlobalSetting.Instance.IsConnectedInternet)
                {
                    DialogService.WarningToastMessage("Lütfen internet bağlantınızı kontrol ediniz! QQQ");
                    
                    DoubleTapping.ResumeTap();

                    return;
                }

                if (string.IsNullOrWhiteSpace(Email))
                {
                    DialogService.WarningToastMessage("Lütfen email adresinizi yazınız! QQQ");

                    DoubleTapping.ResumeTap();

                    return;
                }

                if (string.IsNullOrWhiteSpace(Password))
                {
                    DialogService.WarningToastMessage("Lütfen şifrenizi yazınız! QQQ");
                   
                    DoubleTapping.ResumeTap();

                    return;
                }

                SignIn signIn = new SignIn();

                signIn.Email = Email;
                signIn.Password = Password;
                signIn.DeviceGuid = Guid.NewGuid();
                signIn.Token = string.Empty;

                var result = await _signInService.SignIn(signIn, Models.CommonModels.Enums.enPriorityType.UserInitiated);

                if (result.ResponseData != null && result.IsSuccess)
                {
                    var jwt = result.ResponseData.SignIn.Token;
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(jwt);

                    GlobalSetting.Instance.JWTToken = result.ResponseData.SignIn.Token;
                    GlobalSetting.Instance.JWTTokenExpireDate = Convert.ToDateTime(token.Claims.First(claim => claim.Type == "ExpiredDateTime").Value);

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
                    DialogService.WarningToastMessage("Email veya şifreniz hatalı! QQQ");
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            DoubleTapping.ResumeTap();
        }
    }
}