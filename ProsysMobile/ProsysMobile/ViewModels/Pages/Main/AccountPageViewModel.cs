using System;
using System.Collections.Generic;
using System.Linq;
using ProsysMobile.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using ProsysMobile.Endpoints.UserMobile;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.OtherModels;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Pages;
using ProsysMobile.Services.API.UserMobile;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Pages.System;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class AccountPageViewModel : ViewModelBase
    {
        private readonly IUserDeleteService _userDeleteService;
        private readonly IDefaultSettingsSQLiteService _defaultSettingsSqLiteService;
        private readonly IUserMobileSQLiteService _userSqLiteService;
        
        public AccountPageViewModel(IDefaultSettingsSQLiteService defaultSettingsSqLiteService, IUserMobileSQLiteService userSqLiteService, IUserDeleteService userDeleteService)
        {
            _defaultSettingsSqLiteService = defaultSettingsSqLiteService;
            _userSqLiteService = userSqLiteService;
            _userDeleteService = userDeleteService;

            MessagingCenter.Subscribe<AppShell, string>(this, "AppShellTabIndexChange", async (sender, arg) =>
            {
                try
                {
                    if (TOOLS.ToInt(arg) == (int)enTabBarItem.AccountPage)
                        PageLoad();
                }
                catch (Exception ex)
                {
                    ProsysLogger.Instance.CrashLog(ex);
                }
            });
        }

        #region Propertys
        
        private string _customerName;
        public string CustomerName { get => _customerName; set { _customerName = value; PropertyChanged(() => CustomerName); } }
        
        private string _email;
        public string Email { get => _email; set { _email = value; PropertyChanged(() => Email); } }
        
        private string _customerNameFirstChar;
        public string CustomerNameFirstChar { get => _customerNameFirstChar; set { _customerNameFirstChar = value; PropertyChanged(() => CustomerNameFirstChar); } }
        
        private List<AccountSettings> _settings = new List<AccountSettings>
        {
            new AccountSettings
            {
                Image = "DepotBlack",
                Title = "Orders",
                AccountSettingsType = enAccountSettingsType.Orders
            },
            new AccountSettings
            {
                Image = "Settings",
                Title = "Settings",
                AccountSettingsType = enAccountSettingsType.Settings
            }
        };
        public List<AccountSettings> Settings { get => _settings; set { _settings = value; PropertyChanged(() => Settings); } }
        
        #endregion

        #region Commands
        
        public ICommand LogoutClickCommand => new Command(async () =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                var isOk = await DialogService.ConfirmAsync("Çıkmak istediğinize emin misiniz ?", "UYARI", "Evet","Vazgeç");

                if (!isOk) return;
                
                _userSqLiteService.DeleteUser(GlobalSetting.Instance.User);
                
                GlobalSetting.Instance.User = null;

                var userId = _defaultSettingsSqLiteService.getSettings("UserId");
                var userTokenExpiredDate = _defaultSettingsSqLiteService.getSettings("UserTokenExpiredDate");
                var userToken = _defaultSettingsSqLiteService.getSettings("UserToken");

                _defaultSettingsSqLiteService.Delete(userId);
                _defaultSettingsSqLiteService.Delete(userTokenExpiredDate);
                _defaultSettingsSqLiteService.Delete(userToken);

                await NavigationService.SetMainPageAsync<LoginPageViewModel>();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            DoubleTapping.ResumeTap();
        });
        
        public ICommand DeleteAccountClickCommand => new Command(async () =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                var isOk = await DialogService.ConfirmAsync("Hesabı silmek istediğinize emin misiniz ?", "UYARI", "Evet","Vazgeç");

                if (!isOk) return;

                IsBusy = true;

                var result = await _userDeleteService.UserDelete(
                    userId: GlobalSetting.Instance.User.ID,
                    priorityType: enPriorityType.UserInitiated
                );

                if (result != null && result.IsSuccess)
                {
                    _userSqLiteService.DeleteUser(GlobalSetting.Instance.User);
                
                    GlobalSetting.Instance.User = null;

                    var userId = _defaultSettingsSqLiteService.getSettings("UserId");
                    var userTokenExpiredDate = _defaultSettingsSqLiteService.getSettings("UserTokenExpiredDate");
                    var userToken = _defaultSettingsSqLiteService.getSettings("UserToken");

                    _defaultSettingsSqLiteService.Delete(userId);
                    _defaultSettingsSqLiteService.Delete(userTokenExpiredDate);
                    _defaultSettingsSqLiteService.Delete(userToken);

                    await NavigationService.SetMainPageAsync<LoginPageViewModel>();
                }
                else
                {
                    DialogService.WarningToastMessage("Kullanıcı silinemedi!");
                }
                
            }
            catch (Exception ex)
            {
                DialogService.WarningToastMessage("Bir hata oluştu!");

                ProsysLogger.Instance.CrashLog(ex);
            }
            
            IsBusy = false;
            DoubleTapping.ResumeTap();
        });
        
        
        
        public ICommand SettingsClickCommand => new Command<object>( (sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                if (sender is AccountSettings accountSettings)
                {

                    switch (accountSettings.AccountSettingsType)
                    {
                        case enAccountSettingsType.Orders:
                            DialogService.WarningToastMessage("Şuanda yapılmadı");
                            break;
                        case enAccountSettingsType.Settings:
                            DialogService.WarningToastMessage("Şuanda yapılmadı");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            DoubleTapping.ResumeTap();
        });
        
        #endregion

        #region Methods

        private void PageLoad()
        {
            var user = GlobalSetting.Instance.User;

            CustomerName = !string.IsNullOrWhiteSpace(user.CustomerName) ? user.CustomerName : "-";
            CustomerNameFirstChar = CustomerName.First().ToString();
            Email = user.Email;
        }

        #endregion
        
    }
}