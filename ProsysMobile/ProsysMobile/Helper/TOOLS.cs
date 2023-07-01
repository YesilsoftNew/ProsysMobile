using Plugin.Connectivity;
using Plugin.Multilingual;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using WiseMobile.Helper;
using WiseMobile.Models.CommonModels.Enums;
using WiseMobile.Models.CommonModels.SQLiteModels;
using WiseMobile.Resources.Language;
using Xamarin.Forms;

namespace WiseDynamicMobile.Helper
{
    public static class TOOLS
    {
        public static System.Windows.Input.ICommand ConnectionStausChanged { get; set; }
        //public static System.Windows.Input.ICommand BluetoothConnectionStausChanged { get; set; }
        //public static System.Windows.Input.ICommand BluetoothDataCommand { get; set; }

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

        public static decimal? convertAirPressureForView(decimal? val)
        {// ekrana uygun formata convert ediyor db'de kayıtlar PSI olarak tutuluyor ama data musterini ölçü birimine göre gösterilmeli
            if (val == null) return val;

            if (GlobalSetting.Instance.airPressureUnit == (int)enAirPressureUnit.PSI)
                return val;
            else if (GlobalSetting.Instance.airPressureUnit == (int)enAirPressureUnit.Bar)
                return convertPSItoBAR(val);
            else
                return val;
        }

        public static decimal? convertAirPressureForDb(decimal? val)
        {// ekrana uygun formata convert ediyor db'de kayıtlar PSI olarak tutuluyor ama data musterini ölçü birimine göre gösterilmeli
            if (val == null) return val;

            if (GlobalSetting.Instance.airPressureUnit == (int)enAirPressureUnit.PSI)
                return val;
            else if (GlobalSetting.Instance.airPressureUnit == (int)enAirPressureUnit.Bar)
                return convertBARtoPSI(val);
            else
                return val;
        }

        public static decimal? convertTreadDepthUnitForView(decimal? val)
        {// ekrana uygun formata convert ediyor db'de kayıtlar PSI olarak tutuluyor ama data musterini ölçü birimine göre gösterilmeli
            if (val == null) return val;

            if (GlobalSetting.Instance.treadDepthUnit == (int)enTreadDepthUnit.MM)
                return val;
            else if (GlobalSetting.Instance.treadDepthUnit == (int)enTreadDepthUnit.Inch_32)
                return convertMMtoINCH(val);
            else
                return val;
        }

        public static decimal? convertTreadDepthUnitForDb(decimal? val)
        {// ekrana uygun formata convert ediyor db'de kayıtlar PSI olarak tutuluyor ama data musterini ölçü birimine göre gösterilmeli
            if (val == null) return val;

            if (GlobalSetting.Instance.treadDepthUnit == (int)enTreadDepthUnit.MM)
                return val;
            else if (GlobalSetting.Instance.treadDepthUnit == (int)enTreadDepthUnit.Inch_32)
                return convertINCHtoMM(val);
            else
                return val;
        }

        public static decimal? convertPSItoBAR(decimal? val)
        {
            if (val == null) return val;

            return decimal.Round(val.Value * ToDecimal(0.0689475729), 2);
        }

        public static decimal? convertBARtoPSI(decimal? val)
        {
            if (val == null) return val;

            return decimal.Round(val.Value / ToDecimal(0.0689475729), 2);
        }

        public static decimal? convertMMtoINCH(decimal? val)
        {
            if (val == null) return val;

            return decimal.Round((val.Value * ToDecimal(0.0393700787 * 32)), 2);
        }

        public static decimal? convertINCHtoMM(decimal? val)
        {
            if (val == null) return val;

            return decimal.Round(val.Value / ToDecimal((0.0393700787 * 32)), 2);
        }

        public static string convertBluetoothChannel(string xData)
        {
            try
            {
                string Channel = xData.TrimStart(Convert.ToChar("T"));

                Channel = Channel.Replace(".", System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString());
                Channel = Channel.TrimStart(Convert.ToChar("0"));
                Channel = Channel.TrimStart(Convert.ToChar("0"));

                return Math.Round(TOOLS.ToDecimal(Channel), 1).ToString();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);

                return "";
            }
        }

        public static string convertBluetoothAirPressure(string xData)
        {
            try
            {
                string AirPressure = xData.TrimStart(Convert.ToChar("P"));

                AirPressure = AirPressure.Replace(".", System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator.ToString());
                AirPressure = AirPressure.TrimStart(Convert.ToChar("0"));
                AirPressure = AirPressure.TrimStart(Convert.ToChar("0"));

                return Math.Round(TOOLS.ToDecimal(AirPressure), 1).ToString();
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);

                return "";
            }
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

        public static class AirPressureImportancesColor
        {
            public static Color VeryLow = Color.FromHex("#CF4655");
            public static Color Low = Color.FromHex("#D46A51");
            public static Color Normal = Color.FromHex("#4AAE8C");
            public static Color High = Color.FromHex("#DD9049");
            public static Color VeryHigh = Color.FromHex("#BD7028");
            public static Color Empty = Color.FromHex("#F2F4F5");
        }

        public static class TreadDepthImportancesColor
        {
            public static Color PullPoint = Color.FromHex("#CF4655");
            public static Color NearPullPoint = Color.FromHex("#D46A51");
            public static Color AbovePullPoint = Color.FromHex("#4AAE8C");
            public static Color Empty = Color.FromHex("#F2F4F5");
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
                WiseLogger.Instance.CrashLog(ex);
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
                WiseLogger.Instance.CrashLog(ex);
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
                WiseLogger.Instance.CrashLog(ex);
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
                WiseLogger.Instance.CrashLog(ex);
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
                WiseLogger.Instance.CrashLog(ex);

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
                WiseLogger.Instance.CrashLog(ex);

                return "";
            }
        }

        public static decimal? getChannelAvg(TireAction sTireAction)
        {
            if (sTireAction == null)
                return null;

            if (sTireAction.channel1 == null && sTireAction.channel2 == null && sTireAction.channel3 == null && sTireAction.channel4 == null)
                return null;

            decimal? ChannelSum = 0;
            decimal ChannelCount = 0;

            if (sTireAction.channel1 != null)
            {
                ChannelSum += sTireAction.channel1;
                ChannelCount++;
            }

            if (sTireAction.channel2 != null)
            {
                ChannelSum += sTireAction.channel2;
                ChannelCount++;
            }

            if (sTireAction.channel3 != null)
            {
                ChannelSum += sTireAction.channel3;
                ChannelCount++;
            }

            if (sTireAction.channel4 != null)
            {
                ChannelSum += sTireAction.channel4;
                ChannelCount++;
            }

            if (ChannelCount > 0)
                return decimal.Round((ChannelSum.Value / ChannelCount), 2, MidpointRounding.AwayFromZero);
            else
                return 0;
        }

        public static decimal? getChannelAvg(decimal? channel1, decimal? channel2, decimal? channel3, decimal? channel4)
        {
            if (channel1 == null && channel2 == null && channel3 == null && channel4 == null)
                return null;

            if (channel1 == null && channel2 == null && channel3 == null && channel4 == null)
                return null;

            decimal? ChannelSum = 0;
            decimal ChannelCount = 0;

            if (channel1 != null)
            {
                ChannelSum += channel1;
                ChannelCount++;
            }

            if (channel2 != null)
            {
                ChannelSum += channel2;
                ChannelCount++;
            }

            if (channel3 != null)
            {
                ChannelSum += channel3;
                ChannelCount++;
            }

            if (channel4 != null)
            {
                ChannelSum += channel4;
                ChannelCount++;
            }

            if (ChannelCount > 0)
                return Math.Round(TOOLS.ToDecimal(ChannelSum / ChannelCount), 2);
            else
                return 0;
        }

        //public static decimal? getMaxChannel(TireAction sTireAction)
        //{
        //    if (sTireAction == null)
        //        return null;

        //    decimal? xChannel = null;

        //    if (sTireAction.channel1 != null && sTireAction.channel1 >= TOOLS.ToDecimal(xChannel))
        //        xChannel = sTireAction.channel1;

        //    if (sTireAction.channel2 != null && sTireAction.channel2 >= TOOLS.ToDecimal(xChannel))
        //        xChannel = sTireAction.channel2;

        //    if (sTireAction.channel3 != null && sTireAction.channel3 >= TOOLS.ToDecimal(xChannel))
        //        xChannel = sTireAction.channel3;

        //    if (sTireAction.channel4 != null && sTireAction.channel4 >= TOOLS.ToDecimal(xChannel))
        //        xChannel = sTireAction.channel4;

        //    return xChannel;
        //}

        public static decimal? getMaxChannel(decimal? channel1, decimal? channel2, decimal? channel3, decimal? channel4)
        {
            if (channel1 == null && channel2 == null && channel3 == null && channel4 == null)
                return null;

            decimal? xChannel = null;

            if (channel1 != null && channel1 >= TOOLS.ToDecimal(xChannel))
                xChannel = channel1;

            if (channel2 != null && channel2 >= TOOLS.ToDecimal(xChannel))
                xChannel = channel2;

            if (channel3 != null && channel3 >= TOOLS.ToDecimal(xChannel))
                xChannel = channel3;

            if (channel4 != null && channel4 >= TOOLS.ToDecimal(xChannel))
                xChannel = channel4;

            return xChannel;
        }

        //public static decimal? getAverageChannel(TireAction sTireAction)
        //{
        //    if (sTireAction == null)
        //        return null;

        //    int ChannelCount = 0;
        //    decimal? xChannel = null;

        //    if (sTireAction.channel1 != null)
        //    {
        //        xChannel = sTireAction.channel1 + ToDecimal(xChannel);
        //        ChannelCount++;
        //    }

        //    if (sTireAction.channel2 != null)
        //    {
        //        xChannel = sTireAction.channel2 + ToDecimal(xChannel);
        //        ChannelCount++;
        //    }

        //    if (sTireAction.channel3 != null)
        //    {
        //        xChannel = sTireAction.channel3 + ToDecimal(xChannel);
        //        ChannelCount++;
        //    }

        //    if (sTireAction.channel4 != null)
        //    {
        //        xChannel = sTireAction.channel4 + ToDecimal(xChannel);
        //        ChannelCount++;
        //    }

        //    if (xChannel != null && xChannel > 0)
        //        xChannel = xChannel / ChannelCount;

        //    return xChannel;
        //}

        //public static void ShowMessageBox(MessageBoxType prmMessageBoxType, string prmTitle, string prmMSJ, string OKButton = "", string CancelButton = "")
        //{
        //    pgMessageBox xMessageBox = new pgMessageBox(prmMessageBoxType, prmTitle, prmMSJ, OKButton, CancelButton);
        //    Application.Current.MainPage.Navigation.PushPopupAsync(xMessageBox);
        //}

        public static long DateTimeToUnix(this DateTime dateTime)
        {
            TimeSpan timeSpan = dateTime - new DateTime(1970, 1, 1, 0, 0, 0);

            return (long)timeSpan.TotalSeconds;
        }

        //public static string RemoveSpecialCharacters(this string str)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    foreach (char c in str)
        //    {
        //        if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
        //        {
        //            sb.Append(c);
        //        }
        //    }
        //    return sb.ToString();
        //}

        //public static Base.DtoAttribute GetDtoAttribute(Type t)
        //{
        //    // Get instance of the attribute.
        //    Base.DtoAttribute dtoAttribute = (Base.DtoAttribute)Attribute.GetCustomAttribute(t, typeof(Base.DtoAttribute));

        //    return dtoAttribute;
        //}

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
                WiseLogger.Instance.CrashLog(ex);
                return 0;
            }
        }

        public static Tuple<decimal?, decimal?, decimal?, decimal?> GetInputDirectionBasedChannel(enTreadDepthInputDirection treadDepthInputDirection,
            decimal? channel1 = null, decimal? channel2 = null, decimal? channel3 = null, decimal? channel4 = null)
        {
            // dısdan ice olan version'da herhangi bir degisiklik yapmadan direkt ekrana basabilir db'ye yazabiliriz. icten dısa olan durumda capraz okuyup capraz yazmak gerekir.
            if (treadDepthInputDirection == enTreadDepthInputDirection.DistanIce)
                return Tuple.Create(channel1, channel2, channel3, channel4);
            else
                return Tuple.Create(channel4, channel3, channel2, channel1);
        }

        public static bool ConnectionControl()
        {
            try
            {
                // kullanıcı offline modda kalmak istemiştir bu prm offline ise internetin varlığını sorgulamadan online gibi davranıyoruz. bu parametreyi homepage'deki side menu'den ayarlıyor. default settingslere de kayıt ediyoruz
                if (GlobalSetting.Instance.ConnectionStatus == enConnectionStatus.Offline)
                    return false;

                if (!GlobalSetting.Instance.IsConnectedInternet) // İntetnet baglantısı yok ise hızını kontrol etmeden direkt kullanılamıyor diyebiliriz
                    return false;

                ulong optimalSpeed = (ulong)enConnectionSpeed.Low;
                var speeds = CrossConnectivity.Current.Bandwidths;
                //var connectTypes = CrossConnectivity.Current.ConnectionTypes;

                bool result = false;

                // sadece wifi'da hız kontrolü yapıyorum simdilik
                if (CrossConnectivity.Current.ConnectionTypes.ToList().Any(t => t == Plugin.Connectivity.Abstractions.ConnectionType.WiFi))
                {
                    foreach (var item in speeds) // normalde hep bir adet geliyor
                    {
                        if (item < optimalSpeed)
                            result = false;
                        else
                            result = true;
                    }
                }
                else
                    result = true;

                return result;
            }
            catch (Exception ex)
            {
                WiseLogger.Instance.CrashLog(ex);
                return false;
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
                    AppResources.Culture = CrossMultilingual.Current.CurrentCultureInfo;

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
                WiseLogger.Instance.CrashLog(ex);
            }
        }

        public static string GenerateAutomaticBarcode()
        {
            return "T" + DateTime.Now.ToString("ddMMyyyyHHmmssfff");
        }
    }
}