using System.Threading.Tasks;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Services.API.Items
{
    public interface IGetHomePageDataService : IServiceBase<ServiceBaseResponse<HomePageDataSubDto>>
    {
        Task<ServiceBaseResponse<HomePageDataSubDto>> GetHomePageData(
            int page,
            int userId,
            int mainCategoryId,
            enPriorityType priorityType);
    }
}