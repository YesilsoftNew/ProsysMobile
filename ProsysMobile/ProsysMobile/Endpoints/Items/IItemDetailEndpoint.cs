using System.Collections.Generic;
using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.Items
{
    [Headers("Content-Type : application/json")]
    public interface IItemDetailEndpoint
    {
        [Get("/api/Items/ItemDetail")]
        Task<ServiceBaseResponse<ItemDetailsSubDto>> GetItemDetail(int id, int userId, string culture, [Header("Authorization")] string authorization);
    }
}