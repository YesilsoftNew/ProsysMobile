using System;
using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.UserMobile
{
    [Headers("Content-Type : application/json")]
    public interface ICheckTimeEndpoint
    {
        [Post("/api/UserMobile/CheckTime")]
        Task<ServiceBaseResponse<CheckTimeResponseModel>> CheckTime(int userId, DateTime loginDate, [Header("Authorization")] string authorization);
    }
}