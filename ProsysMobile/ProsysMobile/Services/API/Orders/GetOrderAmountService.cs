using System;
using System.Threading.Tasks;
using Polly;
using ProsysMobile.Endpoints.Orders;
using ProsysMobile.Handler;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Selector;
using Refit;

namespace ProsysMobile.Services.API.Orders
{
    public class GetOrderAmountService : IGetOrderAmountService
    {
        private readonly IApiRequest<IGetOrderAmountEndpoint> _request;
        private readonly IApiRequestSelector<IGetOrderAmountEndpoint> _apiRequestSelector;

        public GetOrderAmountService(IApiRequest<IGetOrderAmountEndpoint> request, IApiRequestSelector<IGetOrderAmountEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }

        public Task<ServiceBaseResponse<OrderAmountSubDto>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceBaseResponse<OrderAmountSubDto>> GetOrderAmount(int userId, enPriorityType priorityType)
        {
            ServiceBaseResponse<OrderAmountSubDto> result = null;
            Task<ServiceBaseResponse<OrderAmountSubDto>> task;
            Exception exception;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.GetOrderAmount(userId ,"Bearer " + GlobalSetting.Instance.JWTToken);
                
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