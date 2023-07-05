using Polly;
using Refit;
using System;
using System.Threading.Tasks;
using ProsysMobile.Endpoints.Auth;
using ProsysMobile.Handler;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Selector;

namespace ProsysMobile.Services.API.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IApiRequest<IAuthEndpoint> _request;
        private readonly IApiRequestSelector<IAuthEndpoint> _apiRequestSelector;
        public AuthService(IApiRequest<IAuthEndpoint> request, IApiRequestSelector<IAuthEndpoint> apiRequestSelector)
        {

        }

        public Task<ServiceBaseResponse<UserAuthResponseModel>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceBaseResponse<UserAuthResponseModel>> UserAuthentication(UserAuthRequestModel userAuthRequestModel, enPriorityType priorityType)
        {
            ServiceBaseResponse<UserAuthResponseModel> result = null;
            Task<ServiceBaseResponse<UserAuthResponseModel>> _task = null;
            Exception exception = null;

            try
            {
                var _api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                _task = _api.UserAuthentication(userAuthRequestModel);
                result = await Policy
                          .Handle<ApiException>()
                          .WaitAndRetryAsync(retryCount: 2, sleepDurationProvider: retryAttempt =>
                          TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
                          .ExecuteAsync(async () => await _task);
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
