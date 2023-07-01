using Xamarin.Forms;
using WiseMobile.Droid.Helper;
using WiseMobile.Helper;

[assembly: Dependency(typeof(AndroidRendererResolver))]
namespace WiseMobile.Droid.Helper
{
    public class AndroidRendererResolver : IRendererResolver
    {
        public object GetRenderer(VisualElement element)
        {
            return Xamarin.Forms.Platform.Android.Platform.GetRenderer(element);
        }

        public bool HasRenderer(VisualElement element)
        {
            return GetRenderer(element) != null;
        }
    }
}