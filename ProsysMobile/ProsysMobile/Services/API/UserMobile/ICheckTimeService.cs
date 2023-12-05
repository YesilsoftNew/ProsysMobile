using System;
using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.UserMobile
{
    public interface ICheckTimeService : IServiceBase<ServiceBaseResponse<CheckTimeResponseModel>>
    {
        Task<ServiceBaseResponse<CheckTimeResponseModel>> CheckTime(int userId, DateTime loginDate, enPriorityType priorityType);
    }
}