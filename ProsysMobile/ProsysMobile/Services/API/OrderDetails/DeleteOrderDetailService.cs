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

        public Task<ServiceBaseResponse<DeleteOrderDetailResponseModel>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceBaseResponse<DeleteOrderDetailResponseModel>> DeleteOrderDetail(int orderDetailId, int userId, DateTime processDate, enPriorityType priorityType)
        {
            ServiceBaseResponse<DeleteOrderDetailResponseModel> result = null;
            Task<ServiceBaseResponse<DeleteOrderDetailResponseModel>> task;
            Exception exception;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.DeleteOrderDetail(orderDetailId, userId, processDate, "Bearer " + GlobalSetting.Instance.JWTToken);
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