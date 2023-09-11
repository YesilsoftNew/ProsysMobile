using System;
using System.Threading.Tasks;
using Polly;
using ProsysMobile.Endpoints.UserDevices;
using ProsysMobile.Handler;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Selector;
using Refit;

namespace ProsysMobile.Services.API.UserDevices
{
    public class SaveUserDevicesService : ISaveUserDevicesService
    {
        private readonly IApiRequest<ISaveUserDevicesEndpoint> _request;
        private readonly IApiRequestSelector<ISaveUserDevicesEndpoint> _apiRequestSelector;

        public SaveUserDevicesService(IApiRequest<ISaveUserDevicesEndpoint> request, IApiRequestSelector<ISaveUserDevicesEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }

        public Task<ServiceBaseResponse<Models.APIModels.ResponseModels.UserDevices>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceBaseResponse<Models.APIModels.ResponseModels.UserDevices>> SaveUserDevices(Models.APIModels.ResponseModels.UserDevices userDevice, enPriorityType priorityType)
        {
            ServiceBaseResponse<Models.APIModels.ResponseModels.UserDevices> result = null;
            Task<ServiceBaseResponse<Models.APIModels.ResponseModels.UserDevices>> task = null;
            Exception exception = null;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.SaveUserDevices(userDevice, "Bearer " + GlobalSetting.Instance.JWTToken);
                
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

            return result;        }
    }
}