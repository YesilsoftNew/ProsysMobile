using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;
using System.Threading.Tasks;

namespace ProsysMobile.Services.API.UserMobile
{
    public interface ISignInService : IServiceBase<ServiceBaseResponse<AuthenticationResponseModel>>
    {
        Task<ServiceBaseResponse<AuthenticationResponseModel>> SignIn(SignIn signIn, enPriorityType priorityType);
    }
}
