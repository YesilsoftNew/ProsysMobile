using Polly;
using ProsysMobile.Endpoints.UserMobile;
using ProsysMobile.Handler;
using ProsysMobile.Helper;
using ProsysMobile.Models.APIModels.RequestModels;
using ProsysMobile.Models.APIModels.ResponseModels;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Selector;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProsysMobile.Resources.Language;

namespace ProsysMobile.Services.API.ItemCategory
{
    public class ItemCategoryService : IItemCategoryService
    {
        private readonly IApiRequest<IItemCategoryEndpoint> _request;
        private readonly IApiRequestSelector<IItemCategoryEndpoint> _apiRequestSelector;

        public ItemCategoryService(IApiRequest<IItemCategoryEndpoint> request, IApiRequestSelector<IItemCategoryEndpoint> apiRequestSelector)
        {
            _request = request;
            _apiRequestSelector = apiRequestSelector;
        }

        public Task<ServiceBaseResponse<Models.APIModels.ResponseModels.ItemCategory>> Get(ApiFilterRequestModel apiFilterRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceBaseResponse<List<Models.APIModels.ResponseModels.ItemCategory>>> ItemCategory(long mainCategoryId, enPriorityType priorityType)
        {
            ServiceBaseResponse<List<Models.APIModels.ResponseModels.ItemCategory>> result = null;
            Task<ServiceBaseResponse<List<Models.APIModels.ResponseModels.ItemCategory>>> task = null;
            Exception exception = null;

            try
            {
                var api = _apiRequestSelector.GetApiRequestByPriority(_request, priorityType);
                task = api.ItemCategory(
                    mainCategoryId: mainCategoryId,
                    culture: Resource.Language,
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
