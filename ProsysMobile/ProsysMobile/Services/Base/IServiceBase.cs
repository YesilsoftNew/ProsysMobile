using System.Threading.Tasks;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;

namespace ProsysMobile.Services.Base
{
    public interface IServiceBase<T> : IApiServiceBase
    {
        Task<T> Get(ApiFilterRequestModel apiFilterRequestModel);
    }
}