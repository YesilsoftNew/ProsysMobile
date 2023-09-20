using System.Collections.Generic;
using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.Items
{
    [Headers("Content-Type : application/json")]
    public interface IDealItemsEndpoint
    {
        [Get("/api/Items/DealItems")]
        Task<ServiceBaseResponse<List<ItemsSubDto>>> DealItems(int userId, int page, [Header("Authorization")] string authorization);
    }
}