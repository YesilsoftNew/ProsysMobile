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
using ProsysMobile.ViewModels.Pages.Item;
using ProsysMobile.ViewModels.Pages.Order;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class OrderPageViewModel : ViewModelBase
    {
        private readonly IGetOrderDetailService _getOrderDetailService;
        private readonly IDeleteOrderDetailService _deleteOrderDetailService;

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
            BasketItems.Clear();
            GetBasketItemsAndBindFromApi();
        }

        #region Properties
        
        private OrderDetailsSubDto _selectedItem;
        public OrderDetailsSubDto SelectedItem { get => _selectedItem; set { _selectedItem = value; PropertyChanged(() => SelectedItem); } }
        
        private bool _isRefreshingOrderList;
        public bool IsRefreshingOrderList { get => _isRefreshingOrderList; set { _isRefreshingOrderList = value; PropertyChanged(() => IsRefreshingOrderList); } }
            
        private bool _showOrderDetail;
        public bool ShowOrderDetail { get => _showOrderDetail; set { _showOrderDetail = value; PropertyChanged(() => ShowOrderDetail); } }

        private bool _showEmptyMsg;
        public bool ShowEmptyMsg { get => _showEmptyMsg; set { _showEmptyMsg = value; PropertyChanged(() => ShowEmptyMsg); } }
        
        private string _emptyMsg;
        public string EmptyMsg { get => _emptyMsg; set { _emptyMsg = value; PropertyChanged(() => EmptyMsg); } }
        
        private string _netTotal;
        public string NetTotal { get => _netTotal; set { _netTotal = value; PropertyChanged(() => NetTotal); } }
        
        private string _orderNo;
        public string OrderNo { get => _orderNo; set { _orderNo = value; PropertyChanged(() => OrderNo); } }
        
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

        public ICommand OrderListRefreshCommand => new Command(async () =>
        {
            try
            {
                IsRefreshingOrderList = false;

                BasketItems.Clear();
                GetBasketItemsAndBindFromApi();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });
        
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

                    if (result.IsSuccess)
                    {
                        DialogService.SuccessToastMessage("Ürün sepetten silindi!");

                        BasketItems.Remove(orderDetailsSubDto);

                        if (!BasketItems.Any())
                        {
                            InitializePage(
                                isError: true,
                                errMessage: "Sepette ürün bulunamadı!"
                            );
                        }
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
        
        public ICommand ShowBasketClickCommand => new Command(async (sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                var navigationModel = new NavigationModel<OrderDetailPageViewParamModel>
                {
                    Model = new OrderDetailPageViewParamModel
                    {
                        BasketItems = BasketItems
                    },
                    ClosedPageEventCommand = OrderDetailClosedEventCommand 
                };
                
                await NavigationService.NavigateToBackdropAsync<OrderDetailPageViewModel>(navigationModel);
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
        
        public ICommand ItemDetailClosedEventCommand => new Command(async (sender) =>
        {
            try
            {
                if (!(sender is ItemDetailPageViewParamModel model)) return;

                if (!model.IsAddItem) return;
                
                BasketItems.Clear();
                GetBasketItemsAndBindFromApi();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });
        
        public ICommand OrderDetailClosedEventCommand => new Command(async (sender) =>
        {
            try
            {
                if (!(sender is OrderDetailPageViewParamModel model)) return;

                if (!model.IsSaveBasket) return;
                
                BasketItems.Clear();
                GetBasketItemsAndBindFromApi();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });
        
        #endregion

        #region Methods

        private async void GetBasketItemsAndBindFromApi()
        {
            try
            {
                IsBusy = true;
                
                var result = await _getOrderDetailService.GetOrderDetail(
                    userId: GlobalSetting.Instance.User.ID,
                    priorityType: enPriorityType.UserInitiated
                );

                if (result?.ResponseData != null && result.IsSuccess)
                {
                    BasketItems.AddRange(result.ResponseData.OrderDetailsSubDtos);
                    NetTotal = result.ResponseData.NetTotal;
                    OrderNo = result.ResponseData.OrderNo;

                    InitializePage(!result.ResponseData.OrderDetailsSubDtos.Any(), !result.ResponseData.OrderDetailsSubDtos.Any() ? "Sepette ürün bulunamadı!" : string.Empty);
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
                if (ShowOrderDetail)
                {
                    ShowOrderDetail = false;
                }
                if (!ShowEmptyMsg)
                {
                    ShowEmptyMsg = true;
                }
                EmptyMsg = errMessage;
            }
            else
            {
                if (!ShowOrderDetail)
                {
                    ShowOrderDetail = true;
                }
                if (ShowEmptyMsg)
                {
                    ShowEmptyMsg = false;
                }
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

                var navigationModel = new NavigationModel<ItemDetailPageViewParamModel>
                {
                    Model = new ItemDetailPageViewParamModel
                    {
                        ItemId = SelectedItem.Id
                    },
                    ClosedPageEventCommand = ItemDetailClosedEventCommand
                };
                
                await NavigationService.NavigateToBackdropAsync<ItemDetailPageViewModel>(navigationModel);
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