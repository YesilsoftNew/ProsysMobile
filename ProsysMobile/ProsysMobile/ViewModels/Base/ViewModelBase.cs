using Polly.Timeout;
using Refit;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using WiseDynamicMobile.Helper;
using ProsysMobile.Helper;
using ProsysMobile.Services.Dialog;
using ProsysMobile.Services.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProsysMobile.ViewModels.Base
{
    public class ViewModelBase : ExtendedBindableObject
    {
        private bool _isBusy;
        
        double time;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                time = 0.2;

                Device.StartTimer(TimeSpan.FromSeconds(0.1), () =>
                {
                    time -= 0.1;

                    if (time <= 0.00)
                    {
                        if (IsBusy)
                            DialogService.ShowLoading();
                        else
                            DialogService.HideLoading();

                        return false;
                    }

                    return true;
                });

                PropertyChanged(() => IsBusy);
            }
        }

        //private bool _isInternetConnectionAvailable;
        //public bool IsInternetConnectionAvailable { get => _isInternetConnectionAvailable; set { _isInternetConnectionAvailable = value; PropertyChanged(() => IsInternetConnectionAvailable); } }

        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        
        public ViewModelBase()
        {
            DialogService = ViewModelLocator.Resolve<IDialogService>();
            NavigationService = ViewModelLocator.Resolve<INavigationService>();

            //GlobalSetting.Instance.IsConnectedInternet = Connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
            //IsInternetConnectionAvailable = GlobalSetting.Instance.IsInternetConnectionAvailable;

            //Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        //private void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs e)
        //{
        //    GlobalSetting.Instance.IsConnectedInternet = e.NetworkAccess == NetworkAccess.Internet ? true : false;

        //    IsInternetConnectionAvailable = GlobalSetting.Instance.IsInternetConnectionAvailable;
        //}
         
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        /// <summary>
        /// run apiClient task and manage response 
        /// </summary>
        /// <typeparam name="T">ApiClient task Response model / BaseResponse T</typeparam>
        /// <param name="task">ApiClient task </param>
        /// <param name="showLoading">true:display loading message</param>
        /// <returns></returns>
        public async Task<T> RunSafeApi<T>(Task<T> task, bool showLoading = false)
        {
            var result = Activator.CreateInstance<T>(); //or default(T)
            try
            {
                if (GlobalSetting.Instance.IsInternetConnectionAvailable)
                {
                    if (showLoading) IsBusy = true;
                    result = await task;
                    //result.IsSuccess = true;
                }
                else
                {
                    //result.IsSuccess = false;
                    //result.ExceptionMessage =   /*Sabit Mesaj Gösterilebilir*/;
                }
            }
            catch (ApiException ae)
            {
                ProsysLogger.Instance.CrashLog(ae);

                //result.IsSuccess = false;
                //result.ExceptionMessage = ae.Message; /*ExceptionHelper.ApiEx(ae);*/
            }
            catch (TaskCanceledException tce)
            {
                ProsysLogger.Instance.CrashLog(tce);

                //result.IsSuccess = false;
                //result.ExceptionMessage = tce.Message; /*Sabit Mesaj Gösterilebilir*/
            }
            catch (TimeoutRejectedException tre)
            {
                ProsysLogger.Instance.CrashLog(tre);

                //result.IsSuccess = false;
                //result.ExceptionMessage = tre.Message;/*Sabit Mesaj Gösterilebilir*/
            }
            catch (Exception e)
            {
                ProsysLogger.Instance.CrashLog(e);

                if (!e.Message.Contains("canceled."))
                {
                    //result.IsSuccess = false;
                    //result.ExceptionMessage = e.ToString();
                }
            }
            finally
            {
                if (showLoading) IsBusy = false;
            }

            return result;
        }
    }
}