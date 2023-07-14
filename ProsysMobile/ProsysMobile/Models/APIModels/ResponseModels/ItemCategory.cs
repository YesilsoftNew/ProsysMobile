using ProsysMobile.Models.CommonModels.SQLiteModels.Base;

namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class ItemCategory : Entity
    {
        public string CategoryDesc { get; set; }
        public int MainCategoryId { get; set; }
        public string Image { get; set; }
        public bool RecordState { get; set; }
    }
}
