using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.Orders
{
    public interface IGetOrderAmountService : IServiceBase<ServiceBaseResponse<OrderAmountSubDto>>
    {
        Task<ServiceBaseResponse<OrderAmountSubDto>> GetOrderAmount(int userId, enPriorityType priorityType);
    }
}