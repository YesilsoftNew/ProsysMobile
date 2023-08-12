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
        Task<ServiceBaseResponse<List<OrderDetailsSubDto>>> GetOrderDetail(int userId, [Header("Authorization")] string authorization);
    }
}