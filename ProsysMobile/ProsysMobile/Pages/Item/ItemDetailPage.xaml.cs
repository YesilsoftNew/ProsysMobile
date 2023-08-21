using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProsysMobile.Helper;
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
        
        private async void OnEntryFocused(object sender, FocusEventArgs e)
        {
            if (Device.RuntimePlatform != Device.iOS) return;
            
            try
            {
                var keyboardHeight = 240;
                Content.TranslationY = -keyboardHeight; // Adjust this value as needed
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }
        
        private void OnEntryUnfocused(object sender, FocusEventArgs e)
        {
            if (Device.RuntimePlatform != Device.iOS) return;
            
            try
            {
                Content.TranslationY = 0;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}