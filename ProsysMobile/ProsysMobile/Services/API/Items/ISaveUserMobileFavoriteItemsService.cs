using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.Items
{
    public interface ISaveUserMobileFavoriteItemsService : IServiceBase<ServiceBaseResponse<EmptyResponseModel>>
    {
        Task<ServiceBaseResponse<EmptyResponseModel>> SaveUserMobileFavoriteItems(int userId, int itemId, bool isFavorite, enPriorityType priorityType);
    }
}