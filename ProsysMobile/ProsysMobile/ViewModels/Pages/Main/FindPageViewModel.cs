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
using ProsysMobile.Models.CommonModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Models.CommonModels.ViewParamModels;
using ProsysMobile.Pages;
using ProsysMobile.Services.API.ItemCategory;
using ProsysMobile.Services.API.Items;
using ProsysMobile.ViewModels.Pages.Order;
using ProsysMobile.ViewModels.Pages.System;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class FindPageViewModel : ViewModelBase
    {
        private IItemCategoryService _itemCategoryService;
        private IItemsService _itemsService;

        private double _searchTime;
        private bool _isTimerWorking = false;
        private List<CategoryFilter> _selectedCategories = new List<CategoryFilter>();
        private int? _mainPageClickedCategoryId = null;

        
        public FindPageViewModel(IItemCategoryService itemCategoryService,IItemsService itemsService)
        {
            _itemCategoryService = itemCategoryService;
            itemsService = itemsService;

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
            
            MessagingCenter.Subscribe<HomePageViewModel, string>(this, "OpenFindPageForMainPageClickCategory", (sender, arg) =>
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(arg))
                    {
                        _mainPageClickedCategoryId = Convert.ToInt32(arg);
                    }
                }
                catch (Exception ex)
                {
                    ProsysLogger.Instance.CrashLog(ex);
                }
            });
        }

        private async void PageLoad()
        {
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
            
            await GetCategoriesAndBindFromApi(
                categoryId: Constants.MainCategoryId,
                isSubCategory: false
             );

            if (_mainPageClickedCategoryId is int mainPageClickedCategoryId)
            {
                Categories.FirstOrDefault(x => x.ID == mainPageClickedCategoryId).IsSelected = true;
                    
                _selectedCategories = new List<CategoryFilter>
                {
                    new CategoryFilter()
                    {
                        Id = mainPageClickedCategoryId,
                        IsMain = true
                    }
                };
            
                GetCategoriesAndBindFromApi(
                    categoryId: mainPageClickedCategoryId,
                    isSubCategory: true
                );
                    
                CheckFilterAndBindShowItems();   
            }
            
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
        
        private ObservableRangeCollection<ItemCategory> _categoriesClone;
        public ObservableRangeCollection<ItemCategory> CategoriesClone
        {
            get
            {
                if (_categoriesClone == null)
                    _categoriesClone = new ObservableRangeCollection<ItemCategory>();

                return _categoriesClone;
            }
            set
            {
                _categoriesClone = value;
                PropertyChanged(() => CategoriesClone);
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
                    category.IsSelected = !category.IsSelected;

                    var selectedCategory = Categories.FirstOrDefault(x => x.ID == category.ID);

                    selectedCategory = category;
                    

                    if (selectedCategory.IsSelected)
                    {
                        _selectedCategories = new List<CategoryFilter>
                        {
                            new CategoryFilter()
                            {
                                Id = category.ID,
                                IsMain = true
                            }
                        };

                        GetCategoriesAndBindFromApi(
                            categoryId: category.ID,
                            isSubCategory: true
                        );
                    }
                    else
                    {
                        _selectedCategories = new List<CategoryFilter>();
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
                    var isAnySelectedCategory = _selectedCategories.Any(x => x.Id == category.ID);

                    if (!isAnySelectedCategory)
                    {
                        _selectedCategories = _selectedCategories.Where(x => x.IsMain).ToList();

                        _selectedCategories.Add(new CategoryFilter()
                        {
                            Id = category.ID,
                            IsMain = false
                        });
                    }
                    else
                    {
                        _selectedCategories = _selectedCategories.Where(x => x.IsMain).ToList();
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

                var navigationModel = new NavigationModel<int>
                {
                    Model = SelectedBestseller.Id
                };
                
                NavigationService.NavigateToBackdropAsync<OrderDetailPageViewModel>(navigationModel);
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
        
        private async Task GetCategoriesAndBindFromApi(int categoryId, bool isSubCategory)
        {
            try
            {
                var result = await _itemCategoryService.ItemCategory(categoryId, enPriorityType.UserInitiated);
                
                if (result.ResponseData != null && result.IsSuccess)
                {
                    if (!isSubCategory)
                    {
                        Categories = new ObservableRangeCollection<ItemCategory>(result.ResponseData);
                        CategoriesClone = Categories;
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
            if (_selectedCategories.Any() || !string.IsNullOrWhiteSpace(Search))
            {
                ShowBestsellers = false;
                ShowItems = true;
            }
            else
            {
                if (!_selectedCategories.Any())
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
            public int Id { get; set; } = 1;
            public string Name { get; set; }
            public string Price { get; set; }
            public string Pieces { get; set; }
            public string Image { get; set; }
        }

        public class CategoryFilter
        {
            public bool IsMain { get; set; }
            public int Id { get; set; }
        }
    }
}