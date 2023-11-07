using ProsysMobile.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.CustomControls.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomEntryPage
    {
        public CustomEntryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (Device.RuntimePlatform == Device.iOS && Content is ContentView _contentView)
                TOOLS.LoopContentPage(_contentView.Content, Content);
            
            base.OnAppearing();
        }
    }
}