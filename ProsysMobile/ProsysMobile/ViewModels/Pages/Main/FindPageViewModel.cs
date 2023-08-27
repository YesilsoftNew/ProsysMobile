using System;
using System.Collections.Generic;
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
using ProsysMobile.ViewModels.Pages.Item;
using ProsysMobile.ViewModels.Pages.Order;
using ProsysMobile.ViewModels.Pages.Other;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ProsysMobile.ViewModels.Pages.Main
{
    public class FindPageViewModel : ViewModelBase
    {
        private readonly IItemCategoryService _itemCategoryService;
        private readonly IItemsService _itemsService;
        private readonly IBestsellersService _bestsellersService;
        private readonly ISaveUserMobileFavoriteItemsService _saveUserMobileFavoriteItemsService;
        
        private double _searchTime;
        private bool _isTimerWorking;
        private List<CategoryFilter> _selectedCategories = new List<CategoryFilter>();
        private int? _mainPageClickedCategoryId;
        private bool _isAllItemLoad;
        private int _listPage = 1;
        private enItemListType _currentItemListType = enItemListType.Primary;


        public FindPageViewModel(IItemCategoryService itemCategoryService, IItemsService itemsService, IBestsellersService bestsellersService, ISaveUserMobileFavoriteItemsService saveUserMobileFavoriteItemsService)
        {
            _itemCategoryService = itemCategoryService;
            _itemsService = itemsService;
            _bestsellersService = bestsellersService;
            _saveUserMobileFavoriteItemsService = saveUserMobileFavoriteItemsService;

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

        #region Propertys

        private bool _showChangeItemListDesignButton;
        public bool ShowChangeItemListDesignButton { get => _showChangeItemListDesignButton; set { _showChangeItemListDesignButton = value; PropertyChanged(() => ShowChangeItemListDesignButton); } }

        private bool _showEmptyText;
        public bool ShowEmptyText { get => _showEmptyText; set { _showEmptyText = value; PropertyChanged(() => ShowEmptyText); } }

        private bool _showBestsellers = true;
        public bool ShowBestsellers { get => _showBestsellers; set { _showBestsellers = value; PropertyChanged(() => ShowBestsellers); } }

        private bool _showItemsPrimary;
        public bool ShowItemsPrimary { get => _showItemsPrimary; set { _showItemsPrimary = value; PropertyChanged(() => ShowItemsPrimary); } }

        private bool _showItemsSecondary;
        public bool ShowItemsSecondary { get => _showItemsSecondary; set { _showItemsSecondary = value; PropertyChanged(() => ShowItemsSecondary); } }

        private bool _showItemsTertiary;
        public bool ShowItemsTertiary { get => _showItemsTertiary; set { _showItemsTertiary = value; PropertyChanged(() => ShowItemsTertiary); } }

        private bool _showSubCategories;
        public bool ShowSubCategories { get => _showSubCategories; set { _showSubCategories = value; PropertyChanged(() => ShowSubCategories); } }

        private string _search;
        public string Search { get => _search; set { _search = value; PropertyChanged(() => Search); } }

        private ItemCategory _selectedCategoryListItem;
        public ItemCategory SelectedCategoryListItem { get => _selectedCategoryListItem; set { _selectedCategoryListItem = value; PropertyChanged(() => SelectedCategoryListItem); } }

        private ObservableRangeCollection<ItemCategory> _categories;
        public ObservableRangeCollection<ItemCategory> Categories
        {
            get => _categories ?? (_categories = new ObservableRangeCollection<ItemCategory>());
            set
            {
                _categories = value;
                PropertyChanged(() => Categories);
            }
        }

        private ObservableRangeCollection<ItemCategory> _subCategories;
        public ObservableRangeCollection<ItemCategory> SubCategories
        {
            get => _subCategories ?? (_subCategories = new ObservableRangeCollection<ItemCategory>());
            set
            {
                _subCategories = value;
                PropertyChanged(() => SubCategories);
            }
        }

        private ObservableRangeCollection<ItemsSubDto> _bestsellers;
        public ObservableRangeCollection<ItemsSubDto> Bestsellers
        {
            get => _bestsellers ?? (_bestsellers = new ObservableRangeCollection<ItemsSubDto>());
            set
            {
                _bestsellers = value;
                PropertyChanged(() => Bestsellers);
            }
        }

        private ItemsSubDto _selectedItem;
        public ItemsSubDto SelectedItem { get => _selectedItem; set { _selectedItem = value; PropertyChanged(() => SelectedItem); } }

        private ObservableRangeCollection<ItemsSubDto> _itemsPrimary;
        public ObservableRangeCollection<ItemsSubDto> ItemsPrimary
        {
            get => _itemsPrimary ?? (_itemsPrimary = new ObservableRangeCollection<ItemsSubDto>());
            set
            {
                _itemsPrimary = value;
                PropertyChanged(() => ItemsPrimary);
            }
        }

        private ObservableRangeCollection<ItemsSubDto> _itemsTertiary;
        public ObservableRangeCollection<ItemsSubDto> ItemsTertiary
        {
            get => _itemsTertiary ?? (_itemsTertiary = new ObservableRangeCollection<ItemsSubDto>());
            set
            {
                _itemsTertiary = value;
                PropertyChanged(() => ItemsTertiary);
            }
        }

        private ObservableRangeCollection<ItemsSubDto> _itemsSecondary;
        public ObservableRangeCollection<ItemsSubDto> ItemsSecondary
        {
            get => _itemsSecondary ?? (_itemsSecondary = new ObservableRangeCollection<ItemsSubDto>());
            set
            {
                _itemsSecondary = value;
                PropertyChanged(() => ItemsSecondary);
            }
        }

        #endregion

        #region Commands
        public ICommand SearchEntryTextChangedCommand => new Command((sender) =>
        {
            try
            {
                if (sender == null) return;

                _searchTime = 0.4;

                if (!_isTimerWorking)
                    SearchTimer();
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

                if (sender != null && sender is ItemCategory category)
                {
                    if (_selectedCategories.Any() &&
                        _selectedCategories.FirstOrDefault(x => x.IsMain).Id != category.ID)
                    {
                        var findCategory = Categories.FirstOrDefault(x => x.IsSelected);

                        if (findCategory != null)
                        {
                            findCategory.IsSelected = false;
                        }
                    }

                    category.IsSelected = !category.IsSelected;

                    if (category.IsSelected)
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
                    UpdateItemsList(
                        resultResponseData: null,
                        clearList: true
                    );

                    await GetItemsAndBindFromApi();
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
                    SubCategories.ForEach(x => x.IsSelected = false);

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
                    UpdateItemsList(
                        resultResponseData: null,
                        clearList: true
                    );

                    await GetItemsAndBindFromApi();
                }

            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            DoubleTapping.ResumeTap();
        });

        public ICommand BestsellersClickCommand => new Command(async (sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                if (sender is ItemsSubDto item)
                {
                    var navigationModel = new NavigationModel<ItemDetailPageViewParamModel>
                    {
                        Model = new ItemDetailPageViewParamModel { ItemId = item.Id },
                        ClosedPageEventCommand = ItemDetailClosedEventCommand
                    };

                    await NavigationService.NavigateToBackdropAsync<ItemDetailPageViewModel>(navigationModel);
                }

            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            DoubleTapping.ResumeTap();
        });

        public ICommand ItemDetailClosedEventCommand => new Command(async (sender) =>
        {
            try
            {
                if (!(sender is ItemDetailPageViewParamModel model)) return;

                if (!model.IsAddItem) return;

                if (ShowBestsellers)
                {
                    GetBestsellersAndBindFromApi();
                }
                else
                {
                    _listPage = 0;
                    _isAllItemLoad = false;
                    UpdateItemsList(
                        resultResponseData: null,
                        clearList: true
                    );

                    await GetItemsAndBindFromApi();
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });

        public ICommand ListItemsSelectionChangedCommand => new Command(async (sender) => ItemsListClick(sender));

        public ICommand ListItemsRemainingItemsThresholdReachedCommand => new Command(async (sender) => RemainingItemsThresholdReachedCommand(sender));

        public ICommand ChangeListDesignButtonClickCommand => new Command(() =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                var scrapReasonPageViewNavigationModel = new NavigationModel<ChangeItemListDesignPageViewParamModel>()
                {
                    Model = new ChangeItemListDesignPageViewParamModel(),
                    ClosedPageEventCommand = ChangeItemListDesignClosedEventCommand
                };

                NavigationService.NavigateToBackdropAsync<ChangeItemListDesignPageViewModel>(scrapReasonPageViewNavigationModel);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            DoubleTapping.ResumeTap();
        });

        public ICommand ChangeItemListDesignClosedEventCommand => new Command((sender) =>
        {
            try
            {
                if (!(sender is ChangeItemListDesignPageViewParamModel model)) return;

                if (model.ItemListType == _currentItemListType)
                    return;

                ChangeListTypeAndUpdateBindings(model.ItemListType);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });

        public ICommand FavoriteClickCommand => new Command(async (sender) =>
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                IsBusy = true;

                if (sender is ItemsSubDto item)
                {

                    var result = await _saveUserMobileFavoriteItemsService.SaveUserMobileFavoriteItems(
                        userId: GlobalSetting.Instance.User.ID,
                        itemId: item.Id,
                        isFavorite: !item.IsFavorite,
                        enPriorityType.UserInitiated
                    );

                    if (result.IsSuccess)
                    {
                        item.IsFavorite = !item.IsFavorite;
                    }
                    else
                    {
                        DialogService.WarningToastMessage("Ürün favorilere eklenemedi.");
                    }
                }
                
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                
                DialogService.WarningToastMessage("Bir hata oluştu.");
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

                    Search = string.Empty;

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

                    await GetItemsAndBindFromApi();
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
                    ChangeShowItemVisibility(isNotNullSearch);
                    ShowChangeItemListDesignButton = isNotNullSearch;

                    if (!string.IsNullOrWhiteSpace(Search))
                    {
                        Categories.Where(x => x.IsSelected).ForEach(x => x.IsSelected = false);
                        _selectedCategories.Clear();
                        ShowSubCategories = false;
                    }

                    if (!string.IsNullOrWhiteSpace(Search) || _selectedCategories.Any())
                    {
                        _listPage = 0;
                        UpdateItemsList(
                            resultResponseData: null,
                            clearList: true
                        );

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
                if (_isAllItemLoad)
                {
                    return;
                }

                var selectedCategoryStr = _selectedCategories
                    .OrderBy(x => x.IsMain)
                    .FirstOrDefault()?
                    .Id
                    .ToString();

                var result = await _itemsService.GetItems(
                    userId: GlobalSetting.Instance.User.ID,
                    filter: Search,
                    categoryIds: selectedCategoryStr,
                    page: _listPage,
                    priorityType: enPriorityType.UserInitiated
                );

                if (result?.ResponseData != null && result.IsSuccess)
                {
                    if (result.ResponseData.Count < GlobalSetting.Instance.ListPageSize)
                        _isAllItemLoad = true;

                    UpdateItemsList(
                        resultResponseData: result.ResponseData,
                        clearList: false
                    );

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
        }

        private void UpdateItemsList(List<ItemsSubDto> resultResponseData, bool clearList)
        {
            try
            {
                switch (_currentItemListType)
                {
                    case enItemListType.Primary:
                        if (clearList) ItemsPrimary.Clear();
                        else ItemsPrimary.AddRange(resultResponseData);
                        break;
                    case enItemListType.Secondary:
                        if (clearList) ItemsSecondary.Clear();
                        else ItemsSecondary.AddRange(resultResponseData);
                        break;
                    case enItemListType.Tertiary:
                        if (clearList) ItemsTertiary.Clear();
                        else ItemsTertiary.AddRange(resultResponseData);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
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
                        result.ResponseData.Insert(0, Constants.ItemCategoryAll);

                        Categories = new ObservableRangeCollection<ItemCategory>(result.ResponseData);
                        
                        foreach (var selectedCategory in _selectedCategories)
                        {
                            var category = Categories.FirstOrDefault(x => x.ID == selectedCategory.Id);

                            if (category != null)
                            {
                                category.IsSelected = true;
                            }
                        }
                    }
                    else
                    {
                        SubCategories = new ObservableRangeCollection<ItemCategory>(result.ResponseData);
                        ShowSubCategories = SubCategories.Any();
                    }
                }
                else
                {
                    DialogService.ErrorToastMessage("Kategorileri getirirken bir hata oluştu!");
                }
            }
            catch (Exception ex)
            {
                DialogService.ErrorToastMessage("Kategorileri getirirken bir hata oluştu!");

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
                ChangeShowItemVisibility(true);
                ShowChangeItemListDesignButton = true;
                ShowEmptyText = false;

                var currentList = GetCurrentItemsList();

                if (!currentList.Any())
                {
                    ShowEmptyText = true;
                    ChangeShowItemVisibility(false);
                    ShowChangeItemListDesignButton = false;
                }
            }
            else
            {
                if (!_selectedCategories.Any())
                {
                    ShowSubCategories = false;
                }

                ShowBestsellers = true;
                ChangeShowItemVisibility(false);
                ShowChangeItemListDesignButton = false;
                ShowEmptyText = false;
            }
        }

        private void ChangeListTypeAndUpdateBindings(enItemListType itemListType)
        {
            try
            {
                var currentItemsData = GetCurrentItemsList();

                switch (itemListType)
                {
                    case enItemListType.Primary:
                        ItemsPrimary = currentItemsData;
                        break;
                    case enItemListType.Secondary:
                        ItemsSecondary = currentItemsData;
                        break;
                    case enItemListType.Tertiary:
                        ItemsTertiary = currentItemsData;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                ShowItemsPrimary = itemListType == enItemListType.Primary;
                ShowItemsSecondary = itemListType == enItemListType.Secondary;
                ShowItemsTertiary = itemListType == enItemListType.Tertiary;

                _currentItemListType = itemListType;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        private void ChangeShowItemVisibility(bool value)
        {
            try
            {
                switch (_currentItemListType)
                {
                    case enItemListType.Primary:
                        ShowItemsPrimary = value;
                        break;
                    case enItemListType.Secondary:
                        ShowItemsSecondary = value;
                        break;
                    case enItemListType.Tertiary:
                        ShowItemsTertiary = value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        private async void ItemsListClick(object sender)
        {
            try
            {
                if (!DoubleTapping.AllowTap) return; DoubleTapping.AllowTap = false;

                var navigationModel = new NavigationModel<ItemDetailPageViewParamModel>
                {
                    Model = new ItemDetailPageViewParamModel
                    {
                        ItemId = SelectedItem.Id,
                    },
                    ClosedPageEventCommand = ItemDetailClosedEventCommand
                };

                await NavigationService.NavigateToBackdropAsync<ItemDetailPageViewModel>(navigationModel);
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }

            SelectedItem = null;
            DoubleTapping.ResumeTap();
        }

        private void RemainingItemsThresholdReachedCommand(object sender)
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
        }

        private ObservableRangeCollection<ItemsSubDto> GetCurrentItemsList()
        {
            try
            {
                ObservableRangeCollection<ItemsSubDto> currentItemsData;

                switch (_currentItemListType)
                {
                    case enItemListType.Primary:
                        currentItemsData = ItemsPrimary;
                        break;
                    case enItemListType.Secondary:
                        currentItemsData = ItemsSecondary;
                        break;
                    case enItemListType.Tertiary:
                        currentItemsData = ItemsTertiary;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return currentItemsData;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);

                return new ObservableRangeCollection<ItemsSubDto>();
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