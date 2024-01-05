using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;
using System.Threading.Tasks;

namespace ProsysMobile.Services.API.UserMobile
{
    public interface ISignUpService : IServiceBase<ServiceBaseResponse<UserMobileDto>>
    {
        Task<ServiceBaseResponse<UserMobileDto>> SignUp(UserMobileDto userMobileDto, string token, enPriorityType priorityType);
    }
}
