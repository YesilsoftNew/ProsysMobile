using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.UserMobile
{
    public interface IAuthAdminService : IServiceBase<ServiceBaseResponse<string>>
    {
        Task<ServiceBaseResponse<string>> AuthAdmin(enPriorityType priorityType);
    }
}