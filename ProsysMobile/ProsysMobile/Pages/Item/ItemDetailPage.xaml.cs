using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.Pages.Item
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            Shell.SetTabBarIsVisible(this, true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            Shell.SetTabBarIsVisible(this, false);
        }
    }
}