using System.Collections.Generic;

namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class HomePageDataSubDto
    {
        public List<ItemsSubDto> DealItemsSubDtos { get; set; }
        public List<ItemCategory> ItemCategorySubDtos { get; set; }
        public ChangeBasketItemCountResponseModel BasketItemCountSubDto { get; set; }
    }
}