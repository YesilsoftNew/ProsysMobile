using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.Items
{
    [Headers("Content-Type : application/json")]
    public interface ISaveUserMobileFavoriteItemsEndpoint
    {
        [Post("/api/Items/SaveUserMobileFavoriteItems")]
        Task<ServiceBaseResponse<EmptyResponseModel>> SaveUserMobileFavoriteItems(int userId, int itemId, bool isFavorite, [Header("Authorization")] string authorization);

    }
}