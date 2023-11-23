using System;
using System.Windows.Input;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Resources.Language;
using ProsysMobile.Services.API.UserMobile;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.System
{
    public class LogoutOrDeleteAccountPageViewModel : ViewModelBase
    {
        private readonly IUserDeleteService _userDeleteService;
        private readonly IDefaultSettingsSQLiteService _defaultSettingsSqLiteService;
        private readonly IUserMobileSQLiteService _userSqLiteService;
        
        public LogoutOrDeleteAccountPageViewModel(IDefaultSettingsSQLiteService defaultSettingsSqLiteService, IUserMobileSQLiteService userSqLiteService, IUserDeleteService userDeleteService)
        {
            _defaultSettingsSqLiteService = defaultSettingsSqLiteService;
            _userSqLiteService = userSqLiteService;
            _userDeleteService = userDeleteService;
        }

        #region Commands
        
        public ICommand LogoutClickCommand => new Command(async () =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                var isOk = await DialogService.ConfirmAsync(Resource.AreYouSureYouWantToLogOut, Resource.Warning_U, Resource.Yes,Resource.Cancel);

                if (!isOk)
                {
                    DoubleTapping.ResumeTap();
                    return;
                }
                
                _userSqLiteService.DeleteUser(GlobalSetting.Instance.User);
                
                GlobalSetting.Instance.User = null;

                var userId = _defaultSettingsSqLiteService.getSettings("UserId");
                var userTokenExpiredDate = _defaultSettingsSqLiteService.getSettings("UserTokenExpiredDate");
                var userToken = _defaultSettingsSqLiteService.getSettings("UserToken");

                _defaultSettingsSqLiteService.Delete(userId);
                _defaultSettingsSqLiteService.Delete(userTokenExpiredDate);
                _defaultSettingsSqLiteService.Delete(userToken);

                await NavigationService.NavigatePopBackdropAsync();
                
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

                var isOk = await DialogService.ConfirmAsync(Resource.AreYouSureYouWantToDeleteTheAccount, Resource.Warning_U, Resource.Yes,Resource.Cancel);

                if (!isOk)
                {
                    DoubleTapping.ResumeTap();
                    return;
                }

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

                    await NavigationService.NavigatePopBackdropAsync();
                    
                    await NavigationService.SetMainPageAsync<LoginPageViewModel>();
                }
                else
                {
                    DialogService.WarningToastMessage(Resource.AnErrorOccurredWhileDeletingTheUser);
                }
                
            }
            catch (Exception ex)
            {
                DialogService.WarningToastMessage(Resource.AnErrorHasOccurred);

                ProsysLogger.Instance.CrashLog(ex);
            }
            
            IsBusy = false;
            DoubleTapping.ResumeTap();
        });
        
        #endregion

        #region Methods

        private void PageLoad()
        {
            
        }

        #endregion
    }
}