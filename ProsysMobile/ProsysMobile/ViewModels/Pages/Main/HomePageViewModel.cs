using MvvmHelpers.Commands;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.API.UserMobile;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Base;
using ProsysMobile.ViewModels.Pages.System;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class HomePageViewModel : ViewModelBase
    {
        private IDefaultSettingsSQLiteService _defaultSettingsSQLiteService;
        private IUserSQLiteService _userSQLiteService;

        public HomePageViewModel(IDefaultSettingsSQLiteService defaultSettingsSQLiteService, IUserSQLiteService userSQLiteService)
        {
            _defaultSettingsSQLiteService = defaultSettingsSQLiteService;
            _userSQLiteService = userSQLiteService;
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        #region Propertys

        #endregion

        #region Commands
        public ICommand LogoutClickCommand => new Command(async () =>
        {
            try
            {
                //user bilgilerini siliyorum
                _userSQLiteService.DeleteUser(GlobalSetting.Instance.User);
                GlobalSetting.Instance.User = null;

                //TODO: BURASIAYARLANACAK
                DefaultSettings defaultSettings = _defaultSettingsSQLiteService.getSettings("UserId");
                DefaultSettings defaultSettingss = _defaultSettingsSQLiteService.getSettings("UserTokenExpiredDate");
                DefaultSettings defaultSettingsss = _defaultSettingsSQLiteService.getSettings("UserToken");

                _defaultSettingsSQLiteService.Delete(defaultSettings);
                _defaultSettingsSQLiteService.Delete(defaultSettingss);
                _defaultSettingsSQLiteService.Delete(defaultSettingsss);

                //login sayfasına atıyorum
                await NavigationService.SetMainPageAsync<LoginPageViewModel>();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });

        #endregion

    }
}