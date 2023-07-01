using System;
using Xamarin.Forms;

namespace WiseMobile.Helper
{
    public interface IRendererResolver
    {
        object GetRenderer(VisualElement element);
        bool HasRenderer(VisualElement element);
    }
}
