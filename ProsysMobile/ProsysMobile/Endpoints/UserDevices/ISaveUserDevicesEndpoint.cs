using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.UserDevices
{
    public interface ISaveUserDevicesEndpoint
    {
        [Post("/api/UserDevices/SaveUserDevices")]
        Task<ServiceBaseResponse<Models.APIModels.ResponseModels.UserDevices>> SaveUserDevices(Models.APIModels.ResponseModels.UserDevices userDevice, [Header("Authorization")] string authorization);
    }
}