using System.Collections.Generic;
using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.Items
{
    public interface IBestsellersService : IServiceBase<ServiceBaseResponse<ItemsSubDto>>
    {
        Task<ServiceBaseResponse<List<ItemsSubDto>>> GetBestsellers(enPriorityType priorityType);
    }
}