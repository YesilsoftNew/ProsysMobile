using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Services.API.Items;
using ProsysMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Order
{
    public class OrderDetailPageViewModel: ViewModelBase
    {
        
        NavigationModel<OrderDetailPageViewParamModel> _orderDetailPageViewModelViewParamModel;

        private readonly IItemDetailService _itemDetailService;
        
        public OrderDetailPageViewModel(IItemDetailService itemDetailService)
        {
            _itemDetailService = itemDetailService;
        }
        
        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null && navigationData is NavigationModel<OrderDetailPageViewParamModel> navigationModel)
                _orderDetailPageViewModelViewParamModel = navigationModel;
            else
                throw new ArgumentNullException(nameof(navigationData), "It is mandatory to send parameter of type ToastMessagePageViewParamModel!");

            PageLoad();
        }

        #region Propertys
        
        private string _itemName;
        public string ItemName { get => _itemName; set { _itemName = value; PropertyChanged(() => ItemName); } }
        
        private string _itemPrice;
        public string ItemPrice { get => _itemPrice; set { _itemPrice = value; PropertyChanged(() => ItemPrice); } }
        
        private string _itemPieces;
        public string ItemPieces { get => _itemPieces; set { _itemPieces = value; PropertyChanged(() => ItemPieces); } }
        
        private string _itemImage;
        public string ItemImage { get => _itemImage; set { _itemImage = value; PropertyChanged(() => ItemImage); } }
        
        private int _itemPurchaseQtyText;
        public int ItemPurchaseQtyText { get => _itemPurchaseQtyText; set { _itemPurchaseQtyText = value; PropertyChanged(() => ItemPurchaseQtyText); } }
        
        private string _categories;
        public string Categories { get => _categories; set { _categories = value; PropertyChanged(() => Categories); } }
        
        #endregion

        #region Commands

        public ICommand AddBasketClickCommand => new Command(async () =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                if (ItemPurchaseQtyText == 0)
                {
                    
                }
                else
                {
                    
                }
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
                if (_orderDetailPageViewModelViewParamModel?.Model != null)
                {
                    IsBusy = true;
                    
                    var itemId = _orderDetailPageViewModelViewParamModel.Model.ItemId;

                    var item = await _itemDetailService.GetDetail(itemId, enPriorityType.UserInitiated);

                    if (item.ResponseData != null && item.IsSuccess)
                    {
                        var responseModel = item.ResponseData;
                
                        ItemName = responseModel.Item.Name;
                        ItemImage = responseModel.Item.Image;
                        ItemPieces = responseModel.Item.Pieces;
                        ItemPrice = responseModel.Item.Price;
                        Categories = responseModel.Categories;
                        ItemPurchaseQtyText = 0;
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

        #endregion
    }
}