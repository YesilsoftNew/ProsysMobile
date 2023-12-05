namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class CheckTimeResponseModel
    {
        public bool IsContinue { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
    }
}