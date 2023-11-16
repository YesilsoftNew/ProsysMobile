namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class UpdateOrderDetailForItemResponseModel
    {
        public string ItemStockCount { get; set; } = string.Empty;
        public int ItemStockCountInt { get; set; }
        public string ItemPrice { get; set; } = string.Empty;
        public string NetTotal { get; set; } = string.Empty;
    }
}