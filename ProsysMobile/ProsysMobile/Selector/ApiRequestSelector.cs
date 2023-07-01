using WiseMobile.Handler;
using WiseMobile.Models.CommonModels.Enums;

namespace WiseMobile.Selector
{
    public class ApiRequestSelector<T> : IApiRequestSelector<T>
    {
        public T GetApiRequestByPriority(IApiRequest<T> apiRequest, enPriorityType priorityType)
        {
            switch (priorityType)
            {
                case enPriorityType.Speculative:
                    return apiRequest.Speculative;
                case enPriorityType.UserInitiated:
                    return apiRequest.UserInitiated;
                case enPriorityType.Background:
                    return apiRequest.Background;
                default:
                    return apiRequest.UserInitiated;
            }
        }
    }
}