using System;
using ProsysMobile.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Pages.System;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class AccountPageViewModel : ViewModelBase
    {
        private IDefaultSettingsSQLiteService _defaultSettingsSqLiteService;
        private IUserMobileSQLiteService _userSqLiteService;
        
        public AccountPageViewModel(IDefaultSettingsSQLiteService defaultSettingsSqLiteService, IUserMobileSQLiteService userSqLiteService)
        {
            _defaultSettingsSqLiteService = defaultSettingsSqLiteService;
            _userSqLiteService = userSqLiteService;
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
                _userSqLiteService.DeleteUser(GlobalSetting.Instance.User);
                GlobalSetting.Instance.User = null;

                //TODO: BURASIAYARLANACAK
                DefaultSettings defaultSettings = _defaultSettingsSqLiteService.getSettings("UserId");
                DefaultSettings defaultSettingss = _defaultSettingsSqLiteService.getSettings("UserTokenExpiredDate");
                DefaultSettings defaultSettingsss = _defaultSettingsSqLiteService.getSettings("UserToken");

                _defaultSettingsSqLiteService.Delete(defaultSettings);
                _defaultSettingsSqLiteService.Delete(defaultSettingss);
                _defaultSettingsSqLiteService.Delete(defaultSettingsss);

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