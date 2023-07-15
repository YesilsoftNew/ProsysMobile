using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.ViewModels.Base;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages.Order
{
    public class OrderDetailViewModel: ViewModelBase
    {
        public OrderDetailViewModel()
        {
            
        }
        
        public override Task InitializeAsync(object navigationData)
        {
            if (Debugger.IsAttached)
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
            }

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
        #endregion

        #region Commands

        public ICommand RegisterClickCommand => new Command(async () =>
        {
            try
            {
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        });
        #endregion
    }
}