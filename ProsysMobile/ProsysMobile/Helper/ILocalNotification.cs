using System.Threading.Tasks;

namespace ProsysMobile.Helper
{
    public interface ILocalNotification
    {
        void ShowNotification(string title, string message);
    }
}