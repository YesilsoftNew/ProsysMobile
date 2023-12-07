using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Resources.Language;

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
            Image = "http://common.yas.yesilsoft.com/Categories/AllCategory.png",
            RecordState = null,
            IsSelected = false
        };

        public static string UnSelectedFavoriteImageSource = "UnSelectedFavorite";
        public static string SelectedFavoriteImageSource = "SelectedFavorite";
        public static string ReplaceWord = "@xxx";
    }
}