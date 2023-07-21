using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ProsysMobile.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Pages;
using ProsysMobile.Services.API.ItemCategory;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class FindPageViewModel : ViewModelBase
    {
        private IItemCategoryService _itemCategoryService;


        private double _searchTime;
        private bool _isTimerWorking = false;
        private List<int> selectedCategories = new List<int>();

        
        public FindPageViewModel(IItemCategoryService itemCategoryService)
        {
            _itemCategoryService = itemCategoryService;

            MessagingCenter.Subscribe<AppShell, string>(this, "AppShellTabIndexChange", async (sender, arg) =>
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
            GetCategoriesAndBindFromApi(
                categoryId: Constants.MainCategoryId,
                isSubCategory: false
            );
            
            Bestsellers = new ObservableRangeCollection<Deneme>()
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

        private bool _showBestsellers = true;
        public bool ShowBestsellers { get => _showBestsellers; set { _showBestsellers = value; PropertyChanged(() => ShowBestsellers); } }
        
        private bool _showItems = false;
        public bool ShowItems { get => _showItems; set { _showItems = value; PropertyChanged(() => ShowItems); } }

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
        
        private ObservableRangeCollection<ItemCategory> _subCategories;
        public ObservableRangeCollection<ItemCategory> SubCategories
        {
            get
            {
                if (_subCategories == null)
                    _subCategories = new ObservableRangeCollection<ItemCategory>();

                return _subCategories;
            }
            set
            {
                _subCategories = value;
                PropertyChanged(() => SubCategories);
            }
        }
        
        private Deneme _selectedBestseller;
        public Deneme SelectedBestseller { get => _selectedBestseller; set { _selectedBestseller = value; PropertyChanged(() => SelectedBestseller); } }

        
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
                PropertyChanged(() => Bestsellers);
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
        
        public ICommand MainCategoryClickCommand => new Command((sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                if (sender is ItemCategory category)
                {
                    var isAnySelectedCategory = selectedCategories.Any(x => x == category.ID);

                    if (!isAnySelectedCategory)
                    {
                        selectedCategories = new List<int> { category.ID };

                        GetCategoriesAndBindFromApi(
                            categoryId: category.ID,
                            isSubCategory: true
                        );
                    }
                    else
                    {
                        selectedCategories.Remove(category.ID);
                    }
                    
                    CheckFilterAndBindShowItems();
                }
                
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            DoubleTapping.ResumeTap();
        });
        
        public ICommand SubCategoryClickCommand => new Command((sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                if (sender is ItemCategory category)
                {
                    var isAnySelectedCategory = selectedCategories.Any(x => x == category.ID);

                    if (!isAnySelectedCategory)
                    {
                        selectedCategories.Add(category.ID);
                    }
                    else
                    {
                        selectedCategories.Remove(category.ID);
                    }

                    CheckFilterAndBindShowItems();
                }
                
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            DoubleTapping.ResumeTap();
        });
        
        public ICommand ListItemsSelectionChangedCommand => new Command((sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                var b = SelectedBestseller;

            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            SelectedBestseller = null;
            DoubleTapping.ResumeTap();
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
                        GetItemsAndBindFromApi();
                    }

                    _isTimerWorking = false;

                    return false;
                }

                _isTimerWorking = true;
                
                return true;
            });
        }

        private void GetItemsAndBindFromApi()
        {
            
        }
        
        private async void GetCategoriesAndBindFromApi(int categoryId, bool isSubCategory)
        {
            try
            {
                var result = await _itemCategoryService.ItemCategory(categoryId, enPriorityType.UserInitiated);
                
                if (result.ResponseData != null && result.IsSuccess)
                {
                    if (!isSubCategory)
                    {
                        Categories = new ObservableRangeCollection<ItemCategory>(result.ResponseData);
                    }
                    else
                    {
                        SubCategories = new ObservableRangeCollection<ItemCategory>(result.ResponseData);
                        ShowSubCategories = SubCategories.Any();
                    }
                }
                else
                {
                    DialogService.ErrorToastMessage("Kategorileri getirirken bir hata oluştu! QQQ");
                }
            }
            catch (Exception ex)
            {
                DialogService.ErrorToastMessage("Kategorileri getirirken bir hata oluştu! QQQ");
                
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        private void CheckFilterAndBindShowItems()
        {
            if (selectedCategories.Any() || !string.IsNullOrWhiteSpace(Search))
            {
                ShowBestsellers = false;
                ShowItems = true;
            }
            else
            {
                if (!selectedCategories.Any())
                {
                    ShowSubCategories = false;
                }
                
                ShowBestsellers = true;
                ShowItems = false;
            }
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