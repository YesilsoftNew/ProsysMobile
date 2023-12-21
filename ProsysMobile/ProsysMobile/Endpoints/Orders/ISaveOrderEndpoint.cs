using System;
using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.Orders
{
    [Headers("Content-Type : application/json")]
    public interface ISaveOrderEndpoint
    {
        [Post("/api/Orders/SaveOrder")]
        Task<ServiceBaseResponse<EmptyResponseModel>> SaveOrder(int orderId, int userId, DateTime processDate, string note, [Header("Authorization")] string authorization);
    }
}