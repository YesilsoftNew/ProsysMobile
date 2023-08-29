using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Services.API.Items;
using ProsysMobile.Services.API.OrderDetails;
using ProsysMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Item
{
    public class ItemDetailPageViewModel: ViewModelBase
    {
        NavigationModel<ItemDetailPageViewParamModel> _itemDetailPageViewModelViewParamModel;
        private ItemDetailsSubDto _itemDetailsSubDto;
        private bool _firstOpenIsFavorite;
        
        private readonly IItemDetailService _itemDetailService;
        private readonly ISaveOrderDetailService _saveOrderDetailService;
        private readonly ISaveUserMobileFavoriteItemsService _saveUserMobileFavoriteItemsService;
        
        public ItemDetailPageViewModel(IItemDetailService itemDetailService, ISaveOrderDetailService saveOrderDetailService, ISaveUserMobileFavoriteItemsService saveUserMobileFavoriteItemsService)
        {
            _itemDetailService = itemDetailService;
            _saveOrderDetailService = saveOrderDetailService;
            _saveUserMobileFavoriteItemsService = saveUserMobileFavoriteItemsService;
        }
        
        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null && navigationData is NavigationModel<ItemDetailPageViewParamModel> navigationModel)
                _itemDetailPageViewModelViewParamModel = navigationModel;
            else
                throw new ArgumentNullException(nameof(navigationData), "It is mandatory to send parameter of type ItemDetailPageViewModel!");

            PageLoad();
        }

        #region Propertys
        
        private int _itemId;
        public int ItemId { get => _itemId; set { _itemId = value; PropertyChanged(() => ItemId); } }
        
        private string _itemName;
        public string ItemName { get => _itemName; set { _itemName = value; PropertyChanged(() => ItemName); } }
        
        private string _itemPrice;
        public string ItemPrice { get => _itemPrice; set { _itemPrice = value; PropertyChanged(() => ItemPrice); } }
        
        private string _itemPieces;
        public string ItemPieces { get => _itemPieces; set { _itemPieces = value; PropertyChanged(() => ItemPieces); } }
        
        private string _itemImage;
        public string ItemImage { get => _itemImage; set { _itemImage = value; PropertyChanged(() => ItemImage); } }
        
        private string _itemPurchaseQtyText;
        public string ItemPurchaseQtyText { get => _itemPurchaseQtyText; set { _itemPurchaseQtyText = value; PropertyChanged(() => ItemPurchaseQtyText); } }
        
        private string _itemPurchaseQtyTitle = "Order Amount (-)";
        public string ItemPurchaseQtyTitle { get => _itemPurchaseQtyTitle; set { _itemPurchaseQtyTitle = value; PropertyChanged(() => ItemPurchaseQtyTitle); } }
        
        private string _categories;
        public string Categories { get => _categories; set { _categories = value; PropertyChanged(() => Categories); } }
        
        private string _favoriteImageSource;
        public string FavoriteImageSource { get => _favoriteImageSource; set { _favoriteImageSource = value; PropertyChanged(() => FavoriteImageSource); } }
        
        #endregion

        #region Commands

        public ICommand AddBasketClickCommand => new Command(async () =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                var isError = false;

                if (string.IsNullOrWhiteSpace(ItemPurchaseQtyText) || ItemPurchaseQtyText.StartsWith("0") || ItemPurchaseQtyText.Contains("-") || !IsInteger(ItemPurchaseQtyText))
                {
                    DialogService.WarningToastMessage("Geçersiz adet!");
                    isError = true;
                }
                
                if (!isError)
                {
                    IsBusy = true;

                    var response = await _saveOrderDetailService.SaveOrderDetail(new OrderDetailsParam
                    {
                        UserId = GlobalSetting.Instance.User.ID,
                        ItemId = ItemId,
                        Amount = int.Parse(ItemPurchaseQtyText)
                    }, enPriorityType.UserInitiated);

                    if (response.IsSuccess)
                    {
                        _itemDetailPageViewModelViewParamModel.Model.IsAddItem = true;
                    
                        DialogService.SuccessToastMessage("Ürün sepete eklendi!");
                    
                        SetAndClosePage(isAddItem: true);
                    }
                    else
                    {
                        var errMessageWithErrCode = TOOLS.GetErrorMessageWithErrorCode(response.ExceptionMessage);

                        if (!string.IsNullOrWhiteSpace(errMessageWithErrCode))
                        {
                            DialogService.WarningToastMessage(errMessageWithErrCode);
                        }
                        else
                        {
                            DialogService.ErrorToastMessage("Ürün sepete eklenemedi!");
                        }
                    }    
                }
            }
            catch (Exception ex)
            {
                DialogService.ErrorToastMessage("Bir hata oluştu!");
                
                ProsysLogger.Instance.CrashLog(ex);
            }

            IsBusy = false;
            DoubleTapping.ResumeTap();
        });
        
        public ICommand FavoriteClickCommand => new Command(async (sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                if (_itemDetailsSubDto != null)
                {

                    var result = await _saveUserMobileFavoriteItemsService.SaveUserMobileFavoriteItems(
                        userId: GlobalSetting.Instance.User.ID,
                        itemId: _itemDetailsSubDto.Item.Id,
                        isFavorite: !_itemDetailsSubDto.Item.IsFavorite,
                        enPriorityType.UserInitiated
                    );

                    if (result.IsSuccess)
                    {
                        _itemDetailsSubDto.Item.IsFavorite = !_itemDetailsSubDto.Item.IsFavorite;
                        
                        FavoriteImageSource = _itemDetailsSubDto.Item.IsFavorite
                            ? Constants.SelectedFavoriteImageSource
                            : Constants.UnSelectedFavoriteImageSource;
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
        
        public ICommand PreviousClickedCommand => new Command(async (sender) =>
        {
            try
            {
                SetAndClosePage(_firstOpenIsFavorite != _itemDetailsSubDto.Item.IsFavorite);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                
                DialogService.WarningToastMessage("Bir hata oluştu.");
            }
        });
        
        #endregion

        #region Methods

        private async void PageLoad()
        {
            try
            {
                if (_itemDetailPageViewModelViewParamModel?.Model != null)
                {
                    IsBusy = true;
                    
                    var itemId = _itemDetailPageViewModelViewParamModel.Model.ItemId;

                    var item = await _itemDetailService.GetDetail(
                        itemId,
                        GlobalSetting.Instance.User.ID,
                        enPriorityType.UserInitiated
                    );

                    if (item.ResponseData != null && item.IsSuccess)
                    {
                        var responseModel = item.ResponseData;

                        _itemDetailsSubDto = responseModel;
                        _firstOpenIsFavorite = responseModel.Item.IsFavorite;
                        
                        ItemId = responseModel.Item.Id;
                        ItemName = responseModel.Item.Name;
                        ItemImage = responseModel.Item.Image;
                        ItemPieces = responseModel.Item.Pieces;
                        ItemPrice = responseModel.Item.Price;
                        Categories = responseModel.Categories;
                        ItemPurchaseQtyText = string.IsNullOrWhiteSpace(responseModel.Item.Amount) ? "0" : responseModel.Item.Amount;
                        FavoriteImageSource = responseModel.Item.IsFavorite
                            ? Constants.SelectedFavoriteImageSource
                            : Constants.UnSelectedFavoriteImageSource;
                        
                        var orderAmountWithUnitDesc = "Order Amount";
                        if (!string.IsNullOrWhiteSpace(responseModel?.Item?.UnitDesc))
                        {
                            orderAmountWithUnitDesc += $" ({responseModel.Item.UnitDesc})";
                        }
                        ItemPurchaseQtyTitle = orderAmountWithUnitDesc;
                    }
                    else
                    {
                        DialogService.ErrorToastMessage("Ürün detayı getirilirken hata oluştu!");
                        
                        NavigationService.NavigatePopBackdropAsync();
                    }    
                }
                else
                {
                    DialogService.ErrorToastMessage("Ürün detayı getirilirken hata oluştu!");
                    
                    NavigationService.NavigatePopBackdropAsync();
                }
                
            }
            catch (Exception ex)
            {
                DialogService.ErrorToastMessage("Ürün detayı getirilirken hata oluştu!");
                
                NavigationService.NavigatePopBackdropAsync();

                ProsysLogger.Instance.CrashLog(ex);
            }
            
            IsBusy = false;
        }
        
        private bool IsInteger(string value)
        {
            return int.TryParse(value, out _);
        }

        private async void SetAndClosePage(bool isAddItem)
        {
            try
            {
                _itemDetailPageViewModelViewParamModel.Model.IsAddItem = isAddItem;
                
                await NavigationService.NavigatePopBackdropAsync();
                
                _itemDetailPageViewModelViewParamModel.ClosedPageEventCommand.Execute(_itemDetailPageViewModelViewParamModel.Model);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }
        
        #endregion
    }
}