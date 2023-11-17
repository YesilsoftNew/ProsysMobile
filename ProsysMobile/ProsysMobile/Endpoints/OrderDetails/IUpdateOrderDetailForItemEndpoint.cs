using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.OrderDetails
{
    [Headers("Content-Type : application/json")]
    public interface IUpdateOrderDetailForItemEndpoint
    {
        [Post("/api/OrderDetails/UpdateOrderDetailForItem")]
        Task<ServiceBaseResponse<UpdateOrderDetailForItemResponseModel>> UpdateOrderDetailForItem(OrderDetailsParam orderDetailsParam, [Header("Authorization")] string authorization);
    }
}