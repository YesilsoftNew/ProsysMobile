using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Polly;
using Refit;
using ProsysMobile.Endpoints.Auth;
using ProsysMobile.Helper;
using ProsysMobile.Helper.ApiClient.Handler;
using ProsysMobile.Endpoints.General;
using ProsysMobile.Endpoints.Enum;

namespace ProsysMobile.Helper.ApiClient.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureApi(this IServiceCollection services)
        {
            TimeSpan TimeOut = TimeSpan.FromSeconds(30);
            try
            {
                //Auth Api
                #region Auth Api
                services.AddRefitClient<IAuthEndpoint>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(GlobalSetting.Instance.BaseEndpoint);
                    c.Timeout = TimeOut;
                }).AddTransientHttpErrorPolicy(p => p.RetryAsync())
                //.AddHttpMessageHandler(serviceProvider =>
                //{
                //    return new AuthApiHandler();
                //})
                ;
                #endregion  
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);

                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
