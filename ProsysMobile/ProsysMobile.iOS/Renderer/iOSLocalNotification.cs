using ProsysMobile.Helper;
using System;
using System.Threading.Tasks;
using Foundation;
using UserNotifications;

[assembly: Xamarin.Forms.Dependency(typeof(ProsysMobile.iOS.Renderer.OsLocalNotification))]
namespace ProsysMobile.iOS.Renderer
{
    public class OsLocalNotification : ILocalNotification
    {
        public void ShowNotification(string title, string message)
        {
            try
            {
                var center = UNUserNotificationCenter.Current;

                //creat a UNMutableNotificationContent which contains your notification content
                UNMutableNotificationContent notificationContent = new UNMutableNotificationContent();

                notificationContent.Title = "xxx";
                notificationContent.Body= "xxxx";

                notificationContent.Sound = UNNotificationSound.Default;

                UNTimeIntervalNotificationTrigger trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(3, false);

                UNNotificationRequest request = UNNotificationRequest.FromIdentifier("FiveSecond", notificationContent, trigger);


                center.AddNotificationRequest(request,(NSError obj) => 
                {



                });
                
                return;
                
            }
            catch (Exception ex)
            {
                // Hata işleme kodunu burada ekleyin.
            }
        }
    }
}