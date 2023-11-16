using System;
using System.Threading.Tasks;
using Polly;
using ProsysMobile.Endpoints.OrderDetails;
using ProsysMobile.Handler;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Selector;
using Refit;

namespace ProsysMobile.Services.API.OrderDetails
{
    public class UpdateOrderDetailForItemService : IUpdateOrderDetailForItemService
    {
        private readonly IApiRequest<IUpdateOrderDetailForItemEndpoint> _request;
        private readonly IApiRequestSelector<IUpdateOrderDetailForItemEndpoint> _apiRequestSelector;

        public UpdateOrderDetailForItemService(IApiRequest<IUpdateOrderDetailForItemEndpoint> request, IApiRequestSelector<IUpdateOrderDetailForItemEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }

        public Task<ServiceBaseResponse<UpdateOrderDetailForItemResponseModel>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceBaseResponse<UpdateOrderDetailForItemResponseModel>> UpdateOrderDetailForItem(OrderDetailsParam orderDetailsParam, enPriorityType priorityType)
        {
            ServiceBaseResponse<UpdateOrderDetailForItemResponseModel> result = null;
            Task<ServiceBaseResponse<UpdateOrderDetailForItemResponseModel>> task;
            Exception exception;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.UpdateOrderDetailForItem(
                    orderDetailsParam,
                    "Bearer " + GlobalSetting.Instance.JWTToken
                );
                result = await Policy
                    .Handle<ApiException>()
                    .WaitAndRetryAsync(retryCount: 2, sleepDurationProvider: retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                    .ExecuteAsync(async () => await task);
            }
            catch (ApiException apiException)
            {
                exception = apiException;
                ProsysLogger.Instance.CrashLog(exception);
            }
            catch (Exception ex)
            {
                exception = ex;
                ProsysLogger.Instance.CrashLog(exception);
            }

            return result;
        }
    }
}