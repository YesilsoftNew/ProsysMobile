using System.Threading.Tasks;

namespace WiseMobile.Helper
{
    public struct DoubleTapping
    {
        // control whether the button events are executed
        public static bool AllowTap = true;

        // wait for 200ms after allowing another button event to be executed
        public static async void ResumeTap()
        {
            await Task.Delay(400);
            AllowTap = true;
        }
    }
}
