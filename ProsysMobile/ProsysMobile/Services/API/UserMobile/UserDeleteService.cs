using Polly;
using Refit;
using System;
using System.Threading.Tasks;
using ProsysMobile.Endpoints.UserMobile;
using ProsysMobile.Handler;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Selector;

namespace ProsysMobile.Services.API.UserMobile
{
    public class UserDeleteService : IUserDeleteService
    {
        private readonly IApiRequest<IUserDeleteEndpoint> _request;
        private readonly IApiRequestSelector<IUserDeleteEndpoint> _apiRequestSelector;

        public UserDeleteService(IApiRequest<IUserDeleteEndpoint> request, IApiRequestSelector<IUserDeleteEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }


        public Task<ServiceBaseResponse<EmptyResponseModel>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceBaseResponse<EmptyResponseModel>> UserDelete(int userId, enPriorityType priorityType)
        {
            ServiceBaseResponse<EmptyResponseModel> result = null;
            Task<ServiceBaseResponse<EmptyResponseModel>> task;
            Exception exception;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.UserDelete(
                    userId: userId,
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
