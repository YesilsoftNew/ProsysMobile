using Refit;
using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;

namespace ProsysMobile.Endpoints.Auth
{
    [Headers("Content-Type : application/json")]
    public interface IAuthEndpoint
    {
        [Post("/User/Authentication")]
        Task<ServiceBaseResponse<UserAuthResponseModel>> UserAuthentication(UserAuthRequestModel userReqModel);
    }
}