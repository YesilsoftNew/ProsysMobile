namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class OrderDetailsSubDto
    {
        public int Id { get; set; }
        public int OrderDetailId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string StockCount { get; set; } = string.Empty;
        public int StockCountInt { get; set; } = 61;
        public string Amount { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string CurrencyType { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string UnitPrice { get; set; } = string.Empty;
    }
}