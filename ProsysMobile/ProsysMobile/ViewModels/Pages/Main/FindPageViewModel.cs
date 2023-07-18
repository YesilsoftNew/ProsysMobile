using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ProsysMobile.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Pages;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class FindPageViewModel : ViewModelBase
    {
        private double _searchTime;
        private bool _isTimerWorking = false;

        
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
            _showBestsellers = true;
            _showItems = false;
            
            _categories = new ObservableRangeCollection<ItemCategory>()
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
            
            _bestsellers = new ObservableRangeCollection<Deneme>()
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
     
        private bool _showBestsellers;
        public bool ShowBestsellers { get => _showBestsellers; set { _showBestsellers = value; PropertyChanged(() => _showBestsellers); } }
        
        private bool _showItems;
        public bool ShowItems { get => _showItems; set { _showItems = value; PropertyChanged(() => _showItems); } }

        private bool _showSubCategories;
        public bool ShowSubCategories { get => _showSubCategories; set { _showSubCategories = value; PropertyChanged(() => ShowSubCategories); } }
        
        private string _search;
        public string Search { get => _search; set { _search = value; PropertyChanged(() => Search); } }
        
        private ObservableRangeCollection<ItemCategory> _categories;
        public ObservableRangeCollection<ItemCategory> Categories
        {
            get
            {
                if (_categories == null)
                    _categories = new ObservableRangeCollection<ItemCategory>();

                return _categories;
            }
            set
            {
                _categories = value;
                PropertyChanged(() => Categories);
            }
        }
        
        private ObservableRangeCollection<Deneme> _bestsellers;
        public ObservableRangeCollection<Deneme> Bestsellers
        {
            get
            {
                if (_bestsellers == null)
                    _bestsellers = new ObservableRangeCollection<Deneme>();

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
        public ICommand SearchEntryTextChangedCommand => new Command((sender) =>
        {
            try
            {
                if (sender != null)
                {
                    _searchTime = 0.8;

                    if (!_isTimerWorking)
                        SearchTimer();
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });
        
        #endregion

        #region Methods

        private void SearchTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
            {
                _searchTime -= 0.1;

                if (_searchTime <= 0.00)
                {
                    ShowBestsellers = string.IsNullOrWhiteSpace(Search);
                    ShowItems = !string.IsNullOrWhiteSpace(Search);
                    
                    if (string.IsNullOrWhiteSpace(Search))
                    {
                        
                    }
                    else
                    {
                        
                    }

                    _isTimerWorking = false;

                    return false;
                }

                _isTimerWorking = true;
                
                return true;
            });
        }

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