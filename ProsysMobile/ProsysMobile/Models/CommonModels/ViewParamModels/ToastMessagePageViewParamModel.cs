using ProsysMobile.Models.CommonModels.Enums;
using WiseMobile.Models.CommonModels.Enums;

namespace ProsysMobile.Models.CommonModels.ViewParamModels
{
    public class ToastMessagePageViewParamModel
    {
        public string MessageText { get; set; }
        public enToastMessageType ToastMessageType { get; set; }
    }
}