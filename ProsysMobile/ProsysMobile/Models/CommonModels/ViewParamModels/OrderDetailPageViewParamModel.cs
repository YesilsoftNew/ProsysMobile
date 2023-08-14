using MvvmHelpers;
using ProsysMobile.Models.APIModels.ResponseModels;

namespace ProsysMobile.Models.CommonModels.ViewParamModels
{
    public class OrderDetailPageViewParamModel
    {
        public ObservableRangeCollection<OrderDetailsSubDto> BasketItems { get; set; }
        public bool IsSaveBasket { get; set; }
    }
}