using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.UserMobile
{
    [Headers("Content-Type : application/json")]
    public interface IAuthAdminEndpoint
    {
        [Post("/api/UserMobile/AuthAdmin")]
        Task<ServiceBaseResponse<string>> AuthAdmin(string userName, string password);
    }
}