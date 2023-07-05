using Xamarin.Forms;
using ProsysMobile.Droid.Helper;
using ProsysMobile.Helper;

[assembly: Dependency(typeof(AndroidRendererResolver))]
namespace ProsysMobile.Droid.Helper
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