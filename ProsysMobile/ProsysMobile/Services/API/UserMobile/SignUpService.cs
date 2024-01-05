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
    public class SignUpService : ISignUpService
    {
        private readonly IApiRequest<ISignUpEndpoint> _request;
        private readonly IApiRequestSelector<ISignUpEndpoint> _apiRequestSelector;

        public SignUpService(IApiRequest<ISignUpEndpoint> request, IApiRequestSelector<ISignUpEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }

        public Task<ServiceBaseResponse<UserMobileDto>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceBaseResponse<UserMobileDto>> SignUp(UserMobileDto userMobileDto, string token, enPriorityType priorityType)
        {
            ServiceBaseResponse<UserMobileDto> result = null;
            Task<ServiceBaseResponse<UserMobileDto>> task;
            Exception exception;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.SignUp(userMobileDto, authorization: "Bearer " + token);

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
