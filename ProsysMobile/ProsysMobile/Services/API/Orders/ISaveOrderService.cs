using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.Orders
{
    public interface ISaveOrderService : IServiceBase<ServiceBaseResponse<EmptyResponseModel>>
    {
        Task<ServiceBaseResponse<EmptyResponseModel>> SaveOrder(int orderId, enPriorityType priorityType);
    }
}