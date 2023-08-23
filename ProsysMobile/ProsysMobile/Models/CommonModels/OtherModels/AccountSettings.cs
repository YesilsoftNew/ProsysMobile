using ProsysMobile.Models.CommonModels.Enums;

namespace ProsysMobile.Models.CommonModels.OtherModels
{
    public class AccountSettings
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public enAccountSettingsType AccountSettingsType { get; set; }
    }
}