using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;
using System.Threading.Tasks;

namespace ProsysMobile.Services.API.ItemCategory
{
    public interface IItemCategoryService : IServiceBase<ServiceBaseResponse<Models.APIModels.ResponseModels.ItemCategory>>
    {
        Task<ServiceBaseResponse<Models.APIModels.ResponseModels.ItemCategory>> ItemCategory(long mainCategoryId, enPriorityType priorityType);
    }
}
