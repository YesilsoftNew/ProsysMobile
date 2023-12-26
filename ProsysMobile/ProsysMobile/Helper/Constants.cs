using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Resources.Language;

namespace ProsysMobile.Helper
{
    public static class Constants
    {
        public const string ADMIN_USERNAME = "YU_L+%@GB&`DCU*<N21ua1reW~R-x5v>oSfZT0(JE9'!I})KMPA{#3=14;.7H:?Q628^n*-3GBSPOa";
        public const string ADMIN_PASSWORD = "YP_Jd$nF!U5yas61in_mn,p*g_?u8900a3W<vAc^2005zbrMK8Eu00a37a)I6qOG61./NA4Mk{^&";
        
        public static int MainCategoryId = -1;
        
        public static ItemCategory ItemCategoryAll = new ItemCategory
        {
            ID = 0,
            CategoryDesc = Resource.All,
            MainCategoryId = 0,
            Image = "http://common.yas.yesilsoft.com/Categories/AllCategory.png",
            RecordState = null,
            IsSelected = false
        };

        public static string UnSelectedFavoriteImageSource = "UnSelectedFavorite";
        public static string SelectedFavoriteImageSource = "SelectedFavorite";
        public static string ReplaceWord = "@xxx";
    }
}