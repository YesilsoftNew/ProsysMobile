using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.Orders
{
    [Headers("Content-Type : application/json")]
    public interface ISaveOrderEndpoint
    {
        [Post("/api/Orders/SaveOrder")]
        Task<ServiceBaseResponse<EmptyResponseModel>> SaveOrder(int orderId, string note, [Header("Authorization")] string authorization);
    }
}