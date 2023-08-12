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
    public class SaveOrderDetailService : ISaveOrderDetailService
    {
        private readonly IApiRequest<ISaveOrderDetailEndpoint> _request;
        private readonly IApiRequestSelector<ISaveOrderDetailEndpoint> _apiRequestSelector;

        public SaveOrderDetailService(IApiRequest<ISaveOrderDetailEndpoint> request, IApiRequestSelector<ISaveOrderDetailEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }
        
        public Task<ServiceBaseResponse<bool>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceBaseResponse<bool>> SaveOrderDetail(OrderDetailsParam orderDetailsParam, enPriorityType priorityType)
        {
            ServiceBaseResponse<bool> result = null;
            Task<ServiceBaseResponse<bool>> task;
            Exception exception;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.SaveOrderDetail(orderDetailsParam, "Bearer " + GlobalSetting.Instance.JWTToken);
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