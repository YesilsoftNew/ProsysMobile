using ProsysMobile.Helper;
using System;
using WiseDynamicMobile.Helper;

namespace ProsysMobile.Models.APIModels.CommonModels
{
    public class PostIdDto
    {
        public string DeviceGuid { get; set; } = GlobalSetting.Instance.DeviceGuid;
        public string PackageNo { get; set; } = TOOLS.GenerateUniqueString();
        //TODO : MODELS - USER
        //public long UserId { get; set; } = GlobalSetting.Instance.User.Id;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
