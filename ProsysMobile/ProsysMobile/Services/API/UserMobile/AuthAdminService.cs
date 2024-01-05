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
    public class AuthAdminService : IAuthAdminService
    {
        private readonly IApiRequest<IAuthAdminEndpoint> _request;
        private readonly IApiRequestSelector<IAuthAdminEndpoint> _apiRequestSelector;

        public AuthAdminService(IApiRequest<IAuthAdminEndpoint> request, IApiRequestSelector<IAuthAdminEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }

        public Task<ServiceBaseResponse<string>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceBaseResponse<string>> AuthAdmin(enPriorityType priorityType)
        {
            ServiceBaseResponse<string> result = null;
            Task<ServiceBaseResponse<string>> task;
            Exception exception;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.AuthAdmin(
                    userName: Constants.ADMIN_USERNAME,
                    password: Constants.ADMIN_PASSWORD
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