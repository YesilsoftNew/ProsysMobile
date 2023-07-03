using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProsysMobile.Helper
{
    public class ProsysLogger
    {
        private static readonly ProsysLogger _instance = new ProsysLogger();
        public static ProsysLogger Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Error Log
        /// </summary>
        /// <param name="properties"> Additional Data</param>
        /// <param name="exception">Exception</param>
        public void CrashLog(Exception exception, Dictionary<string, string> properties = null)
        {
            try
            {
                if (properties is null)
                    properties = new Dictionary<string, string>();

                var methodName = new StackTrace(exception).GetFrame(0).GetMethod().Name;
                var pageName = new StackTrace(exception).GetFrame(0).GetMethod().DeclaringType.FullName;
                properties.Add("JWTToken", !String.IsNullOrWhiteSpace(GlobalSetting.Instance.JWTToken) ? GlobalSetting.Instance.JWTToken : "");
                //TODO : MODELS - USER
                //if (GlobalSetting.Instance.User != null)
                //{
                //    properties.Add("UserToken", GlobalSetting.Instance.User.Token);
                //    properties.Add("FleetId", GlobalSetting.Instance.User.FleetGuid);
                //    properties.Add("Userid", GlobalSetting.Instance.User.UserId.ToString());
                //    properties.Add("Email", GlobalSetting.Instance.User.EMail);
                //}

                properties.Add("DeviceId", !String.IsNullOrWhiteSpace(GlobalSetting.Instance.DeviceGuid) ? GlobalSetting.Instance.DeviceGuid : "");
                properties.Add("Time", DateTime.Now.ToString());
                properties.Add("MethodName", methodName);
                properties.Add("PageName", pageName);
                // TODO: Userid, DeviceId, Token, FleetId,
                Crashes.TrackError(exception, properties);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>   
        /// Info Log
        /// </summary>
        /// <param name="eventName">EventName</param>
        /// <param name="log">Log Data</param>
        /// <param name="additionalData">Additional Data</param>
        public void InfoLog(string eventName, string log, string additionalData = "")
        {
            try
            { //TODO : MODELS - USER
                //Analytics.TrackEvent(log, new Dictionary<string, string> {
                //    { "JWTToken", !String.IsNullOrWhiteSpace(GlobalSetting.Instance.JWTToken) ? GlobalSetting.Instance.JWTToken : "" },
                //    { "UserToken", !String.IsNullOrWhiteSpace(GlobalSetting.Instance.User.Token) ? GlobalSetting.Instance.User.Token : "" },
                //    { "FleetId", !String.IsNullOrWhiteSpace(GlobalSetting.Instance.User.FleetGuid) ? GlobalSetting.Instance.User.FleetGuid : "" },
                //    { "Userid", !String.IsNullOrWhiteSpace(GlobalSetting.Instance.User.UserId.ToString()) ? GlobalSetting.Instance.User.UserId.ToString() : ""},
                //    { "DeviceId", !String.IsNullOrWhiteSpace(GlobalSetting.Instance.DeviceGuid) ? GlobalSetting.Instance.DeviceGuid : "" },

                //    { "EventName", eventName },
                //    { "Email", !String.IsNullOrWhiteSpace(GlobalSetting.Instance.User.EMail)? GlobalSetting.Instance.User.EMail:"" },
                //    { "Time", DateTime.Now.ToString()},
                //    { "Additional Data", additionalData }
                //});
            }
            catch (Exception ex)
            {

            }
        }
    }
}
