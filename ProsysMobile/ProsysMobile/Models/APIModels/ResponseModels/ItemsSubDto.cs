namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class ItemsSubDto
    {
        public int Id { get; set;}
        public int CategoryId { get; set;}
        public string Name { get; set; } = string.Empty;
        public string Count { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string CurrencyType { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
