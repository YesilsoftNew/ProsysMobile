﻿using System;
using System.Linq;
using ProsysMobile.ViewModels.Base;
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
using ProsysMobile.Pages;
using ProsysMobile.Resources.Language;
using ProsysMobile.Services.API.OrderDetails;
using ProsysMobile.ViewModels.Pages.Item;
using ProsysMobile.ViewModels.Pages.Order;
using ProsysMobile.ViewModels.Pages.System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class OrderPageViewModel : ViewModelBase
    {
        private int focusedBeforeCounterText;

        
        
        private readonly IUpdateOrderDetailForItemService _updateOrderDetailForItemService;
        private readonly IGetOrderDetailService _getOrderDetailService;
        private readonly IDeleteOrderDetailService _deleteOrderDetailService;

        public OrderPageViewModel(IGetOrderDetailService getOrderDetailService, IDeleteOrderDetailService deleteOrderDetailService, IUpdateOrderDetailForItemService updateOrderDetailForItemService)
        {
            _getOrderDetailService = getOrderDetailService;
            _deleteOrderDetailService = deleteOrderDetailService;
            _updateOrderDetailForItemService = updateOrderDetailForItemService;
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
        
        private bool _showEmptyDataGrid;
        public bool ShowEmptyDataGrid { get => _showEmptyDataGrid; set { _showEmptyDataGrid = value; PropertyChanged(() => ShowEmptyDataGrid); } }
        
        private string _emptyMsg;
        public string EmptyMsg { get => _emptyMsg; set { _emptyMsg = value; PropertyChanged(() => EmptyMsg); } }
        
        private string _netTotal;
        public string NetTotal { get => _netTotal; set { _netTotal = value; PropertyChanged(() => NetTotal); } }
        
        private string _orderNo;
        public string OrderNo { get => _orderNo; set { _orderNo = value; PropertyChanged(() => OrderNo); } }
        
        private string _focusedBeforeEntryCounterText;
        public string FocusedBeforeEntryCounterText { get => _focusedBeforeEntryCounterText; set { _focusedBeforeEntryCounterText = value; PropertyChanged(() => OrderNo); } }
        
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
                    DeleteItemInBasket(orderDetailsSubDto);
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
                DialogService.ErrorToastMessage(Resource.AnErrorHasOccurred);
                
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
        
        public ICommand StartShoppingClickCommand => new Command(async (sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;
                
                MessagingCenter.Send(this, "OpenFindPageForBasketPage", Constants.ItemCategoryAll.ID.ToString());
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            DoubleTapping.ResumeTap();
        });
        
        public ICommand UnFocusedCounterCommand => new Command(sender =>
        {
            try
            {
                if (sender is int value)
                {
                    focusedBeforeCounterText = value;
                }
                else
                {
                    focusedBeforeCounterText = 0;
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });
        
        public ICommand ChangeCountCommand => new Command(async sender =>
        {
            try
            {
                if (!(sender is ChangeItemCountCommandParameterModel changeItemCountCommandParameterModel)) return;
                
                var item = BasketItems.FirstOrDefault(x => x.Id == changeItemCountCommandParameterModel.ItemId);

                if (item == null)
                {
                    DialogService.WarningToastMessage(Resource.AnErrorHasOccurred);
                    return;
                }

                IsBusy = true;
                
                if (changeItemCountCommandParameterModel.IsDeleteItem)
                {
                    DeleteItemInBasket(item);
                }
                else
                {
                    var result = await _updateOrderDetailForItemService.UpdateOrderDetailForItem(
                        orderDetailsParam: new OrderDetailsParam
                        {
                            UserId = GlobalSetting.Instance.User.ID,
                            ItemId = changeItemCountCommandParameterModel.ItemId,
                            Amount = changeItemCountCommandParameterModel.Count
                        },
                        priorityType: enPriorityType.UserInitiated
                    );
                    
                    if (result?.ResponseData != null && result.IsSuccess)
                    {
                        var responseModel = result.ResponseData;
                        
                        item.StockCount = responseModel.ItemStockCount;
                        item.StockCountInt = responseModel.ItemStockCountInt;
                        item.Price = responseModel.ItemPrice;
                        NetTotal = responseModel.NetTotal;
                    }
                    else
                    {
                        var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result.ExceptionMessage);

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
                            DialogService.ErrorToastMessage(Resource.AnError0ccurredWhileUpdatingTheQuantity);
                        }

                        item.Amount = focusedBeforeCounterText;
                    }
                }
            }
            catch (Exception ex)
            {
                DialogService.WarningToastMessage(Resource.AnErrorHasOccurred);

                ProsysLogger.Instance.CrashLog(ex);
            }
            
            IsBusy = false;
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

                if (result is { ResponseData: { }, IsSuccess: true })
                {
                    MessagingCenter.Send(this, "UpdateBasketCount", result.ResponseData.OrderDetailsSubDtos.Count);
                    BasketItems.AddRange(result.ResponseData.OrderDetailsSubDtos);
                    NetTotal = result.ResponseData.NetTotal;
                    OrderNo = result.ResponseData.OrderNo;

                    InitializePage(
                        isError: false,
                        isEmptyData: !result.ResponseData.OrderDetailsSubDtos.Any()
                    );
                }
                else
                {
                    InitializePage(
                        isError: true,
                        isEmptyData:false,
                        errMessage: Resource.AnErrorOccurredWhileFetchingTheProducts
                    );
                }
            }
            catch (Exception ex)
            {
                InitializePage(
                    isError: true,
                    isEmptyData:false,
                    errMessage: Resource.AnErrorHasOccurred
                );

                ProsysLogger.Instance.CrashLog(ex);
            }

            IsBusy = false;
        }

        private void InitializePage(bool isError,bool isEmptyData, string errMessage = "")
        {
            ShowEmptyDataGrid = false;

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
            else if (isEmptyData)
            {
                ShowEmptyMsg = false;
                ShowOrderDetail = false;
                ShowEmptyDataGrid = true;
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
                
                await NavigationService.NavigateToAsync<ItemDetailPageViewModel>(navigationModel);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        
            IsBusy = false;
            SelectedItem = null;
            DoubleTapping.ResumeTap();
        }
        
        private async void DeleteItemInBasket(OrderDetailsSubDto orderDetailsSubDto)
        {
            var result = await _deleteOrderDetailService.DeleteOrderDetail(
                orderDetailId: orderDetailsSubDto.OrderDetailId,
                userId: GlobalSetting.Instance.User.ID,
                processDate: DateTime.Now,
                priorityType: enPriorityType.UserInitiated
            );

            if (result.ResponseData != null && result.IsSuccess)
            {
                MessagingCenter.Send(this, "UpdateBasketCount", result.ResponseData.BasketItemCount);
                NetTotal = result.ResponseData.NetTotal;
                
                BasketItems.Remove(orderDetailsSubDto);

                if (!BasketItems.Any())
                {
                    InitializePage(
                        isError: false,
                        isEmptyData: true
                    );
                }
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModel>(result.ExceptionMessage);

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
                
                DialogService.WarningToastMessage(Resource.AnErrorOccurredWhileDeletingTheProductFromTheBasket);
            }
        }
        
        #endregion
        
    }
}