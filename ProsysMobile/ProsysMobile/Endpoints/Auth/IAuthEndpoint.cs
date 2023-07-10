using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;
using System.Threading.Tasks;

namespace ProsysMobile.Endpoints.Auth
{
    [Headers("Content-Type : application/json")]
    public interface IAuthEndpoint
    {
        [Post("/api/UserMobile/SignIn")]
        Task<ServiceBaseResponse<AuthenticationResponseModel>> SignIn(SignIn signIn);
    }
}