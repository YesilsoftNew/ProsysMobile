using System.Collections.Generic;
using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.Items
{
    [Headers("Content-Type : application/json")]
    public interface IItemsEndpoint
    {
        [Get("/api/Items/Items")]
        Task<ServiceBaseResponse<ItemsSubDto>> GetItems(string filter, string categoryIds, int page, [Header("Authorization")] string authorization);
    }
}