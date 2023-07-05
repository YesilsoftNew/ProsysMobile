using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.Auth
{
    public interface IAuthService : IServiceBase<ServiceBaseResponse<UserAuthResponseModel>>
    {
        Task<ServiceBaseResponse<UserAuthResponseModel>> UserAuthentication(UserAuthRequestModel userAuthRequestModel, enPriorityType priorityType);

    }
}
