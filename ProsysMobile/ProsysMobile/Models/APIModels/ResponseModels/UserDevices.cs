using System;
using ProsysMobile.Models.CommonModels.SQLiteModels.Base;

namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class UserDevices: Entity
    {
        public int? UserId { get; set; } 
        public int? LastLoginUserId { get; set; } 
        public DateTime? LastLoginDateTime { get; set; } 
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public string AppVersion { get; set; } = string.Empty;
        public string PushToken { get; set; } = string.Empty;
        public string Timezone { get; set; } = string.Empty;
        public DateTime? RecordDate { get; set; } 
    }
}