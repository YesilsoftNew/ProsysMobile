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
        private IBestsellersService _bestsellersService;

        private double _searchTime;
        private bool _isTimerWorking = false;
        private List<CategoryFilter> _selectedCategories = new List<CategoryFilter>();
        private int? _mainPageClickedCategoryId = null;
        private bool _isAllItemLoad;
        private int _listPage = 1;

        
        public FindPageViewModel(IItemCategoryService itemCategoryService,IItemsService itemsService, IBestsellersService bestsellersService)
        {
            _itemCategoryService = itemCategoryService;
            _itemsService = itemsService;
            _bestsellersService = bestsellersService;

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

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }

        #region Propertys

        private bool _showEmptyText;
        public bool ShowEmptyText { get => _showEmptyText; set { _showEmptyText = value; PropertyChanged(() => ShowEmptyText); } }
        
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
        
        private ItemsSubDto _selectedBestseller;
        public ItemsSubDto SelectedBestseller { get => _selectedBestseller; set { _selectedBestseller = value; PropertyChanged(() => SelectedBestseller); } }
        
        private ObservableRangeCollection<ItemsSubDto> _bestsellers;
        public ObservableRangeCollection<ItemsSubDto> Bestsellers
        {
            get
            {
                if (_bestsellers == null)
                    _bestsellers = new ObservableRangeCollection<ItemsSubDto>();

                return _bestsellers;
            }
            set
            {
                _bestsellers = value;
                PropertyChanged(() => Bestsellers);
            }
        }
        
        private ItemsSubDto _selectedItem;
        public ItemsSubDto SelectedItem { get => _selectedItem; set { _selectedItem = value; PropertyChanged(() => SelectedItem); } }

        private ObservableRangeCollection<ItemsSubDto> _items;
        public ObservableRangeCollection<ItemsSubDto> Items
        {
            get => _items ?? (_items = new ObservableRangeCollection<ItemsSubDto>());
            set
            {
                _items = value;
                PropertyChanged(() => Items);
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
                    _searchTime = 0.4;

                    if (!_isTimerWorking)
                        SearchTimer();
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });
        
        public ICommand MainCategoryClickCommand => new Command(async (sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                IsBusy = true;
                
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

                        await GetCategoriesAndBindFromApi(
                            categoryId: category.ID,
                            isSubCategory: true
                        );
                        
                    }
                    else
                    {
                        _selectedCategories = new List<CategoryFilter>();
                    }
                    
                    _listPage = 0;
                    _isAllItemLoad = false;
                    Items.Clear();

                    GetItemsAndBindFromApi();
                }
                
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            IsBusy = false;
            DoubleTapping.ResumeTap();
        });
        
        public ICommand SubCategoryClickCommand => new Command(async (sender) =>
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
                    
                    _listPage = 0;
                    _isAllItemLoad = false;
                    Items.Clear();

                    await GetItemsAndBindFromApi();
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
                    Model = SelectedItem.Id
                };
                
                NavigationService.NavigateToBackdropAsync<OrderDetailPageViewModel>(navigationModel);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            SelectedItem = null;
            DoubleTapping.ResumeTap();
        });
        
        public ICommand ListItemsRemainingItemsThresholdReachedCommand => new Command(async (s) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                IsBusy = true;
                
                GetItemsAndBindFromApi();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
            
            IsBusy = false;
            DoubleTapping.ResumeTap();
        });
        
        #endregion

        #region Methods

        private async void PageLoad()
        {
            try
            {
                IsBusy = true;

                await GetCategoriesAndBindFromApi(
                    categoryId: Constants.MainCategoryId,
                    isSubCategory: false
                );

                await GetBestsellersAndBindFromApi();

                if (_mainPageClickedCategoryId is int mainPageClickedCategoryId)
                {
                    if (Categories != null)
                        Categories.FirstOrDefault(x => x.ID == mainPageClickedCategoryId).IsSelected = true;

                    _selectedCategories = new List<CategoryFilter>
                    {
                        new CategoryFilter()
                        {
                            Id = mainPageClickedCategoryId,
                            IsMain = true
                        }
                    };
                
                    await GetCategoriesAndBindFromApi(
                        categoryId: mainPageClickedCategoryId,
                        isSubCategory: true
                    );
                        
                    GetItemsAndBindFromApi();
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            IsBusy = false;
        }

        private void SearchTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
            {
                _searchTime -= 0.1;

                if (_searchTime <= 0.00)
                {
                    _isAllItemLoad = false;

                    var isNullSearch = string.IsNullOrWhiteSpace(Search);
                    var isNotNullSearch = !string.IsNullOrWhiteSpace(Search);
                    
                    ShowBestsellers = isNullSearch;
                    ShowItems = isNotNullSearch;
                    ShowEmptyText = false;

                    if (!string.IsNullOrWhiteSpace(Search) || _selectedCategories.Any())
                    {
                        _listPage = 0;
                        Items.Clear();

                        GetItemsAndBindFromApi();
                    }

                    _isTimerWorking = false;
                    
                    return false;
                }

                _isTimerWorking = true;
                
                return true;
            });
        }

        private async Task GetItemsAndBindFromApi()
        {
            try
            {
                IsBusy = true;

                if (_isAllItemLoad)
                {
                    return;
                }
                
                var selectedCategoryStr = _selectedCategories
                    .OrderBy(x=> x.IsMain)
                    .FirstOrDefault()?
                    .Id
                    .ToString();
                
                var result = await _itemsService.GetItems(
                    filter: Search,
                    categoryIds: selectedCategoryStr,
                    page: _listPage,
                    priorityType: enPriorityType.UserInitiated
                );

                if (result?.ResponseData != null && result.IsSuccess)
                {
                    if (result.ResponseData.Count < GlobalSetting.Instance.ListPageSize)
                        _isAllItemLoad = true;
                    
                    Items.AddRange(result.ResponseData);
                    
                    _listPage++;
                }
                else
                {
                    DialogService.ErrorToastMessage("Ürünleri getiriken bir hata oluştu!");
                }
                
                CheckFilterAndBindShowItems();
            }
            catch (Exception ex)
            {
                DialogService.ErrorToastMessage("Ürünleri getiriken bir hata oluştu!");

                ProsysLogger.Instance.CrashLog(ex);
            }

            IsBusy = false;
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

        private async Task GetBestsellersAndBindFromApi()
        {
            try
            {
                var result = await _bestsellersService.GetBestsellers(enPriorityType.UserInitiated);

                if (result?.ResponseData != null && result.IsSuccess)
                {
                    Bestsellers = new ObservableRangeCollection<ItemsSubDto>(result.ResponseData);
                }
                else
                {
                    DialogService.ErrorToastMessage("En çok satanları getirirken bir hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                DialogService.ErrorToastMessage("En çok satanları getirirken bir hata oluştu!");

                ProsysLogger.Instance.CrashLog(ex);
            }
        }
        
        private void CheckFilterAndBindShowItems()
        {
            if (_selectedCategories.Any() || !string.IsNullOrWhiteSpace(Search))
            {
                ShowBestsellers = false;
                ShowItems = true;
                
                if (!Items.Any())
                {
                    ShowEmptyText = true;
                    ShowItems = false;
                }
            }
            else
            {
                if (!_selectedCategories.Any())
                {
                    ShowSubCategories = false;
                }
                
                ShowBestsellers = true;
                ShowItems = false;
                ShowEmptyText = false;
            }
        }
        
        #endregion

        public class CategoryFilter
        {
            public bool IsMain { get; set; }
            public int Id { get; set; }
        }
    }
}