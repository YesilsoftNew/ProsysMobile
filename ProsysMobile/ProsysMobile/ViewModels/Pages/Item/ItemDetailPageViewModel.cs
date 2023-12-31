using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using Newtonsoft.Json;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.OtherModels;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Resources.Language;
using ProsysMobile.Services.API.Items;
using ProsysMobile.Services.API.OrderDetails;
using ProsysMobile.ViewModels.Base;
using ProsysMobile.ViewModels.Pages.Other;
using ProsysMobile.ViewModels.Pages.System;
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
            if (navigationData != null && navigationData is NavigationModel<ItemDetailPageViewParamModel> navigationModel && navigationModel?.Model != null)
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

        private string _itemUnitPrice;
        public string ItemUnitPrice { get => _itemUnitPrice; set { _itemUnitPrice = value; PropertyChanged(() => ItemUnitPrice); } }
        
        private string _itemPrice;
        public string ItemPrice { get => _itemPrice; set { _itemPrice = value; PropertyChanged(() => ItemPrice); } }
        
        private string _itemPieces;
        public string ItemPieces { get => _itemPieces; set { _itemPieces = value; PropertyChanged(() => ItemPieces); } }
        
        private Color _itemPiecesTextColor;
        public Color ItemPiecesTextColor { get => _itemPiecesTextColor; set { _itemPiecesTextColor = value; PropertyChanged(() => ItemPiecesTextColor); } }
        
        private string _itemPurchaseQtyText;
        public string ItemPurchaseQtyText { get => _itemPurchaseQtyText; set { _itemPurchaseQtyText = value; PropertyChanged(() => ItemPurchaseQtyText); } }
        
        private string _itemPurchaseQtyTitle = "Order Amount (-)";
        public string ItemPurchaseQtyTitle { get => _itemPurchaseQtyTitle; set { _itemPurchaseQtyTitle = value; PropertyChanged(() => ItemPurchaseQtyTitle); } }
        
        private ObservableRangeCollection<Tag> _tags;
        public ObservableRangeCollection<Tag> Tags
        {
            get => _tags ?? (_tags = new ObservableRangeCollection<Tag>());
            set
            {
                _tags = value;
                PropertyChanged(() => Tags);
            }
        }
        
        private string _favoriteImageSource;
        public string FavoriteImageSource { get => _favoriteImageSource; set { _favoriteImageSource = value; PropertyChanged(() => FavoriteImageSource); } }
        
        private bool _isFocusAndSelectText = true;
        public bool IsFocusAndSelectText { get => _isFocusAndSelectText; set { _isFocusAndSelectText = value; PropertyChanged(() => IsFocusAndSelectText); } }
        
        private ObservableRangeCollection<string> _images;

        public ObservableRangeCollection<string> Images
        {
            get
            {
                if (_images == null)
                    _images = new ObservableRangeCollection<string>();

                return _images;
            }
            set
            {
                _images = value;
                PropertyChanged(() => Images);
            }
        }

        #endregion

        #region Commands

        public ICommand AddBasketClickCommand => new Command(async () =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                var itemPurchaseQtyTextTrimValue = ItemPurchaseQtyText.TrimStart('0');
                
                var isError = false;

                if (string.IsNullOrWhiteSpace(itemPurchaseQtyTextTrimValue) || itemPurchaseQtyTextTrimValue.Contains("-") || !IsInteger(itemPurchaseQtyTextTrimValue))
                {
                    DialogService.WarningToastMessage(Resource.InvalidQuantity);
                    isError = true;
                }
                
                if (!isError)
                {
                    IsBusy = true;

                    var response = await _saveOrderDetailService.SaveOrderDetail(new OrderDetailsParam
                    {
                        UserId = GlobalSetting.Instance.User.ID,
                        ItemId = ItemId,
                        Amount = int.Parse(itemPurchaseQtyTextTrimValue)
                    }, enPriorityType.UserInitiated);

                    if (response is { ResponseData: { }, IsSuccess: true })
                    {
                        MessagingCenter.Send(this, "UpdateBasketCount", response.ResponseData.BasketItemCount);
                        
                        _itemDetailPageViewModelViewParamModel.Model.IsAddItem = true;
                    
                        DialogService.SuccessToastMessage(Resource.TheProductHasBeenAddedToTheBasket);
                    
                        SetAndClosePage(isAddItem: true);
                    }
                    else
                    {
                        var errorModel = JsonConvert.DeserializeObject<ErrorModel>(response.ExceptionMessage);
                        
                        if (errorModel.ErrorCode == ErrorCode.CheckTime)
                        {
                            DialogService.WarningToastMessage(Resource.YourTransactionHasNotBeenCompletedBecauseTheStoreIsClosed);
                            
                            var startTime = errorModel.Parameter.Split("-")[0].Trim();
                            var endTime = errorModel.Parameter.Split("-")[1].Trim();
                            
                            var navigationModel = new NavigationModel<MaintenancePageViewParamModel>
                            {
                                Model = new MaintenancePageViewParamModel
                                {
                                    CheckTimeResponseModel = new CheckTimeResponseModel
                                    {
                                        IsContinue = false,
                                        StartTime = startTime,
                                        EndTime = endTime
                                    }
                                }
                            };
                            
                            await NavigationService.SetMainPageAsync<MaintenancePageViewModel>(true, navigationModel);
                            
                            return;
                        }
                        
                        var errMessageWithErrCode = TOOLS.GetErrorMessageWithErrorCode(errorModel.ErrorCode);

                        errMessageWithErrCode = errMessageWithErrCode.Replace("@xxx", errorModel.Parameter);
                        
                        if (!string.IsNullOrWhiteSpace(errMessageWithErrCode))
                        {
                            DialogService.WarningToastMessage(errMessageWithErrCode);
                        }
                        else
                        {
                            DialogService.ErrorToastMessage(Resource.TheProductCouldNotBeAddedToTheBasket);
                        }
                    }    
                }
            }
            catch (Exception ex)
            {
                DialogService.ErrorToastMessage(Resource.AnErrorHasOccurred);
                
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
                    IsBusy = true;
                    
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
                        DialogService.WarningToastMessage(Resource.TheProductCouldNotBeAddedToFavorites);
                    }
                }
                
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                
                DialogService.WarningToastMessage(Resource.AnErrorHasOccurred);
            }

            IsBusy = false;
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
                
                DialogService.WarningToastMessage(Resource.AnErrorHasOccurred);
            }
        });

        public ICommand ImageClickCommand => new Command((sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                if (!(sender is string imageSource))
                {
                    DoubleTapping.ResumeTap();
                    return;
                }
                
                var model = new NavigationModel<BigImagePageViewParamModel>()
                {
                    Model = new BigImagePageViewParamModel
                    {
                        Source = imageSource
                    },
                };

                NavigationService.NavigateToBackdropAsync<BigImagePageViewModel>(model);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            DoubleTapping.ResumeTap();
        });
        
        #endregion

        #region Methods

        private async void PageLoad()
        {
            try
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
                    ItemUnitPrice = responseModel.Item.UnitPriceDesc;
                    Images = new ObservableRangeCollection<string>(responseModel.Item.Images);
                    ItemPieces = responseModel.Item.Pieces;

                    if (responseModel.Item.IsStockFinished)
                    {
                        ItemPieces = ItemPieces + " - " + Resource.SoldOut;
                        ItemPiecesTextColor = Color.Red;
                    }
                    else
                    {
                        Application.Current.Resources.TryGetValue("Gray3", out var gray3Color);
                
                        if (gray3Color == null) return;
                
                        var gray3 = (Color)gray3Color;

                        ItemPiecesTextColor = gray3;
                    }
                    
                    ItemPrice = responseModel.Item.Price;
                    Tags = new ObservableRangeCollection<Tag>(responseModel.Tags ?? new List<Tag>());
                    ItemPurchaseQtyText = string.IsNullOrWhiteSpace(responseModel.Item.Amount) || responseModel.Item.Amount == "0" ? "1" : responseModel.Item.Amount;
                    FavoriteImageSource = responseModel.Item.IsFavorite
                        ? Constants.SelectedFavoriteImageSource
                        : Constants.UnSelectedFavoriteImageSource;
                    
                    var orderAmountWithUnitDesc = Resource.OrderAmount;
                    if (!string.IsNullOrWhiteSpace(responseModel?.Item?.UnitDesc))
                    {
                        orderAmountWithUnitDesc += $" ({responseModel.Item.UnitDesc})";
                    }
                    ItemPurchaseQtyTitle = orderAmountWithUnitDesc;
                }
                else
                {
                    DialogService.ErrorToastMessage(Resource.AnErrorOccurredWhileFetchingProductDetails);
                    
                    NavigationService.NavigatePopAsync();
                }
            }
            catch (Exception ex)
            {
                DialogService.ErrorToastMessage(Resource.AnErrorHasOccurred);
                
                NavigationService.NavigatePopAsync();

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
                
                await NavigationService.NavigatePopAsync();
                
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