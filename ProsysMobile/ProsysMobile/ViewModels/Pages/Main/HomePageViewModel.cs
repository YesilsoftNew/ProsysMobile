using ProsysMobile.Helper;
using ProsysMobile.Pages;
using ProsysMobile.Services.API.ItemCategory;
using ProsysMobile.ViewModels.Base;
using System;
using System.Windows.Input;
using MvvmHelpers;
using Plugin.LocalNotification;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using Xamarin.CommunityToolkit.Converters;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class HomePageViewModel : ViewModelBase
    {
        private IItemCategoryService _itemCategoryService;

        public HomePageViewModel(IItemCategoryService itemCategoryService)
        {
            _itemCategoryService = itemCategoryService;

            Xamarin.Forms.MessagingCenter.Subscribe<AppShell, string>(this, "AppShellTabIndexChange", (sender, arg) =>
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

        #region Propertys
        private ObservableRangeCollection<ItemCategory> _categories;
        public ObservableRangeCollection<ItemCategory> Categories
        {
            get
            {
                if (_categories == null)
                    _categories = new ObservableRangeCollection<ItemCategory>();

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

        public ICommand CategoryClickCommand => new Command<object>( (sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                var category = sender as ItemCategory;
                
                MessagingCenter.Send<HomePageViewModel, string>(this, "OpenFindPageForMainPageClickCategory", category.ID.ToString());
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            DoubleTapping.ResumeTap();
        });

        
        public ICommand Allahbalaniversinmumtazcommand => new Command<object>( (sender) =>
        {
            var random = new Random();

            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = "aaa",
                Title = "aaa",
                NotificationId = random.Next(1, int.MaxValue)
            };

            NotificationCenter.Current.Show(notification);
        });

        
        #endregion

        #region Methods

        private void PageLoad()
        {
            GetCategoryAndBindFromApi(
                categoryId: Constants.MainCategoryId
            );
        }
        
        private async void GetCategoryAndBindFromApi(int categoryId)
        {
            try
            {
                IsBusy = true;
                
                var result = await _itemCategoryService.ItemCategory(categoryId, enPriorityType.UserInitiated);
                
                if (result.ResponseData != null && result.IsSuccess)
                {
                    result.ResponseData.Insert(0, Constants.ItemCategoryAll);
                    
                    Categories = new ObservableRangeCollection<ItemCategory>(result.ResponseData);
                }
                else
                {
                    DialogService.ErrorToastMessage("Kategorileri getirirken bir hata oluştu!");
                }

            }
            catch (Exception ex)
            {
                DialogService.ErrorToastMessage("Kategorileri getirirken bir hata oluştu!");
                
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            IsBusy = false;
        }

        #endregion

    }
}