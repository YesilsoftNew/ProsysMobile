using ProsysMobile.Helper;
using ProsysMobile.Pages;
using ProsysMobile.Services.API.ItemCategory;
using ProsysMobile.ViewModels.Base;
using System;
using System.Windows.Input;
using MvvmHelpers;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Services.API.Items;
using ProsysMobile.ViewModels.Pages.Item;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class HomePageViewModel : ViewModelBase
    {
        private readonly IItemCategoryService _itemCategoryService;
        private readonly ISaveUserMobileFavoriteItemsService _saveUserMobileFavoriteItemsService;

        public HomePageViewModel(IItemCategoryService itemCategoryService, ISaveUserMobileFavoriteItemsService saveUserMobileFavoriteItemsService)
        {
            _itemCategoryService = itemCategoryService;
            _saveUserMobileFavoriteItemsService = saveUserMobileFavoriteItemsService;

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

        private ObservableRangeCollection<ItemsSubDto> _deals;
        public ObservableRangeCollection<ItemsSubDto> Deals
        {
            get
            {
                if (_deals == null)
                    _deals = new ObservableRangeCollection<ItemsSubDto>();

                return _deals;
            }
            set
            {
                _deals = value;
                PropertyChanged(() => Deals);
            }
        }
        
        #endregion

        #region Commands

        public ICommand CategoryClickCommand => new MvvmHelpers.Commands.Command<object>( (sender) =>
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
        
        public ICommand FavoriteClickCommand => new Command(async (sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                if (sender is ItemsSubDto item)
                {

                    var result = await _saveUserMobileFavoriteItemsService.SaveUserMobileFavoriteItems(
                        userId: GlobalSetting.Instance.User.ID,
                        itemId: item.Id,
                        isFavorite: !item.IsFavorite,
                        enPriorityType.UserInitiated
                    );

                    if (result.IsSuccess)
                    {
                        item.IsFavorite = !item.IsFavorite;
                    }
                    else
                    {
                        DialogService.WarningToastMessage("Ürün favorilere eklenemedi.");
                    }
                }
                
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                
                DialogService.WarningToastMessage("Bir hata oluştu.");
            }

            DoubleTapping.ResumeTap();
        });
        
        public ICommand DealsClickCommand => new Command(async (sender) => ItemsListClick(sender));

        public ICommand ItemDetailClosedEventCommand => new Command(async (sender) =>
        {
            try
            {
                if (!(sender is ItemDetailPageViewParamModel model)) return;

                if (!model.IsAddItem) return;

                Deals.Clear();
                GetDealsAndBindFromApi();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });
        
        #endregion

        #region Methods

        private void PageLoad()
        {
            GetCategoryAndBindFromApi(
                categoryId: Constants.MainCategoryId
            );

            GetDealsAndBindFromApi();
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

        private async void GetDealsAndBindFromApi()
        {
            Deals = new ObservableRangeCollection<ItemsSubDto>
            {
                new ItemsSubDto
                {
                    Id = 0,
                    CategoryId = 0,
                    Name = "null",
                    Pieces = "null",
                    Price = "null",
                    CurrencyType = "null",
                    Image = "http://yas.yesilsoft.net/Images/Legumes.png",
                    Amount = "null",
                    UnitDesc = "null",
                    IsFavorite = false
                },
                new ItemsSubDto
                {
                    Id = 1,
                    CategoryId = 2,
                    Name = "Karpuz",
                    Pieces = "3 dolar",
                    Price = "3 dolar",
                    CurrencyType = "null",
                    Image = "http://yas.yesilsoft.net/Images/Legumes.png",
                    Amount = "null",
                    UnitDesc = "null",
                    IsFavorite = false
                },
                new ItemsSubDto
                {
                    Id = 1,
                    CategoryId = 2,
                    Name = "Karpuz",
                    Pieces = "3 dolar",
                    Price = "3 dolar",
                    CurrencyType = "null",
                    Image = "http://yas.yesilsoft.net/Images/Legumes.png",
                    Amount = "null",
                    UnitDesc = "null",
                    IsFavorite = false
                },
                new ItemsSubDto
                {
                    Id = 1,
                    CategoryId = 2,
                    Name = "Karpuz",
                    Pieces = "3 dolar",
                    Price = "3 dolar",
                    CurrencyType = "null",
                    Image = "http://yas.yesilsoft.net/Images/Legumes.png",
                    Amount = "null",
                    UnitDesc = "null",
                    IsFavorite = false
                }
            };
        }
        
        private async void ItemsListClick(object sender)
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                if (sender is ItemsSubDto item)
                {
                    var navigationModel = new NavigationModel<ItemDetailPageViewParamModel>
                    {
                        Model = new ItemDetailPageViewParamModel
                        {
                            ItemId = item.Id,
                        },
                        ClosedPageEventCommand = ItemDetailClosedEventCommand
                    };

                    await NavigationService.NavigateToBackdropAsync<ItemDetailPageViewModel>(navigationModel);
                }
                
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            DoubleTapping.ResumeTap();
        }
        
        #endregion

    }
}