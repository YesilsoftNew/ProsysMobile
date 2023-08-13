using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.Orders
{
    [Headers("Content-Type : application/json")]
    public interface IGetOrderAmountEndpoint
    {
        [Get("/api/Orders/GetOrderAmount")]
        Task<ServiceBaseResponse<OrderAmountSubDto>> GetOrderAmount(int userId, [Header("Authorization")] string authorization);
    }
}