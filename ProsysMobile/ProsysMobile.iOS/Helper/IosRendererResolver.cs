using WiseMobile.Helper;
using WiseMobile.iOS.Helper;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosRendererResolver))]
namespace WiseMobile.iOS.Helper
{
    public class IosRendererResolver : IRendererResolver
    {
        public object GetRenderer(VisualElement element)
        {
            return Xamarin.Forms.Platform.iOS.Platform.GetRenderer(element);
        }

        public bool HasRenderer(VisualElement element)
        {
            return GetRenderer(element) != null;
        }
    }
}