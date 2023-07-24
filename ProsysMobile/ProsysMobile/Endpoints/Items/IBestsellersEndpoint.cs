using System.Collections.Generic;
using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.Items
{
    [Headers("Content-Type : application/json")]
    public interface IBestsellersEndpoint
    {
        [Get("/api/Items/BestSellers")]
        Task<ServiceBaseResponse<List<ItemsSubDto>>> GetBestsellers([Header("Authorization")] string authorization);
    }
}