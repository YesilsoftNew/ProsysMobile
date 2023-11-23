using System.Collections.Generic;
using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using Refit;

namespace ProsysMobile.Endpoints.Items
{
    [Headers("Content-Type : application/json")]
    public interface IGetHomePageDataEndpoint
    {
        [Get("/api/Items/GetHomePageData")]
        Task<ServiceBaseResponse<HomePageDataSubDto>> GetHomePageData(
            int page,
            int userId,
            int mainCategoryId,
            string culture,
            [Header("Authorization")] string authorization
        );
    }
}