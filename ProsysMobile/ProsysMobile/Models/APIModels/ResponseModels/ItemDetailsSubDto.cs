namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class ItemDetailsSubDto
    {
        public ItemsSubDto Item { get; set; } = new ItemsSubDto();
        public string Categories { get; set; } = string.Empty;
    }
}
