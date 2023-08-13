using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Order
{
    public class OrderDetailPageViewModel: ViewModelBase
    {
        
        NavigationModel<OrderDetailPageViewParamModel> _orderDetailPageViewModelViewParamModel;

        private bool _isAllItemLoad;
        private int _listPage;
        
        public OrderDetailPageViewModel()
        {
        }
        
        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null && navigationData is NavigationModel<OrderDetailPageViewParamModel> navigationModel)
                _orderDetailPageViewModelViewParamModel = navigationModel;
            else
                throw new ArgumentNullException(nameof(navigationData), "It is mandatory to send parameter of type OrderDetailPageViewModel!");

            PageLoad();
        }

        #region Propertys
        
        private string _backdropTitle = "Order Detail (-)";
        public string BackdropTitle { get => _backdropTitle; set { _backdropTitle = value; PropertyChanged(() => BackdropTitle); } }
        
        private string _itemPrice;
        public string ItemPrice { get => _itemPrice; set { _itemPrice = value; PropertyChanged(() => ItemPrice); } }
        
        private decimal _grossTotal;
        public decimal GrossTotal { get => _grossTotal; set { _grossTotal = value; PropertyChanged(() => GrossTotal); } }
        
        private decimal _netTotal;
        public decimal NetTotal { get => _netTotal; set { _netTotal = value; PropertyChanged(() => NetTotal); } }
        
        private decimal _vatTotal;
        public decimal VatTotal { get => _vatTotal; set { _vatTotal = value; PropertyChanged(() => VatTotal); } }
        
        private ItemsSubDto _selectedItem;
        public ItemsSubDto SelectedItem { get => _selectedItem; set { _selectedItem = value; PropertyChanged(() => SelectedItem); } }
        
        private ObservableRangeCollection<ItemsSubDto> _basketItems;
        public ObservableRangeCollection<ItemsSubDto> BasketItems
        {
            get => _basketItems ?? (_basketItems = new ObservableRangeCollection<ItemsSubDto>());
            set
            {
                _basketItems = value;
                PropertyChanged(() => BasketItems);
            }
        }
        
        #endregion

        #region Commands

        public ICommand ApplyOrderClickCommand => new Command(async () =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                var isError = false;

                // if (string.IsNullOrWhiteSpace(ItemPurchaseQtyText) || ItemPurchaseQtyText.StartsWith("0") || ItemPurchaseQtyText.Contains("-") || !IsInteger(ItemPurchaseQtyText))
                // {
                //     DialogService.WarningToastMessage("Geçersiz adet!");
                //     isError = true;
                // }
                //
                // if (!isError)
                // {
                //     IsBusy = true;
                //
                //     var response = await _saveOrderDetailService.SaveOrderDetail(new OrderDetailsParam
                //     {
                //         UserId = GlobalSetting.Instance.User.ID,
                //         ItemId = ItemId,
                //         Amount = int.Parse(ItemPurchaseQtyText)
                //     }, enPriorityType.UserInitiated);
                //
                //     if (response.IsSuccess)
                //     {
                //     
                //         DialogService.SuccessToastMessage("Ürün sepete eklendi!");
                //     
                //         NavigationService.NavigatePopBackdropAsync();
                //     }
                //     else
                //     {
                //         var errMessageWithErrCode = TOOLS.GetErrorMessageWithErrorCode(response.ExceptionMessage);
                //
                //         DialogService.ErrorToastMessage(!string.IsNullOrWhiteSpace(errMessageWithErrCode)
                //             ? errMessageWithErrCode
                //             : "Ürün sepete eklenemedi!");
                //     }    
                // }
                
            }
            catch (Exception ex)
            {
                DialogService.ErrorToastMessage("Bir hata oluştu!");
                
                ProsysLogger.Instance.CrashLog(ex);
            }

            IsBusy = false;
            DoubleTapping.ResumeTap();
        });
        
        public ICommand ListItemsSelectionChangedCommand => new Command(async (sender) => ItemsListClick(sender));

        public ICommand ListItemsRemainingItemsThresholdReachedCommand => new Command(async (sender) => RemainingItemsThresholdReachedCommand(sender));

        
        #endregion

        #region Methods

        private async void PageLoad()
        {
            try
            {
                BasketItems.Add(new ItemsSubDto
                {
                    Id = 0,
                    CategoryId = 0,
                    Name = "null",
                    Pieces = "null",
                    Price = "null",
                    CurrencyType = "null",
                    Image = "null",
                    Amount = "null"
                });

                VatTotal = new decimal(23.55);
                GrossTotal = new decimal(23.55);
                NetTotal = new decimal(23.55);
                BackdropTitle = "Order Detail (7)";
                return;
                
                _isAllItemLoad = false;
                _listPage = 0;
                BasketItems.Clear();
                GetItemsAndBindFromApi();
                
                if (_orderDetailPageViewModelViewParamModel?.Model != null)
                {
                    IsBusy = true;
                    
                    var itemId = 22;

                    // var item = await _itemDetailService.GetDetail(
                    //     itemId,
                    //     GlobalSetting.Instance.User.ID,
                    //     enPriorityType.UserInitiated
                    // );
                    //
                    // if (item.ResponseData != null && item.IsSuccess)
                    // {
                    //     var responseModel = item.ResponseData;
                    //
                    //     ItemId = responseModel.Item.Id;
                    //     ItemName = responseModel.Item.Name;
                    //     ItemImage = responseModel.Item.Image;
                    //     ItemPieces = responseModel.Item.Pieces;
                    //     ItemPrice = responseModel.Item.Price;
                    //     Categories = responseModel.Categories;
                    //     ItemPurchaseQtyText = responseModel.Item.Amount ?? "0";
                    // }
                    // else
                    // {
                    //     DialogService.ErrorToastMessage("Ürün detayı getirilirken hata oluştu!");
                    //     
                    //     NavigationService.NavigatePopBackdropAsync();
                    // }    
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

        private async void ItemsListClick(object sender)
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            SelectedItem = null;
            DoubleTapping.ResumeTap();
        }
        
        private void RemainingItemsThresholdReachedCommand(object sender)
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                IsBusy = true;
                
                GetItemsAndBindFromApi();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            IsBusy = false;
            DoubleTapping.ResumeTap();
        }

        private async void GetItemsAndBindFromApi()
        {
            try
            {
                if (_isAllItemLoad)
                {
                    return;
                }
                
                IsBusy = true;

                // var result = await _itemsService.GetItems(
                //     filter: Search,
                //     categoryIds: selectedCategoryStr,
                //     page: _listPage,
                //     priorityType: enPriorityType.UserInitiated
                // );
                //
                // if (result?.ResponseData != null && result.IsSuccess)
                // {
                //     if (result.ResponseData.Count < GlobalSetting.Instance.ListPageSize)
                //         _isAllItemLoad = true;
                //
                //     Items.AddRange(result.ResponseData);
                //     
                //     _listPage++;
                // }
                // else
                // {
                //     DialogService.ErrorToastMessage("Ürünleri getiriken bir hata oluştu!");
                // }
            }
            catch (Exception ex)
            {
                DialogService.ErrorToastMessage("Ürünleri getiriken bir hata oluştu!");

                ProsysLogger.Instance.CrashLog(ex);
            }
            
            IsBusy = false;
        }

        #endregion
    }
}