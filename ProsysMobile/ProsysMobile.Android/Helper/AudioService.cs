using Android.Media;
using WiseMobile.Droid.Helper;
using WiseMobile.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(AudioService))]
namespace WiseMobile.Droid.Helper
{
    public class AudioService : IAudio
    {
        public AudioService() { }

        private MediaPlayer _mediaPlayer;

        //public bool PlayMp3File(string fileName)
        //{
        //	_mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.test);
        //	_mediaPlayer.Start();

        //	return true;
        //}

        public bool PlayWavFile()
        {
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.Laser);
            _mediaPlayer.Start();

            return true;
        }
    }
}