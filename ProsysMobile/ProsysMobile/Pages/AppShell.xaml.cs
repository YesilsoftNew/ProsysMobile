using ProsysMobile.Helper;
using ProsysMobile.Renderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using ProsysMobile.ViewModels.Pages.Main;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : CustomShell
    {
        public int count = 0;
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

        public AppShell()
        {
            InitializeComponent();
            
            MessagingCenter.Subscribe<HomePageViewModel, string>(this, "OpenFindPageForMainPageClickCategory", (sender, arg) =>
            {
                try
                {
                    Tabbar.CurrentItem = Tabbar.Items[1];
                }
                catch (Exception ex)
                {
                    ProsysLogger.Instance.CrashLog(ex);
                }
            });
        }

        private void TabBar_PropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            try
            {
                var tabbar = sender as TabBar;
                
                if (e.PropertyName == "CurrentItem")
                {
                    MessagingCenter.Send<AppShell, string>(this, "AppShellTabIndexChange", tabbar.CurrentItem.TabIndex.ToString());
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }
    }
}