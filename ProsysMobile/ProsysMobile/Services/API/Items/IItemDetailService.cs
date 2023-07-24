using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.Items
{
    public interface IItemDetailService : IServiceBase<ServiceBaseResponse<ItemDetailsSubDto>>
    {
        Task<ServiceBaseResponse<ItemDetailsSubDto>> GetDetail(int itemId, enPriorityType priorityType);
    }
}