using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.UserMobile
{
    [Headers("Content-Type : application/json")]
    public interface IUserDeleteEndpoint
    {
        [Post("/api/UserMobile/UserDelete")]
        Task<ServiceBaseResponse<EmptyResponseModel>> UserDelete(int userId, [Header("Authorization")] string authorization);
    }
}