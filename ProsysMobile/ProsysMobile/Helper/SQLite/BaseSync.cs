using Polly.Timeout;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WiseMobile.Helper;
using WiseMobile.Models.APIModels.ResponseModels;

namespace WiseMobile.Helper.SQLite
{
    public class BaseSync
    {

        /// <summary>
        /// run apiClient task and manage response 
        /// </summary>
        /// <typeparam name="T">ApiClient task Response model / BaseResponse T</typeparam>
        /// <param name="task">ApiClient task </param>
        /// <param name="showLoading">true:display loading message</param>
        /// <returns></returns>
        public async Task<ServiceBaseResponse<T>> RunSafeApi<T>(Task<ServiceBaseResponse<T>> task, bool showLoading = false)
        {
            var result = Activator.CreateInstance<ServiceBaseResponse<T>>(); //or default(T)
            try
            {
                result = await task;

                //if (!GlobalSetting.Instance.IsNotConnected)
                //{
                //    //if (showLoading) IsBusy = true;
                //    //result.IsSuccess = true;
                //}
                //else
                //{
                //    //result.IsSuccess = false;
                //    //result.ExceptionMessage =   /*Sabit Mesaj Gösterilebilir*/;
                //}
            }
            catch (ApiException ae)
            {
                WiseLogger.Instance.CrashLog(ae);

                //result.IsSuccess = false;
                //result.ExceptionMessage = ae.Message; /*ExceptionHelper.ApiEx(ae);*/
            }
            catch (TaskCanceledException tce)
            {
                WiseLogger.Instance.CrashLog(tce);

                //result.IsSuccess = false;
                //result.ExceptionMessage = tce.Message; /*Sabit Mesaj Gösterilebilir*/
            }
            catch (TimeoutRejectedException tre)
            {
                WiseLogger.Instance.CrashLog(tre);

                //result.IsSuccess = false;
                //result.ExceptionMessage = tre.Message;/*Sabit Mesaj Gösterilebilir*/
            }
            catch (Exception e)
            {
                WiseLogger.Instance.CrashLog(e);

                if (!e.Message.Contains("canceled."))
                {
                    //result.IsSuccess = false;
                    //result.ExceptionMessage = e.ToString();
                }
            }
            finally
            {
                //if (showLoading) IsBusy = false;
            }

            return result;

        }


    }
}
