using System;
using System.Collections.Generic;
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
    public class ItemsService : IItemsService
    {
        private readonly IApiRequest<IItemsEndpoint> _request;
        private readonly IApiRequestSelector<IItemsEndpoint> _apiRequestSelector;

        public ItemsService(IApiRequest<IItemsEndpoint> request, IApiRequestSelector<IItemsEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }
        
        public Task<ServiceBaseResponse<ItemsSubDto>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceBaseResponse<List<ItemsSubDto>>> GetItems(int userId, string filter, string categoryIds, bool? isFavorite, int page, enPriorityType priorityType)
        {
            ServiceBaseResponse<List<ItemsSubDto>> result = null;
            Task<ServiceBaseResponse<List<ItemsSubDto>>> task = null;
            Exception exception = null;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.GetItems(
                    userId: userId,
                    filter: filter,
                    categoryIds: categoryIds,
                    culture: Resource.Language,
                    isFavorite: isFavorite,
                    page: page,
                    "Bearer " + GlobalSetting.Instance.JWTToken
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