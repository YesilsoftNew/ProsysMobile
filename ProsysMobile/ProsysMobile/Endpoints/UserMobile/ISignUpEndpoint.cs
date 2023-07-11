using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;
using System.Threading.Tasks;

namespace ProsysMobile.Endpoints.UserMobile
{
    [Headers("Content-Type : application/json")]
    public interface ISignUpEndpoint
    {
        [Post("/api/UserMobile/SignUp")]
        Task<ServiceBaseResponse<UserMobileDto>> SignUp(UserMobileDto userMobileDto);
    }
}