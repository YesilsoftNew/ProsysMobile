using System;
using System.Threading.Tasks;
using Polly;
using ProsysMobile.Endpoints.Orders;
using ProsysMobile.Handler;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Resources.Language;
using ProsysMobile.Selector;
using Refit;

namespace ProsysMobile.Services.API.Orders
{
    public class SaveOrderService : ISaveOrderService
    {
        private readonly IApiRequest<ISaveOrderEndpoint> _request;
        private readonly IApiRequestSelector<ISaveOrderEndpoint> _apiRequestSelector;

        public SaveOrderService(IApiRequest<ISaveOrderEndpoint> request, IApiRequestSelector<ISaveOrderEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }

        public Task<ServiceBaseResponse<EmptyResponseModel>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceBaseResponse<EmptyResponseModel>> SaveOrder(int orderId, int userId, DateTime processDate, string note, enPriorityType priorityType)
        {
            ServiceBaseResponse<EmptyResponseModel> result = null;
            Task<ServiceBaseResponse<EmptyResponseModel>> task;
            Exception exception;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.SaveOrder(orderId, userId, processDate, note, Resource.Language , "Bearer " + GlobalSetting.Instance.JWTToken);
                
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