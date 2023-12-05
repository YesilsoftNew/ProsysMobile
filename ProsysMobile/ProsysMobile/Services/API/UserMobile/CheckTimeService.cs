using System;
using System.Threading.Tasks;
using Polly;
using ProsysMobile.Endpoints.UserMobile;
using ProsysMobile.Handler;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Selector;
using Refit;

namespace ProsysMobile.Services.API.UserMobile
{
    public class CheckTimeService : ICheckTimeService
    {
        private readonly IApiRequest<ICheckTimeEndpoint> _request;
        private readonly IApiRequestSelector<ICheckTimeEndpoint> _apiRequestSelector;

        public CheckTimeService(IApiRequest<ICheckTimeEndpoint> request, IApiRequestSelector<ICheckTimeEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }

        public Task<ServiceBaseResponse<CheckTimeResponseModel>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceBaseResponse<CheckTimeResponseModel>> CheckTime(int userId, DateTime loginDate, enPriorityType priorityType)
        {
            ServiceBaseResponse<CheckTimeResponseModel> result = null;
            Task<ServiceBaseResponse<CheckTimeResponseModel>> task;
            Exception exception;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.CheckTime(
                    userId: userId,
                    loginDate: loginDate,
                    authorization: "Bearer " + GlobalSetting.Instance.JWTToken
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