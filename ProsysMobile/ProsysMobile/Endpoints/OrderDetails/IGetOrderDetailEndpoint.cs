using System.Collections.Generic;
using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.OrderDetails
{
    [Headers("Content-Type : application/json")]
    public interface IGetOrderDetailEndpoint
    {
        [Get("/api/OrderDetails/GetOrderDetail")]
        Task<ServiceBaseResponse<OrderSubDto>> GetOrderDetail(int userId, string culture, [Header("Authorization")] string authorization);
    }
}