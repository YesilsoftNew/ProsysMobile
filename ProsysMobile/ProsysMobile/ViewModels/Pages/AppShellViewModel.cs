using System.Threading.Tasks;
using ProsysMobile.ViewModels.Base;
using ProsysMobile.ViewModels.Pages.Item;
using ProsysMobile.ViewModels.Pages.Main;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Pages
{
    public class AppShellViewModel : ViewModelBase
    {
        public override Task InitializeAsync(object navigationData)
        {
            MessagingCenter.Subscribe<ItemDetailPageViewModel, int>(this, "UpdateBasketCount", (sender, arg) =>
            {
                BasketCount = arg <= 0 ? string.Empty : arg.ToString();
            });
            
            MessagingCenter.Subscribe<OrderPageViewModel, int>(this, "UpdateBasketCount", (sender, arg) =>
            {
                BasketCount = arg <= 0 ? string.Empty : arg.ToString();
            });
            
            MessagingCenter.Subscribe<HomePageViewModel, int>(this, "UpdateBasketCount", (sender, arg) =>
            {
                BasketCount = arg <= 0 ? string.Empty : arg.ToString();
            });

            return base.InitializeAsync(navigationData);
        }

        #region Propertys
        
        private string _basketCount;
        public string BasketCount { get => _basketCount; set { _basketCount = value; PropertyChanged(() => BasketCount); } }
        
        #endregion
    }
}