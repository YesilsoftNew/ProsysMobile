using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.OrderDetails
{
    public interface ISaveOrderDetailService : IServiceBase<ServiceBaseResponse<bool>>
    {
        Task<ServiceBaseResponse<bool>> SaveOrderDetail(OrderDetailsParam orderDetailsParam, enPriorityType priorityType);
    }
}