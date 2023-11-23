using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.OrderDetails
{
    [Headers("Content-Type : application/json")]
    public interface IDeleteOrderDetailEndpoint
    {
        [Post("/api/OrderDetails/DeleteOrderDetail")]
        Task<ServiceBaseResponse<ChangeBasketItemCountResponseModel>> DeleteOrderDetail(int orderDetailId, [Header("Authorization")] string authorization);
    }
}