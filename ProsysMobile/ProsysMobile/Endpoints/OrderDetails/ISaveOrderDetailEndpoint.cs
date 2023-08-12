using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.OrderDetails
{
    [Headers("Content-Type : application/json")]
    public interface ISaveOrderDetailEndpoint
    {
        [Post("/api/OrderDetails/SaveOrderDetail")]
        Task<ServiceBaseResponse<bool>> SaveOrderDetail(OrderDetailsParam orderDetailsParam, [Header("Authorization")] string authorization);
    }
}