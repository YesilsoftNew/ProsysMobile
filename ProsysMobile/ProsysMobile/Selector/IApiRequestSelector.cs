using WiseMobile.Handler;
using WiseMobile.Models.CommonModels.Enums;
using WiseMobile.Services.Base;

namespace WiseMobile.Selector
{
    public interface IApiRequestSelector<T> : IMobileServiceBase
    {
        T GetApiRequestByPriority(IApiRequest<T> apiRequest, enPriorityType priorityType);
    }
}