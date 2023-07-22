using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels;
using ProsysMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Order
{
    public class OrderDetailPageViewModel: ViewModelBase
    {
        
        NavigationModel<int> _orderDetailPageViewModelViewParamModel;

        public OrderDetailPageViewModel()
        {
            
        }
        
        public override async Task InitializeAsync(object navigationData)
        {

            if (navigationData != null && navigationData is NavigationModel<int> navigationModel)
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
        
        private IList<ItemCategory> _categories;
        public IList<ItemCategory> Categories
        {
            get
            {
                if (_categories == null)
                    _categories = new ObservableCollection<ItemCategory>();

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
            if (Debugger.IsAttached)
            {
                ItemName = "SUNTAT Gehackte Tomaten";
                ItemImage = "http://yas.yesilsoft.net/Images/Legumes.png";
                ItemPieces = "750 pcs";
                ItemPrice = "100 TL";
                ItemPurchaseQtyText = 0;
                
                Categories = new List<ItemCategory>()
                {
                    new ItemCategory()
                    {
                        CategoryDesc = "Legumes",
                        Image = "http://yas.yesilsoft.net/Images/Legumes.png"
                    },
                    new ItemCategory()
                    {
                        CategoryDesc = "Beverages",
                        Image = "http://yas.yesilsoft.net/Images/Beverages.png"
                    },
                    new ItemCategory()
                    {
                        CategoryDesc = "Groceriesh",
                        Image = "http://yas.yesilsoft.net/Images/Groceriesh.png"
                    },
                    new ItemCategory()
                    {
                        CategoryDesc = "Discount",
                        Image = "http://yas.yesilsoft.net/Images/Discount.png"
                    },
                    new ItemCategory()
                    {
                        CategoryDesc = "Other",
                        Image = "http://yas.yesilsoft.net/Images/Other.png"
                    }
                };


                var itemId = _orderDetailPageViewModelViewParamModel.Model;
            }
        }

        #endregion
    }
}