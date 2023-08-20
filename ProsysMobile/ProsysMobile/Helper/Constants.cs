using ProsysMobile.Models.APIModels.ResponseModels;

namespace ProsysMobile.Helper
{
    public static class Constants
    {
        public static int MainCategoryId = -1;
        public static ItemCategory ItemCategoryAll = new ItemCategory
        {
            ID = 0,
            CategoryDesc = "All",
            MainCategoryId = 0,
            Image = "http://yas.yesilsoft.net/Images/AllCategory.png",
            RecordState = null,
            IsSelected = false
        };
    }
}