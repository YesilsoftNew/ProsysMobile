using System;
using System.Linq;
using ProsysMobile.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Pages;
using ProsysMobile.Services.API.OrderDetails;
using ProsysMobile.ViewModels.Pages.Order;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class OrderPageViewModel : ViewModelBase
    {
        private readonly IGetOrderDetailService _getOrderDetailService;
        private readonly IDeleteOrderDetailService _deleteOrderDetailService;
        
        private bool _isAllItemLoad;
        private int _listPage;

        public OrderPageViewModel(IGetOrderDetailService getOrderDetailService, IDeleteOrderDetailService deleteOrderDetailService)
        {
            _getOrderDetailService = getOrderDetailService;
            _deleteOrderDetailService = deleteOrderDetailService;
            Xamarin.Forms.MessagingCenter.Subscribe<AppShell, string>(this, "AppShellTabIndexChange", async (sender, arg) =>
            {
                try
                {
                    if (TOOLS.ToInt(arg) == (int)enTabBarItem.OrderPage)
                        PageLoad();
                }
                catch (Exception ex)
                {
                    ProsysLogger.Instance.CrashLog(ex);
                }
            });
        }

        private void PageLoad()
        {
            _isAllItemLoad = false;
            _listPage = 0;
            BasketItems.Clear();
            GetBasketItemsAndBindFromApi();
        }

        #region Propertys
        
        private OrderDetailsSubDto _selectedItem;
        public OrderDetailsSubDto SelectedItem { get => _selectedItem; set { _selectedItem = value; PropertyChanged(() => SelectedItem); } }
        
        private bool _showBasketItems;
        public bool ShowBasketItems { get => _showBasketItems; set { _showBasketItems = value; PropertyChanged(() => ShowBasketItems); } }

        private bool _showEmptyMsg;
        public bool ShowEmptyMsg { get => _showEmptyMsg; set { _showEmptyMsg = value; PropertyChanged(() => ShowEmptyMsg); } }
        
        private string _emptyMsg;
        public string EmptyMsg { get => _emptyMsg; set { _emptyMsg = value; PropertyChanged(() => EmptyMsg); } }
        
        private ObservableRangeCollection<OrderDetailsSubDto> _basketItems;
        public ObservableRangeCollection<OrderDetailsSubDto> BasketItems
        {
            get
            {
                if (_basketItems == null)
                    _basketItems = new ObservableRangeCollection<OrderDetailsSubDto>();

                return _basketItems;
            }
            set
            {
                _basketItems = value;
                PropertyChanged(() => BasketItems);
            }
        }
        
        #endregion

        #region Commands
        
        public ICommand DeleteItemClickCommand => new Command(async (sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                IsBusy = true;

                if (sender is OrderDetailsSubDto orderDetailsSubDto)
                {
                    var result = await _deleteOrderDetailService.DeleteOrderDetail(
                        orderDetailId: orderDetailsSubDto.OrderDetailId,
                        priorityType: enPriorityType.UserInitiated
                    );

                    if (result.IsSuccess && result.ResponseData)
                    {
                        DialogService.SuccessToastMessage("Ürün sepetten silindi!");

                        _isAllItemLoad = false;
                        _listPage = 0;
                        BasketItems.Clear();
                        GetBasketItemsAndBindFromApi();
                    }
                    else
                    {
                        DialogService.WarningToastMessage("Ürün sepetten silinemedi!");
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

        public ICommand ListBasketSelectionChangedCommand => new Command(async (sender) => ItemsListClick(sender));

        public ICommand ListBasketRemainingItemsThresholdReachedCommand => new Command((sender) => RemainingItemsThresholdReachedCommand(sender));
        
        #endregion

        #region Methods

        private async void GetBasketItemsAndBindFromApi()
        {
            try
            {
                if (_isAllItemLoad)
                {
                    return;
                }

                IsBusy = true;
                
                var result = await _getOrderDetailService.GetOrderDetail(
                    userId: GlobalSetting.Instance.User.ID,
                    priorityType: enPriorityType.UserInitiated
                );

                if (result?.ResponseData != null && result.IsSuccess)
                {
                    if (result.ResponseData.Count < GlobalSetting.Instance.ListPageSize)
                        _isAllItemLoad = true;

                    BasketItems.AddRange(result.ResponseData);

                    _listPage++;

                    InitializePage(!result.ResponseData.Any(), !result.ResponseData.Any() ? "Sepette ürün bulunamadı!" : string.Empty);
                }
                else
                {
                    InitializePage(true, "Ürünleri getiriken bir hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                InitializePage(true, "Bir hata oluştu!");

                ProsysLogger.Instance.CrashLog(ex);
            }

            IsBusy = false;
        }

        private void InitializePage(bool isError, string errMessage = "")
        {
            if (isError)
            {
                ShowBasketItems = false;
                ShowEmptyMsg = true;
                EmptyMsg = errMessage;
            }
            else
            {
                ShowBasketItems = true;
                ShowEmptyMsg = false;
            }
        }

        private void RemainingItemsThresholdReachedCommand(object sender)
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                GetBasketItemsAndBindFromApi();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            DoubleTapping.ResumeTap();
        }
        
        private async void ItemsListClick(object sender)
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;
        
                IsBusy = true;

                var navigationModel = new NavigationModel<OrderDetailPageViewParamModel>
                {
                    Model = new OrderDetailPageViewParamModel
                    {
                        ItemId = SelectedItem.Id
                    }
                };
                
                await NavigationService.NavigateToBackdropAsync<OrderDetailPageViewModel>(navigationModel);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        
            IsBusy = false;
            SelectedItem = null;
            DoubleTapping.ResumeTap();
        }
        
        #endregion
        
    }
}