using Newtonsoft.Json;
using Splat.ModeDetection;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WiseDynamicMobile.Helper;
using ProsysMobile.Helper;

namespace ProsysMobile.Helper.ApiClient.Handler
{
    public class AuthApiHandler : DelegatingHandler
    {
        public AuthApiHandler()
        {

        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(GlobalSetting.Instance.JWTToken) || DateTime.Now <= GlobalSetting.Instance.JWTTokenExpireDate)
                {
                    request.Headers.Add("Accept-Encoding", "br");
                    //request.Headers.Add("Authorization", "Bearer " + GlobalSetting.Instance.JWTToken);
                    //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", GlobalSetting.Instance.JWTToken);

                    var response = await base.SendAsync(request, cancellationToken);
                    return response;



                    //try
                    //{
                    //    var httpResponse = (HttpWebResponse)request.Content();

                    //    if (httpResponse.StatusCode != HttpStatusCode.OK)
                    //    {
                    //        xServiceResponse.ErrorMessage = "Hata";
                    //        xServiceResponse.ResultCode = 500;
                    //        return xServiceResponse;
                    //    }

                    //    using (BrotliStream bs = new BrotliStream(httpResponse.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress))
                    //    {
                    //        using (System.IO.MemoryStream msOutput = new System.IO.MemoryStream())
                    //        {
                    //            bs.CopyTo(msOutput);
                    //            msOutput.Seek(0, System.IO.SeekOrigin.Begin);
                    //            using (StreamReader reader = new StreamReader(msOutput))
                    //            {
                    //                xServiceResponse.Result = reader.ReadToEnd();
                    //                return xServiceResponse;
                    //            }
                    //        }
                    //    }
                    //}
                    //catch (WebException ex)
                    //{
                    //    using (WebResponse response = ex.Response)
                    //    {
                    //        HttpWebResponse retval = (HttpWebResponse)response;
                    //        Console.WriteLine("Error code: {0}", retval.StatusCode);
                    //        using (Stream data = response.GetResponseStream())
                    //        using (var reader = new StreamReader(data))
                    //        {
                    //            xServiceResponse.ErrorMessage = TOOLS.RemoveSpecialCharacters(reader.ReadToEnd());
                    //            xServiceResponse.ResultCode = 500;
                    //            return xServiceResponse;
                    //        }
                    //    }
                    //}






                    //using (var streamWriter = new StreamWriter(request.Content.ReadAsStreamAsync().Result))
                    //{
                    //    //JsonConvert.SerializeObject(sModel, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
                    //    streamWriter.Write(JsonConvert.SerializeObject(sModel, Formatting.Indented, new JsonSerializerSettings
                    //    {
                    //        NullValueHandling = NullValueHandling.Ignore
                    //    }));
                    //}

                    //try
                    //{
                    //    var httpResponse = (HttpWebResponse)request.Content.GetResponseStream.GetResponse();

                    //    //if (httpResponse.StatusCode != HttpStatusCode.OK)
                    //    //{
                    //    //    xServiceResponse.ErrorMessage = "Hata";
                    //    //    xServiceResponse.ResultCode = 500;
                    //    //    return xServiceResponse;
                    //    //}

                    //    using (BrotliStream bs = new BrotliStream(httpResponse.GetResponseStream(), System.IO.Compression.CompressionMode.Decompress))
                    //    {
                    //        using (System.IO.MemoryStream msOutput = new System.IO.MemoryStream())
                    //        {
                    //            bs.CopyTo(msOutput);
                    //            msOutput.Seek(0, System.IO.SeekOrigin.Begin);
                    //            using (StreamReader reader = new StreamReader(msOutput))
                    //            {
                    //                xServiceResponse.Result = reader.ReadToEnd();
                    //                return xServiceResponse;
                    //            }
                    //        }
                    //    }
                    //}
                    //catch (WebException ex)
                    //{
                    //    using (WebResponse response = ex.Response)
                    //    {
                    //        HttpWebResponse retval = (HttpWebResponse)response;
                    //        Console.WriteLine("Error code: {0}", retval.StatusCode);
                    //        using (Stream data = response.GetResponseStream())
                    //        using (var reader = new StreamReader(data))
                    //        {
                    //            xServiceResponse.ErrorMessage = TOOLS.RemoveSpecialCharacters(reader.ReadToEnd());
                    //            xServiceResponse.ResultCode = 500;
                    //            return xServiceResponse;
                    //        }
                    //    }
                    //}
                }
                else
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NotAcceptable) { Content = new StringContent("Token bulunamadı.") };
                }

            }
            catch (TaskCanceledException ex)
            {
                WiseLogger.Instance.CrashLog(ex);

                return new HttpResponseMessage(System.Net.HttpStatusCode.RequestTimeout) { Content = new StringContent("Timeout") };
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);

                return new HttpResponseMessage(System.Net.HttpStatusCode.Conflict) { Content = new StringContent(ex.ToString()) };
            }
        }
    }
}
