using System.Collections.Generic;

namespace ProsysMobile.Models.APIModels.ResponseModels
{
    public class OrderSubDto
    {
        public List<OrderDetailsSubDto> OrderDetailsSubDtos { get; set; }
        public string OrderNo { get; set; } = string.Empty;
        public string NetTotal { get; set; } = string.Empty;
    }
}