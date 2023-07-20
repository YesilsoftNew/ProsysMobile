using System;
using ProsysMobile.ViewModels.Base;
using System.Threading.Tasks;
using MvvmHelpers;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Pages;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class OrderPageViewModel : ViewModelBase
    {

        public OrderPageViewModel()
        {
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
            GetBasketItemsAndBindFromApi();
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        #region Propertys
        private bool _showBasketItems = true;
        public bool ShowBasketItems { get => _showBasketItems; set { _showBasketItems = value; PropertyChanged(() => ShowBasketItems); } }

        private bool _showEmptyMsg = false;
        public bool ShowEmptyMsg { get => _showEmptyMsg; set { _showEmptyMsg = value; PropertyChanged(() => ShowEmptyMsg); } }
        
        private ObservableRangeCollection<Deneme> _basketItems;
        public ObservableRangeCollection<Deneme> BasketItems
        {
            get
            {
                if (_basketItems == null)
                    _basketItems = new ObservableRangeCollection<Deneme>();

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
        
        #endregion

        #region Methods

        private void GetBasketItemsAndBindFromApi()
        {
            BasketItems = new ObservableRangeCollection<Deneme>()
            {
                new Deneme()
                {
                    Price = "$2,66",
                    Pieces = "500 pcs",
                    Name = "Fruits banana 100% organic",
                    Image = "http://yas.yesilsoft.net/Images/Legumes.png"
                },
                new Deneme()
                {
                    Price = "$2,66",
                    Pieces = "500 pcs",
                    Name = "Fruits banana 100% organic",
                    Image = "http://yas.yesilsoft.net/Images/Legumes.png"
                },
                new Deneme()
                {
                    Price = "$2,66",
                    Pieces = "500 pcs",
                    Name = "Fruits banana 100% organic",
                    Image = "http://yas.yesilsoft.net/Images/Legumes.png"
                },
                new Deneme()
                {
                    Price = "$2,66",
                    Pieces = "500 pcs",
                    Name = "Fruits banana 100% organic",
                    Image = "http://yas.yesilsoft.net/Images/Legumes.png"
                }
            };
        }

        #endregion
        
        public class Deneme
        {
            public string Name { get; set; }
            public string Price { get; set; }
            public string Pieces { get; set; }
            public string Image { get; set; }
            public string Amount { get; set; } = "100";
        }
    }
}