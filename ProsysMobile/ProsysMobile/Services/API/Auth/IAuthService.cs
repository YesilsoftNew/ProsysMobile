using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;
using System.Threading.Tasks;

namespace ProsysMobile.Services.API.Auth
{
    public interface IAuthService : IServiceBase<ServiceBaseResponse<AuthenticationResponseModel>>
    {
        Task<ServiceBaseResponse<AuthenticationResponseModel>> UserAuthentication(SignIn signIn, enPriorityType priorityType);
    }
}
