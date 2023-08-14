namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class OrderAmountSubDto
    {
        public int OrderId { get; set; }
        public string GrossTotal { get; set; } = string.Empty;
        public string VatTotal { get; set; } = string.Empty;
        public string NetTotal { get; set; } = string.Empty;
    }
}