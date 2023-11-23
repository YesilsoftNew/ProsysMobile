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
    public class DeleteOrderDetailService : IDeleteOrderDetailService
    {
        private readonly IApiRequest<IDeleteOrderDetailEndpoint> _request;
        private readonly IApiRequestSelector<IDeleteOrderDetailEndpoint> _apiRequestSelector;

        public DeleteOrderDetailService(IApiRequest<IDeleteOrderDetailEndpoint> request, IApiRequestSelector<IDeleteOrderDetailEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }

        public Task<ServiceBaseResponse<ChangeBasketItemCountResponseModel>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceBaseResponse<ChangeBasketItemCountResponseModel>> DeleteOrderDetail(int orderDetailId, enPriorityType priorityType)
        {
            ServiceBaseResponse<ChangeBasketItemCountResponseModel> result = null;
            Task<ServiceBaseResponse<ChangeBasketItemCountResponseModel>> task;
            Exception exception;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.DeleteOrderDetail(orderDetailId, "Bearer " + GlobalSetting.Instance.JWTToken);
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