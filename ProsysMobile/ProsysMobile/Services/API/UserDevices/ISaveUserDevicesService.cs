using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.UserDevices
{
    public interface ISaveUserDevicesService : IServiceBase<ServiceBaseResponse<Models.APIModels.ResponseModels.UserDevices>>
    {
        Task<ServiceBaseResponse<Models.APIModels.ResponseModels.UserDevices>> SaveUserDevices(Models.APIModels.ResponseModels.UserDevices userDevice, enPriorityType priorityType);
    }
}