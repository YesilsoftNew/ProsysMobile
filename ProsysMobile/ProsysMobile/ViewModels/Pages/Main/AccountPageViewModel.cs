using System;
using System.Collections.Generic;
using System.Linq;
using ProsysMobile.ViewModels.Base;
using System.Windows.Input;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.OtherModels;
using ProsysMobile.Pages;
using ProsysMobile.Resources.Language;
using ProsysMobile.ViewModels.Pages.Order;
using ProsysMobile.ViewModels.Pages.System;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class AccountPageViewModel : ViewModelBase
    {
        public AccountPageViewModel()
        {
            MessagingCenter.Subscribe<AppShell, string>(this, "AppShellTabIndexChange", (sender, arg) =>
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
            
        private string _versionText;
        public string VersionText { get => _versionText; set { _versionText = value; PropertyChanged(() => VersionText); } }
            
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
                Title = Resource.Orders,
                AccountSettingsType = enAccountSettingsType.Orders
            }
        };
        public List<AccountSettings> Settings { get => _settings; set { _settings = value; PropertyChanged(() => Settings); } }
        
        #endregion

        #region Commands

        public ICommand UserInfoClickCommand => new Command(sender =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;
                
                NavigationService.NavigateToBackdropAsync<LogoutOrDeleteAccountPageViewModel>();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            DoubleTapping.ResumeTap();
        });
        
        public ICommand SettingsClickCommand => new Command<object>(async (sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                if (sender is AccountSettings accountSettings)
                {
                    switch (accountSettings.AccountSettingsType)
                    {
                        case enAccountSettingsType.Orders:
                            var status = PermissionStatus.Unknown;

                            status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

                            if (status != PermissionStatus.Granted && Device.RuntimePlatform != Device.iOS)
                            {
                                status = await TOOLS.CheckPermissions(Permission.Camera);
                            }
                            NavigationService.NavigateToModalAsync<UserOrdersPageViewModel>();
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
            VersionText = "v " + Xamarin.Essentials.VersionTracking.CurrentBuild + $" ({Xamarin.Essentials.VersionTracking.CurrentVersion})";

            var user = GlobalSetting.Instance.User;

            CustomerName = !string.IsNullOrWhiteSpace(user.CustomerName) ? user.CustomerName : "-";
            CustomerNameFirstChar = CustomerName.First().ToString();
            Email = user.Email;
        }

        #endregion
    }
}