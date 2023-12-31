using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Polly;
using ProsysMobile.Endpoints.OrderDetails;
using ProsysMobile.Handler;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Resources.Language;
using ProsysMobile.Selector;
using Refit;

namespace ProsysMobile.Services.API.OrderDetails
{
    public class GetOrderDetailService : IGetOrderDetailService
    {
        private readonly IApiRequest<IGetOrderDetailEndpoint> _request;
        private readonly IApiRequestSelector<IGetOrderDetailEndpoint> _apiRequestSelector;
        
        public GetOrderDetailService(IApiRequest<IGetOrderDetailEndpoint> request, IApiRequestSelector<IGetOrderDetailEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }
        
        public Task<ServiceBaseResponse<OrderSubDto>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceBaseResponse<OrderSubDto>> GetOrderDetail(int userId, enPriorityType priorityType)
        {
            ServiceBaseResponse<OrderSubDto> result = null;
            Task<ServiceBaseResponse<OrderSubDto>> task;
            Exception exception;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.GetOrderDetail(userId, Resource.Language, "Bearer " + GlobalSetting.Instance.JWTToken);
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