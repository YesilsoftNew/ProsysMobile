using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Services.API.Orders;
using ProsysMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Order
{
    public class OrderDetailPageViewModel: ViewModelBase
    {
        
        NavigationModel<OrderDetailPageViewParamModel> _orderDetailPageViewModelViewParamModel;

        private readonly IGetOrderAmountService _getOrderAmountService;
        private readonly ISaveOrderService _saveOrderService;
        private int _orderId = 0;
        
        public OrderDetailPageViewModel(IGetOrderAmountService getOrderAmountService, ISaveOrderService saveOrderService)
        {
            _getOrderAmountService = getOrderAmountService;
            _saveOrderService = saveOrderService;
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
        
        private string _grossTotal;
        public string GrossTotal { get => _grossTotal; set { _grossTotal = value; PropertyChanged(() => GrossTotal); } }
        
        private string _netTotal;
        public string NetTotal { get => _netTotal; set { _netTotal = value; PropertyChanged(() => NetTotal); } }
        
        private string _vatTotal;
        public string VatTotal { get => _vatTotal; set { _vatTotal = value; PropertyChanged(() => VatTotal); } }
        
        private OrderDetailsSubDto _selectedItem;
        public OrderDetailsSubDto SelectedItem { get => _selectedItem; set { _selectedItem = value; PropertyChanged(() => SelectedItem); } }
        
        private ObservableRangeCollection<OrderDetailsSubDto> _basketItems;
        public ObservableRangeCollection<OrderDetailsSubDto> BasketItems
        {
            get => _basketItems ?? (_basketItems = new ObservableRangeCollection<OrderDetailsSubDto>());
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
                
                IsBusy = true;

                var response = await _saveOrderService.SaveOrder(
                    orderId: _orderId,
                    enPriorityType.UserInitiated);

                if (response.IsSuccess)
                {
                    _orderDetailPageViewModelViewParamModel.Model.IsSaveBasket = true;
                
                    DialogService.SuccessToastMessage("Sepet onaylandı!");
                
                    SetAndClosePage(isSaveBasket: true);
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
                        DialogService.ErrorToastMessage("Sepet onaylanmadı!");
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

        public ICommand ListItemsSelectionChangedCommand => new Command(async (sender) => ItemsListClick(sender));

        #endregion

        #region Methods

        private void PageLoad()
        {
            try
            {
                if (_orderDetailPageViewModelViewParamModel?.Model != null)
                {
                    GetOrderAmountAndBindFromApi();
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
        }

        private void ItemsListClick(object sender)
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
        
        private async void GetOrderAmountAndBindFromApi()
        {
            try
            {
                IsBusy = true;

                var result = await _getOrderAmountService.GetOrderAmount(
                    userId: GlobalSetting.Instance.User.ID,
                    priorityType: enPriorityType.UserInitiated
                );
                
                if (result?.ResponseData != null && result.IsSuccess)
                {
                    var responseModel = result.ResponseData;

                    BasketItems = _orderDetailPageViewModelViewParamModel.Model.BasketItems;
                    VatTotal = responseModel.VatTotal;
                    GrossTotal = responseModel.GrossTotal;
                    NetTotal = responseModel.NetTotal;
                    BackdropTitle = "Order Detail" + $" ({BasketItems.Count})";
                    _orderId = responseModel.OrderId;
                }
                else
                {
                    DialogService.ErrorToastMessage("Ürün bilgilerini getiriken bir hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                DialogService.ErrorToastMessage("Ürün bilgilerini getiriken bir hata oluştu!");

                ProsysLogger.Instance.CrashLog(ex);
            }
            
            IsBusy = false;
        }
        
        private async void SetAndClosePage(bool isSaveBasket)
        {
            try
            {
                _orderDetailPageViewModelViewParamModel.Model.IsSaveBasket = isSaveBasket;
                
                await NavigationService.NavigatePopBackdropAsync();
                
                _orderDetailPageViewModelViewParamModel.ClosedPageEventCommand.Execute(_orderDetailPageViewModelViewParamModel.Model);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        #endregion
    }
}