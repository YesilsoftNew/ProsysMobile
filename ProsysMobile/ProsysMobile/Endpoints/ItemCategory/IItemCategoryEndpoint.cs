using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;
using System.Threading.Tasks;

namespace ProsysMobile.Endpoints.UserMobile
{
    [Headers("Content-Type : application/json")]
    public interface IItemCategoryEndpoint
    {
        [Post("/api/ItemCategory/ItemCategory")]
        Task<ServiceBaseResponse<ItemCategory>> ItemCategory(long mainCategoryId);
    }
}