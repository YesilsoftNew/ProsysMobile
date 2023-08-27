using System;
using System.Threading.Tasks;
using Polly;
using ProsysMobile.Endpoints.Items;
using ProsysMobile.Handler;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Selector;
using Refit;

namespace ProsysMobile.Services.API.Items
{
    public class SaveUserMobileFavoriteItemsService : ISaveUserMobileFavoriteItemsService
    {
        private readonly IApiRequest<ISaveUserMobileFavoriteItemsEndpoint> _request;
        private readonly IApiRequestSelector<ISaveUserMobileFavoriteItemsEndpoint> _apiRequestSelector;
        
        public SaveUserMobileFavoriteItemsService(IApiRequest<ISaveUserMobileFavoriteItemsEndpoint> request, IApiRequestSelector<ISaveUserMobileFavoriteItemsEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }
        
        public Task<ServiceBaseResponse<EmptyResponseModel>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceBaseResponse<EmptyResponseModel>> SaveUserMobileFavoriteItems(int userId, int itemId, bool isFavorite, enPriorityType priorityType)
        {
            ServiceBaseResponse<EmptyResponseModel> result = null;
            Task<ServiceBaseResponse<EmptyResponseModel>> task = null;
            Exception exception = null;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.SaveUserMobileFavoriteItems(userId, itemId, isFavorite, "Bearer " + GlobalSetting.Instance.JWTToken);
                
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