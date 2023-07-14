using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProsysMobile.Endpoints.UserMobile
{
    [Headers("Content-Type : application/json")]
    public interface IItemCategoryEndpoint
    {
        [Get("/api/ItemCategory/ItemCategory")]
        Task<ServiceBaseResponse<List<ItemCategory>>> ItemCategory(long mainCategoryId, [Header("Authorization")] string authorization);
    }
}