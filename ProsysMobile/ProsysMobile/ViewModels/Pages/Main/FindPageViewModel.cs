using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ProsysMobile.ViewModels.Base;
using System.Threading.Tasks;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Pages;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class FindPageViewModel : ViewModelBase
    {

        public FindPageViewModel()
        {
            Xamarin.Forms.MessagingCenter.Subscribe<AppShell, string>(this, "AppShellTabIndexChange", async (sender, arg) =>
            {
                try
                {
                    if (TOOLS.ToInt(arg) == (int)enTabBarItem.FindPage)
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
            
            Bestsellers = new List<Deneme>()
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

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        #region Propertys
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
        
        private IList<Deneme> _bestsellers;
        public IList<Deneme> Bestsellers
        {
            get
            {
                if (_bestsellers == null)
                    _bestsellers = new ObservableCollection<Deneme>();

                return _bestsellers;
            }
            set
            {
                _bestsellers = value;
                PropertyChanged(() => Categories);
            }
        }
        #endregion

        #region Commands
        
        #endregion

        public class Deneme
        {
            public string Name { get; set; }
            public string Price { get; set; }
            public string Pieces { get; set; }
            public string Image { get; set; }
        }
    }
}