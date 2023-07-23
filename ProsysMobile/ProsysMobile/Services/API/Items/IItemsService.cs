using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.Items
{
    public interface IItemsService : IServiceBase<ServiceBaseResponse<ItemsSubDto>>
    {
        Task<ServiceBaseResponse<ItemsSubDto>> GetItems(string filter, string categoryIds, int page, enPriorityType priorityType);
    }
}