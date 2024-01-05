using System;
using System.Threading.Tasks;
using Polly;
using ProsysMobile.Endpoints.Items;
using ProsysMobile.Handler;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Resources.Language;
using ProsysMobile.Selector;
using Refit;

namespace ProsysMobile.Services.API.Items
{
    public class GetHomePageDataService : IGetHomePageDataService
    {
        private readonly IApiRequest<IGetHomePageDataEndpoint> _request;
        private readonly IApiRequestSelector<IGetHomePageDataEndpoint> _apiRequestSelector;

        public GetHomePageDataService(IApiRequestSelector<IGetHomePageDataEndpoint> apiRequestSelector, IApiRequest<IGetHomePageDataEndpoint> request)
        {
            _apiRequestSelector = apiRequestSelector;
            _request = request;
        }


        public Task<ServiceBaseResponse<HomePageDataSubDto>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceBaseResponse<HomePageDataSubDto>> GetHomePageData(int page, int userId, int mainCategoryId, enPriorityType priorityType)
        {
            ServiceBaseResponse<HomePageDataSubDto> result = null;
            Task<ServiceBaseResponse<HomePageDataSubDto>> task;
            Exception exception;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.GetHomePageData(
                    page: page,
                    userId: userId,
                    mainCategoryId: mainCategoryId,
                    culture: Resource.Language,
                    authorization: "Bearer " + GlobalSetting.Instance.JWTToken
                );
                
                result = await Policy
                    .Handle<ApiException>()
                    .WaitAndRetryAsync(retryCount: 2, sleepDurationProvider: retryAttempt =>
                        TimeSpan.FromSeconds(Math.Pow(10, retryAttempt)))
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