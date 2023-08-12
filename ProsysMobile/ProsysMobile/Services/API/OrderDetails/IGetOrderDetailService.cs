using System.Collections.Generic;
using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.OrderDetails
{
    public interface IGetOrderDetailService : IServiceBase<ServiceBaseResponse<OrderDetailsSubDto>>
    {
        Task<ServiceBaseResponse<List<OrderDetailsSubDto>>> GetOrderDetail(int userId, enPriorityType priorityType);
    }
}