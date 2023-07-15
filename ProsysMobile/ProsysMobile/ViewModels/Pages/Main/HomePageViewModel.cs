﻿using MvvmHelpers.Commands;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels.SQLiteModels;
using ProsysMobile.Pages;
using ProsysMobile.Services.API.ItemCategory;
using ProsysMobile.Services.SQLite;
using ProsysMobile.ViewModels.Base;
using ProsysMobile.ViewModels.Pages.System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.ViewModels.Pages.Order;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class HomePageViewModel : ViewModelBase
    {
        private IItemCategoryService _itemCategoryService;
        private IDefaultSettingsSQLiteService _defaultSettingsSQLiteService;
        private IUserMobileSQLiteService _userSQLiteService;

        public HomePageViewModel( IDefaultSettingsSQLiteService defaultSettingsSQLiteService, IUserMobileSQLiteService userSQLiteService, IItemCategoryService itemCategoryService)
        {
            _defaultSettingsSQLiteService = defaultSettingsSQLiteService;
            _userSQLiteService = userSQLiteService;
            _itemCategoryService = itemCategoryService;

            Xamarin.Forms.MessagingCenter.Subscribe<AppShell, string>(this, "AppShellTabIndexChange", async (sender, arg) =>
            {
                try
                {
                    if (TOOLS.ToInt(arg) == (int)enTabBarItem.HomePage)
                        PageLoad();
                }
                catch (Exception ex)
                {
                    ProsysLogger.Instance.CrashLog(ex);
                }
            });
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        async Task PageLoad()
        {
            var allCategoryId = -1;
            
            var result = await _itemCategoryService.ItemCategory(allCategoryId, Models.CommonModels.Enums.enPriorityType.UserInitiated);

            if (result.ResponseData != null && result.IsSuccess)
            {
                Categories = result.ResponseData;
            }
            else
            {
                
            }
        }

        #region Propertys
        private IList<ItemCategory> _categories;
        public IList<ItemCategory> Categories
        {
            get
            {
                if (_categories == null)
                    _categories = new ObservableCollection<ItemCategory>();

                return _categories;
            }
            set
            {
                _categories = value;
                PropertyChanged(() => Categories);
            }
        }
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
        
        public ICommand ItemDetailTest => new Command(async () =>
        {
            try
            {
                NavigationService.NavigateToBackdropAsync<OrderDetailViewModel>();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });
        
        

        #endregion

    }
}