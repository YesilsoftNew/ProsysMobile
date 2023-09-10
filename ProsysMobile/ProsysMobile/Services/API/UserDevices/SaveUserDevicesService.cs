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
                task = api.SaveUserDevices(userDevice, "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik11aGFtbWV0LmRlcmVnb3p1QGhvdG1haWwuY29tIiwiR3VpZCI6IjgzODRjZTc4LWYyMjEtNGEwNi04YzRhLTY5ZTc2ODFjNTI1MCIsIkV4cGlyZWREYXRlVGltZSI6IjkvMTEvMjAyMyA0OjQ0OjM5IFBNIiwibmJmIjoxNjk0MzUzNDc5LCJleHAiOjE2OTQ0Mzk4NzksImlhdCI6MTY5NDM1MzQ3OX0.jcLT6QFgYjxCiq5F2YHqbu6GHEmRRjLgV1EY-tP-6zM");
                
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