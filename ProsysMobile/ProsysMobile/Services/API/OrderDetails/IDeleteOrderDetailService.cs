using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.OrderDetails
{
    public interface IDeleteOrderDetailService : IServiceBase<ServiceBaseResponse<DeleteOrderDetailResponseModel>>
    {
        Task<ServiceBaseResponse<DeleteOrderDetailResponseModel>> DeleteOrderDetail(int orderDetailId, enPriorityType priorityType);
    }
}