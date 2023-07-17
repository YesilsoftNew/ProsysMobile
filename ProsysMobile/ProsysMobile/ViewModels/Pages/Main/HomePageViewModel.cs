using MvvmHelpers.Commands;
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

        public HomePageViewModel(IItemCategoryService itemCategoryService)
        {
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
                DialogService.ErrorToastMessage("Kategorileri getirirken bir hata oluştu! QQQ");
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

        public ICommand CategoryClickCommand => new Command<object>(async (sender) =>
        {
            try
            {
                var category = sender as ItemCategory;
                
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });

        #endregion

    }
}