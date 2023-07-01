using Microsoft.Extensions.DependencyInjection;
using System;
using WiseMobile.Endpoints.Auth;
using WiseMobile.Helper.ApiClient.Extensions;

namespace WiseMobile.Helper.ApiClient
{
    public class ApiClient
    {
        #region fields
        private static readonly ApiClient _instance = new ApiClient();
        public static ApiClient Instance
        {
            get { return _instance; }
        }

        private static ApiClient shared;
        private static object obj = new object();
        #endregion

        #region properties
        internal static IServiceCollection Services { get; set; }
        internal static IServiceProvider ServiceProvider { get; set; }
        private static ApiClient Shared
        {
            get
            {
                if (shared == null)
                {
                    lock (obj)
                    {
                        if (shared == null)
                        {
                            shared = new ApiClient();
                        }
                    }
                }
                return shared;
            }
        }
        #endregion

        #region api spesific properties
        private IAuthEndpoint _AuthApi { get => ServiceProvider.GetRequiredService<IAuthEndpoint>(); }
        public IAuthEndpoint AuthApi { get => Shared._AuthApi; }

        #endregion

        #region ctor
        public ApiClient()
        {
            if (Services == null)
                Services = new ServiceCollection();
            Init();
        }
        #endregion

        #region internal methods
        private void Init()
        {
            ConfigureServices(Services);
            ServiceProvider = Services.BuildServiceProvider();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApi();
        }
        #endregion
    }
}
