namespace ProsysMobile.Models.CommonModels.OtherModels
{
    public class ChangeItemCountCommandParameterModel
    {
        public int ItemId { get; set; }
        public int Count { get; set; }
        public bool IsDeleteItem { get; set; }
    }
}