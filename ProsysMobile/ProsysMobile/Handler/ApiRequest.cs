﻿using Fusillade;
using ModernHttpClient;
using Refit;
using System;
using System.Net.Http;
using ProsysMobile.Helper;

namespace ProsysMobile.Handler
{
    public class ApiRequest<T> : IApiRequest<T>
    {
        private readonly Lazy<T> _background;
        private readonly Lazy<T> _userInitiated;
        private readonly Lazy<T> _speculative;
        private string _baseApiAddress = GlobalSetting.Instance.BaseEndpoint;

        public ApiRequest()
        {
            _background = new Lazy<T>(() => CreateClient(new RateLimitedHttpMessageHandler(
                new NativeMessageHandler(), Priority.Background), BaseApiAddress));
            _userInitiated = new Lazy<T>(() => CreateClient(new RateLimitedHttpMessageHandler(
                new NativeMessageHandler(), Priority.UserInitiated), BaseApiAddress));
            _speculative = new Lazy<T>(() => CreateClient(new RateLimitedHttpMessageHandler(
                new NativeMessageHandler(), Priority.Speculative), BaseApiAddress));
        }

        public T Background => _background.Value;
        public T Speculative => _speculative.Value;
        public T UserInitiated => _userInitiated.Value;

        public string BaseApiAddress
        {
            get { return _baseApiAddress; }
            set { _baseApiAddress = value; }
        }

        public T CreateClient(HttpMessageHandler handler, string baseApiAddress = null)
        {
            HttpClient client;
            if (GlobalSetting.Instance.UseNativeHttpHandler)
                client = new HttpClient(handler);
            else
                client = new HttpClient();
            client.BaseAddress = new Uri(baseApiAddress ?? BaseApiAddress);
            return RestService.For<T>(client);
        }
    }
}