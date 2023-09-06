﻿using Plugin.FirebasePushNotification;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProsysMobile.Pages.System
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            CrossFirebasePushNotification.Current.OnNotificationReceived += (source, args) =>
            {
                DisplayAlert("Notification", $"Data: {args.Data["myData"]}", "OK");
            };
        }
    }
}