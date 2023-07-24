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
    public class SignInService : ISignInService
    {
        private readonly IApiRequest<ISignInEndpoint> _request;
        private readonly IApiRequestSelector<ISignInEndpoint> _apiRequestSelector;

        public SignInService(IApiRequest<ISignInEndpoint> request, IApiRequestSelector<ISignInEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }

        public Task<ServiceBaseResponse<AuthenticationResponseModel>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceBaseResponse<AuthenticationResponseModel>> SignIn(SignIn signIn, enPriorityType priorityType)
        {
            ServiceBaseResponse<AuthenticationResponseModel> result = null;
            Task<ServiceBaseResponse<AuthenticationResponseModel>> task = null;
            Exception exception = null;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.SignIn(signIn);
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
