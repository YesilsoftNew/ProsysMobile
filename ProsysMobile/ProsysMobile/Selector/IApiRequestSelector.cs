using ProsysMobile.Handler;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Services.Base;

namespace ProsysMobile.Selector
{
    public interface IApiRequestSelector<T> : IMobileServiceBase
    {
        T GetApiRequestByPriority(IApiRequest<T> apiRequest, enPriorityType priorityType);
    }
}