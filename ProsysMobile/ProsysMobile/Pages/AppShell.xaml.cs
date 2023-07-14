using ProsysMobile.Helper;
using ProsysMobile.Renderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : CustomShell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

        public AppShell()
        {
            InitializeComponent();
        }

        private void TabBar_PropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            try
            {
                var tabbar = sender as TabBar;

                MessagingCenter.Send<AppShell, string>(this, "AppShellTabIndexChange", tabbar.CurrentItem.TabIndex.ToString());
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }
    }
}