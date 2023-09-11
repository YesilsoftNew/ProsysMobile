using Plugin.Connectivity;
using Plugin.Multilingual;
using ProsysMobile.Helper;
using ProsysMobile.Models.CommonModels.Enums;
using ProsysMobile.Resources.Language;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Plugin.FirebasePushNotification;
using ProsysMobile.Models.APIModels.ResponseModels;
using Xamarin.Essentials;

namespace ProsysMobile.Helper
{
    public static class TOOLS
    {
        
        public static string GenerateUniqueString()
        {
            string _unqStr = string.Empty; Random rnd = new Random();

            int rval = rnd.Next(5); _unqStr =
            'M' +
            ((((new DateTimeOffset(DateTime.UtcNow)).ToUnixTimeMilliseconds().ToString().Substring(8, 5))
            .Substring(0, rval) +
            Convert.ToChar((Int32.Parse((new DateTimeOffset(DateTime.UtcNow)).ToUnixTimeMilliseconds().ToString()
            .Substring(8, 2)) % 24) + 65).ToString() +
            ((new DateTimeOffset(DateTime.UtcNow)).ToUnixTimeMilliseconds().ToString().Substring(8, 5)).ToString()
            .Substring(rval, 5 - rval) +
            Convert.ToChar((Int32.Parse((new DateTimeOffset(DateTime.UtcNow)).ToUnixTimeMilliseconds().ToString()
            .Substring(11, 2)) % 24) + 65).ToString()).Trim()) +
            (new DateTimeOffset(DateTime.UtcNow)).ToUnixTimeMilliseconds().ToString().Substring(12, 1);

            return _unqStr;
        }

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        public static int ToInt(object stringValue)
        {
            try
            {
                if (stringValue == null)
                    return 0;

                setCulture();

                int tmp;
                if (int.TryParse(stringValue.ToString(), out tmp))
                    return tmp;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                return 0;
            }
        }

        public static int? ToIntNull(object stringValue)
        {
            try
            {
                if (stringValue == null)
                    return null;

                setCulture();

                int tmp;
                if (int.TryParse(stringValue.ToString(), out tmp))
                    return tmp;
                else
                    return null;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                return null;
            }
        }

        public static decimal ToDecimal(object stringValue)
        {
            try
            {
                if (stringValue == null)
                    return 0;

                setCulture();

                decimal tmp;

                if (decimal.TryParse(stringValue.ToString(), out tmp))
                    return tmp;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                return 0;
            }
        }

        public static decimal? ToDecimalNull(object stringValue)
        {
            try
            {
                if (stringValue == null)
                    return null;

                setCulture();

                decimal tmp;
                if (decimal.TryParse(stringValue.ToString(), out tmp))
                    return tmp;
                else
                    return null;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                return null;
            }
        }

        public static bool intToBool(int intValue)
        {
            if (intValue == null)
                return false;

            if (intValue == 1)
                return true;
            else
                return false;
        }

        public static int BoolToInt(bool boolValue)
        {
            if (boolValue == true)
                return 1;
            else
                return 0;
        }

        public static object IsNull(object obj, object nullValue)
        {
            try
            {
                if (obj == null)
                    return nullValue;

                return obj;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);

                return obj;
            }
        }

        public static string ToString(object obj)
        {
            try
            {
                if (obj == null) return "";
                return obj.ToString();
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);

                return "";
            }
        }

        public static long DateTimeToUnix(this DateTime dateTime)
        {
            TimeSpan timeSpan = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);

            return (long)timeSpan.TotalSeconds;
        }

        public static long ToLong(object stringValue)
        {
            try
            {
                if (stringValue == null)
                    return 0;

                setCulture();

                long tmp;
                if (long.TryParse(stringValue.ToString(), out tmp))
                    return tmp;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                return 0;
            }
        }

        public static void setCulture()
        {
            try
            {
                if (GlobalSetting.Instance is null) return;

                if (!string.IsNullOrWhiteSpace(GlobalSetting.Instance.AppLanguage))
                {
                    CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo(GlobalSetting.Instance.AppLanguage);
                    Resource.Culture = CrossMultilingual.Current.CurrentCultureInfo;

                    CultureInfo ci = new CultureInfo(GlobalSetting.Instance.AppLanguage);

                    ci.DateTimeFormat.DateSeparator = "/";
                    ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                    ci.NumberFormat.NumberDecimalSeparator = ".";
                    ci.NumberFormat.NumberGroupSeparator = ",";

                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                }
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
            }
        }

        public static bool ConnectionControl()
        {
            try
            {
                return CrossConnectivity.Current.IsConnected;
            }
            catch (Exception ex)
            {
                ProsysLogger.Instance.CrashLog(ex);
                return false;
            }
        }

        public static string GetErrorMessageWithErrorCode(string errCode)
        {
            var errMessage = string.Empty;
            
            switch (errCode)
            {
                case "NO_STOCK":
                    errMessage = "Stokta ürün kalmadı!";
                    break;
            }

            return errMessage;
        }

        public static UserDevices GetUserDevices(int userId)
        {
            return new UserDevices
            {
                UserId = userId,
                LastLoginUserId = userId,
                LastLoginDateTime = DateTime.Now,
                Manufacturer = DeviceInfo.Manufacturer,
                Model = DeviceInfo.Model,
                Name = DeviceInfo.Name,
                Version = DeviceInfo.VersionString,
                Platform = DeviceInfo.Platform.ToString(),
                AppVersion = VersionTracking.CurrentVersion,
                PushToken = CrossFirebasePushNotification.Current?.Token ?? "",
                Timezone = string.Empty,
                RecordDate = DateTime.Now
            };
        }
    }
}