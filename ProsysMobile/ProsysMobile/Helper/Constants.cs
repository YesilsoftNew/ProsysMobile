using System;
using Plugin.FirebasePushNotification;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Resources.Language;
using Xamarin.Essentials;

namespace ProsysMobile.Helper
{
    public static class Constants
    {
        public static int MainCategoryId = -1;
        
        public static ItemCategory ItemCategoryAll = new ItemCategory
        {
            ID = 0,
            CategoryDesc = Resource.All,
            MainCategoryId = 0,
            Image = "http://yas.yesilsoft.net/Images/AllCategory.png",
            RecordState = null,
            IsSelected = false
        };

        public static string UnSelectedFavoriteImageSource = "UnSelectedFavorite";
        public static string SelectedFavoriteImageSource = "SelectedFavorite";
        
    }
}