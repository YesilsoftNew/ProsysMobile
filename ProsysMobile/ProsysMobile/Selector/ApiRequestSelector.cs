using ProsysMobile.Handler;
using ProsysMobile.Models.CommonModels.Enums;

namespace ProsysMobile.Selector
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